using System;
using System.Collections.Generic;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbServiceEndpointMapper
  {
    List<DbServiceEndpoint> Map(Guid serviceId, List<string> serviceEndpoints);
  }
}
