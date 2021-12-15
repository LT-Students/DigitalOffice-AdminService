using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data
{
  public class ServiceConfigurationRepository : IServiceConfigurationRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServiceConfigurationRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(List<DbServiceConfiguration> dbServicesConfigurations, int totalCount)> FindAsync(BaseFindFilter filter)
    {
      if (filter is null)
      {
        return (null, default);
      }

      IQueryable<DbServiceConfiguration> dbServicesConfigurations = _provider.ServicesConfigurations;

      return (
        await dbServicesConfigurations.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await dbServicesConfigurations.CountAsync());
    }

    public async Task<bool> InstallAppAsync(List<Guid> confirmedServicesIds)
    {
      foreach (Guid serviceId in confirmedServicesIds)
      {
        DbServiceConfiguration configuration = await _provider.ServicesConfigurations
          .FirstOrDefaultAsync(x => x.Id == serviceId);

        configuration.IsActive = false;
        configuration.ModifiedAtUtc = DateTime.UtcNow;
        configuration.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();

        _provider.ServicesConfigurations.Update(configuration);
      }

      await _provider.SaveAsync();
      return true;
    }

    public async Task<List<Guid>> AreExistingIdsAsync(List<Guid> servicesIds)
    {
      if (servicesIds is null)
      {
        return null;
      }

      if (!await _provider.ServicesConfigurations.AnyAsync())
      {
        return null;
      }

      return await _provider.ServicesConfigurations
        .Where(s => servicesIds.Contains(s.Id))
        .Select(s => s.Id)
        .ToListAsync();
    }
  }
}
