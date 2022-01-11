using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.AdminService.Mappers.Db.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;

namespace LT.DigitalOffice.AdminService.Mappers.Db
{
  public class DbServiceEndpointMapper : IDbServiceEndpointMapper
  {
    public List<DbServiceEndpoint> Map(Guid serviceId, List<string> serviceEndpoints)
    {
      if (serviceEndpoints is null)
      {
        return default;
      }

      return serviceEndpoints
        .Select(
          endpoint => new DbServiceEndpoint
          {
            Id = Guid.NewGuid(),
            ServiceId = serviceId,
            Endpoint = endpoint,
            Service = null
          })
        .ToList();
    }
  }
}
