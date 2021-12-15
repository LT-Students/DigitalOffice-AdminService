using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.AdminService.Validation.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Requests.Email;
using LT.DigitalOffice.Models.Broker.Requests.User;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LT.DigitalOffice.AdminService.Business.Commands
{
  public class InstallAppCommand
  {
    private readonly ILogger<IInstallAppCommand> _logger;
    private readonly IInstallAppRequestValidator _validator;
    private readonly IServiceConfigurationRepository _repository;
    private readonly IRequestClient<ICreateAdminRequest> _rcCreateAdmin;
    private readonly IRequestClient<ICreateSmtpCredentialsRequest> _rcCreateSmtp;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;

    public InstallAppCommand(
      ILogger<IInstallAppCommand> logger,
      IInstallAppRequestValidator validator,
      IServiceConfigurationRepository repository,
      IRequestClient<ICreateAdminRequest> rcCreateAdmin,
      IRequestClient<ICreateSmtpCredentialsRequest> rcCreateSmtp,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator)
    {
      _logger = logger;
      _validator = validator;
      _repository = repository;
      _rcCreateAdmin = rcCreateAdmin;
      _rcCreateSmtp = rcCreateSmtp;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
    }

    private async Task<bool> CreateSmtp(SmtpInfo smtpInfo, List<string> errors)
    {
      string message = "Can not create smtp credentials.";

      try
      {
        Response<IOperationResult<bool>> response = await _rcCreateSmtp.GetResponse<IOperationResult<bool>>(
          ICreateSmtpCredentialsRequest.CreateObj(
            host: smtpInfo.Host,
            port: smtpInfo.Port,
            enableSsl: smtpInfo.EnableSsl,
            email: smtpInfo.Email,
            password: smtpInfo.Password));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(message, string.Join("\n", response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, message);
      }
      errors.Add(message);

      return false;
    }

    private async Task<bool> CreateAdmin(AdminInfo info, List<string> errors)
    {
      string message = "Can not create admin.";

      try
      {
        Response<IOperationResult<bool>> response = await _rcCreateAdmin.GetResponse<IOperationResult<bool>>(
          ICreateAdminRequest.CreateObj(
            info.FirstName, 
            info.MiddleName, 
            info.LastName, 
            info.Email, 
            info.Login, 
            info.Password));

        if (response.Message.IsSuccess && response.Message.Body)
        {
          return true;
        }

        _logger.LogWarning(message, string.Join("\n", response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, message);
      }

      errors.Add(message);
      return false;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(InstallAppRequest request)
    {
      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      if (!(await CreateSmtp(request.SmtpInfo, errors) &&
        await CreateAdmin(request.AdminInfo, errors)))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      List<Guid> confirmedServicesIds = await _repository.AreExistingIdsAsync(request.ServicesToDisable);

      if (confirmedServicesIds is null || !confirmedServicesIds.Any())
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }
      else
      {
        bool response = await _repository.InstallAppAsync(confirmedServicesIds);
        
        if (response)
        {
          _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

          return new OperationResultResponse<bool>
          {
            Status = OperationResultStatusType.FullSuccess,
            Body = true
          };
        }
        else
        {
          return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
        }
      }
    }
  }
}
