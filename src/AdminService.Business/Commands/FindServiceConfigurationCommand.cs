using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests.Filtres;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.AdminService.Business.Commands
{
  public class FindServiceConfigurationCommand : IFindServiceConfigurationCommand
  {
    private readonly IBaseFindFilterValidator _baseFindValidator;
    private readonly IServiceConfigurationRepository _configrepository;
    private readonly IConfigurationServicesInfoMapper _configmapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreater _responseCreator;

    public FindAdminCommand(
      IBaseFindFilterValidator baseFindValidator,
      IServiceConfigurationRepository configrepository,
      IConfigurationServicesInfoMapper configmapper, 
      IHttpContextAccessor httpContextAccessor,
      IResponseCreater responseCreator)
    {
      _baseFindValidator = baseFindValidator;
      _configrepository = configrepository;
      _configmapper = configmapper;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
    }

    public async Task<FindResultResponse<ConfigurationServicesInfo>> ExecuteAsync(FindAdminFilter filter)
    {
      if(!_baseFindValidator.ValidateCustom(filter, out List<string> errors))
      {
        return _responseCreator.CreateFailureFindResponse<ConfigurationServicesInfo>(HttpStatusCode.BadRequest, errors);
      };     

      FindResultResponse<ConfigurationServicesInfo> response = new();
      response.Body = new();

      (List<DbServiceConfiguration> dbConfig, int totalCount) =
        await _configrepository.FindAsync(filter);

      response.TotalCount = totalCount;      
      response.Body.AddRange(dbConfig.Select((dbConfig) => _configmapper.Map(dbConfig)));

      response.Status = response.Errors.Any() 
        ? OperationResultStatusType.PartialSuccess 
        : OperationResultStatusType.FullSuccess;

      return response;
    }   
  }
}

