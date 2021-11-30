﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Requests;
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
  }
}
