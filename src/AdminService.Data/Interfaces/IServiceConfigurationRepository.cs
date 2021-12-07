using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Requests;

namespace LT.DigitalOffice.AdminService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceConfigurationRepository
  {
    Task<(List<DbServiceConfiguration> dbServicesConfigurations, int totalCount)> FindAsync(BaseFindFilter filter);
    Task<List<Guid>> EditAsync(List<Guid> servicesId);
  }
}
