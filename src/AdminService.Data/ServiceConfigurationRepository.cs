using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Requests.Filtres;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data
{
  public class ServiceConfigurationRepository : IServiceConfigurationRepository
  {
    private readonly IDataProvider _provider;
    public ServiceConfigurationRepository(IDataProvider provider)
    {
      _provider = provider;
    }
    public async Task<(List<DbServiceConfiguration> dbconfig, int totalCount)> FindAsync(FindAdminFilter filter)
    {
      if(filter == null)
      {
        return (null, default);
      }

      IQueryable<DbServiceConfiguration> dbconfig = _provider.ServicesConfigurations.AsQueryable();

      return (
        await dbconfig.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await dbconfig.CountAsync());
    }
  }
}
