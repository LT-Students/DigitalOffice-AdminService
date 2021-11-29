using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.AdminService.Business.Commands
{
  public class FindServiceConfigurationCommand : IFindServiceConfigurationCommand
  {
    private readonly IBaseFindFilterValidator _baseFindValidator;
    private readonly IServiceConfigurationRepository _configrepository;
    private readonly IConfigurationServicesInfoMapper _configmapper;
    private readonly IResponseCreater _responseCreator;

    public FindServiceConfigurationCommand(
      IBaseFindFilterValidator baseFindValidator,
      IServiceConfigurationRepository configrepository,
      IConfigurationServicesInfoMapper configmapper,
      IResponseCreater responseCreator)
    {
      _baseFindValidator = baseFindValidator;
      _configrepository = configrepository;
      _configmapper = configmapper;
      _responseCreator = responseCreator;
    }

    public async Task<FindResultResponse<ServiceConfigurationInfo>> ExecuteAsync(BaseFindFilter filter)
    {
      if (!_baseFindValidator.ValidateCustom(filter, out List<string> errors))
      {
        return _responseCreator.CreateFailureFindResponse<ServiceConfigurationInfo>(HttpStatusCode.BadRequest, errors);
      };

      FindResultResponse<ServiceConfigurationInfo> response = new();
      response.Body = new();

      (List<DbServiceConfiguration> dbConfig, int totalCount) =
        await _configrepository.FindAsync(filter);

      response.TotalCount = totalCount;
      response.Body.AddRange(dbConfig.Select((dbConfig) => _configmapper.Map(dbConfig)));

      return response;
    }
  }
}

