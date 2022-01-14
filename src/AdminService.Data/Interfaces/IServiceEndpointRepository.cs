using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceEndpointRepository
  {
    Task<bool> CreateAsync(List<DbServiceEndpoint> dbServicesEndpoints);

    Task<DbServiceConfiguration> GetAsync(string serviceName);
  }
}
