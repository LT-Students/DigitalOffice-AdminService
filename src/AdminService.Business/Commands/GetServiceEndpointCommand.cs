using System;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands
{
  public class GetServiceEndpointCommand : IGetServiceEndpointCommand
  {
    private readonly IServiceConfigurationRepository _configurationRepository;
    private readonly IEndpointInfoMapper _mapper;
    private readonly IResponseCreator _responseCreator;

    public GetServiceEndpointCommand(
      IServiceConfigurationRepository configurationRepository,
      IEndpointInfoMapper mapper,
      IResponseCreator responseCreator)
    {
      _configurationRepository = configurationRepository;
      _mapper = mapper;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<ServiceEndpointsInfo>> ExecuteAsync(Guid serviceId)
    {
      ServiceEndpointsInfo serviceEndpointsInfo = new ServiceEndpointsInfo();

      DbServiceConfiguration dbServiceConfiguration = await _configurationRepository
        .GetAsync(serviceId);

      if (dbServiceConfiguration is null)
      {
        return _responseCreator.CreateFailureResponse<ServiceEndpointsInfo>(HttpStatusCode.NotFound);
      }

      serviceEndpointsInfo = _mapper.Map(dbServiceConfiguration);

      return new OperationResultResponse<ServiceEndpointsInfo>()
      {
        Body = serviceEndpointsInfo,
      };
    }
  }
}
