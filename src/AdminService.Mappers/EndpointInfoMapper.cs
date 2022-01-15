using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Mappers
{
  public class EndpointInfoMapper : IEndpointInfoMapper
  {
    public EndpointInfo Map(DbServiceEndpoint endpoint)
    {
      if (endpoint is null)
      {
        return null;
      }

      return new EndpointInfo()
      {
        Id = endpoint.Id,
        Name = endpoint.Name,
      };
    }
  }
}
