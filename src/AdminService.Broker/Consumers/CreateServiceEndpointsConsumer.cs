using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Db.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Admin;
using MassTransit;

namespace LT.DigitalOffice.AdminService.Broker.Consumers
{
  public class CreateServiceEndpointsConsumer : IConsumer<ICreateServiceEndpointsRequest>
  {
    private readonly IDbServiceEndpointMapper _mapper;
    private readonly IServiceEndpointRepository _repository;

    public CreateServiceEndpointsConsumer(
      IDbServiceEndpointMapper mapper,
      IServiceEndpointRepository repository)
    {
      _mapper = mapper;
      _repository = repository;
    }

    private async Task<bool> CreateEndpoints(ICreateServiceEndpointsRequest request)
    {
      DbServiceConfiguration dbServiceConfiguration = await _repository.GetAsync(request.ServiceName);
      if (dbServiceConfiguration is null)
      {
        return false;
      }

      List<string> savedEndpoints = dbServiceConfiguration.Endpoints.Select(x => x.Endpoint).ToList();
      List<string> newEndpoints = request.EndpointsNames.Where(x => !savedEndpoints.Contains(x)).ToList();

      return
        newEndpoints.Any() ?
        await _repository.CreateAsync(_mapper.Map(dbServiceConfiguration.Id, newEndpoints)) :
        false;
    }

    public async Task Consume(ConsumeContext<ICreateServiceEndpointsRequest> context)
    {
      var response = OperationResultWrapper.CreateResponse(CreateEndpoints, context.Message);

      await context.RespondAsync<IOperationResult<bool>>(response);
    }
  }
}
