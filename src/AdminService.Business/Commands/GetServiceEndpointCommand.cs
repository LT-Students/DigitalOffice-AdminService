using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands
{
  public class GetServiceEndpointCommand : IGetServiceEndpointCommand
  {
    private readonly IServiceConfigurationRepository _configurationRepository;
    private readonly IServiceEndpointRepository _endpointRepository;
    private readonly IEndpointInfoMapper _mapper;
    private readonly IResponseCreator _responseCreator;

    public GetServiceEndpointCommand(
      IServiceConfigurationRepository configurationRepository,
      IServiceEndpointRepository repository,
      IEndpointInfoMapper mapper,
      IResponseCreator responseCreator)
    {
      _configurationRepository = configurationRepository;
      _endpointRepository = repository;
      _mapper = mapper;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<ServiceEndpointsInfo>> ExecuteAsync(Guid serviceId)
    {
      ServiceEndpointsInfo serviceEndpointsInfo = new ServiceEndpointsInfo();

      serviceEndpointsInfo.ServiceName = await _configurationRepository.GetServiceNameAsync(serviceId);
      serviceEndpointsInfo.ServiceId = serviceId;

      if (serviceEndpointsInfo.ServiceName is null)
      {
        return _responseCreator.CreateFailureResponse<ServiceEndpointsInfo>(HttpStatusCode.NotFound);
      }

      DbServiceConfiguration dbServiceConfiguration = await _endpointRepository
        .GetAsync(serviceEndpointsInfo.ServiceName);

      if (dbServiceConfiguration.Endpoints is null)
      {
        return _responseCreator.CreateFailureResponse<ServiceEndpointsInfo>(HttpStatusCode.NotFound);
      }

      serviceEndpointsInfo.Endpoints = dbServiceConfiguration.Endpoints.Select(x => _mapper.Map(x)).ToList();

      return new OperationResultResponse<ServiceEndpointsInfo>()
      {
        Body = serviceEndpointsInfo,
        Status = OperationResultStatusType.FullSuccess,
        Errors = null
      };
    }
  }
}
