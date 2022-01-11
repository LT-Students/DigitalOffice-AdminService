using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data
{
  public class ServiceEndpointRepository : IServiceEndpointRepository
  {
    private readonly IDataProvider _provider;

    public ServiceEndpointRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<bool> CreateAsync(List<DbServiceEndpoint> servicesEndpoints)
    {
      if (!servicesEndpoints.Any())
      {
        return false;
      }

      _provider.ServicesEndpoints.AddRange(servicesEndpoints);
      await _provider.SaveAsync();

      return true;
    }

    public async Task<DbServiceConfiguration> GetAsync(string serviceName)
    {
      return await _provider.ServicesConfigurations
        .Include(x => x.Endpoints)
        .FirstOrDefaultAsync(x => x.ServiceName == serviceName);
    }
  }
}
