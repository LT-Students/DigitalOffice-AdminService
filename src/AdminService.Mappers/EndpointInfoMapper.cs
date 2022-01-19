using System.Linq;
using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Mappers
{
  public class EndpointInfoMapper : IEndpointInfoMapper
  {
    public ServiceEndpointsInfo Map(DbServiceConfiguration dbConfig)
    {
      return new ServiceEndpointsInfo()
      {
        ServiceId = dbConfig.Id,
        ServiceName = dbConfig.ServiceName,
        Endpoints = dbConfig.Endpoints?.Select(x => new EndpointInfo
        {
          Id = x.Id,
          Name = x.Name,
        }).ToList()
      };
    }
  }
}
