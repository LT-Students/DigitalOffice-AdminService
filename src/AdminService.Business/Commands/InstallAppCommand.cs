using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
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
  public class InstallAppCommand : IInstallAppCommand
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
      if (smtpInfo is null)
      {
        return false;
      }

      try
      {
        Response<IOperationResult<bool>> response = await _rcCreateSmtp.GetResponse<IOperationResult<bool>>(
          ICreateSmtpCredentialsRequest.CreateObj(
            host: smtpInfo.Host,
            port: smtpInfo.Port,
            enableSsl: smtpInfo.EnableSsl,
            email: smtpInfo.Email,
            password: smtpInfo.Password));

        if (response.Message.IsSuccess)
        {
          return response.Message.Body;
        }

        _logger.LogWarning(
          "Error while creating smtp credentials.\nErrors: {Errors}",
          string.Join("\n", response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(
          exc,
          "Can not create smtp credentials.");
      }
      errors.Add("Can not create smtp credentials.");

      return false;
    }

    private async Task<bool> CreateAdmin(AdminInfo adminInfo, List<string> errors)
    {
      if (adminInfo is null)
      {
        return false;
      }

      try
      {
        Response<IOperationResult<bool>> response = await _rcCreateAdmin.GetResponse<IOperationResult<bool>>(
          ICreateAdminRequest.CreateObj(
            firstName: adminInfo.FirstName,
            middleName: adminInfo.MiddleName,
            lastName: adminInfo.LastName,
            email: adminInfo.Email,
            login: adminInfo.Login,
            password: adminInfo.Password));

        if (response.Message.IsSuccess)
        {
          return response.Message.Body;
        }

        _logger.LogWarning(
          "Error while creating admin.\nErrors: {Errors}",
          string.Join("\n", response.Message.Errors));
      }
      catch (Exception exc)
      {
        _logger.LogError(
          exc,
          "Can not create admin.");
      }

      errors.Add("Can not create admin.");
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

      OperationResultResponse<bool> response = new();

      int countDisabledServices = await _repository.InstallAppAsync(request.ServicesToDisable);

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      if (request.ServicesToDisable.Count != countDisabledServices)
      {
        response.Status = OperationResultStatusType.PartialSuccess;
        response.Errors = new List<string>() { "not all services have been disabled." };
      }
      else
      {
        response.Body = true;
      }

      return response;
    }
  }
}
