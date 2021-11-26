using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Requests.Filtres;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceConfigurationRepository
  {
    Task<(List<DbServiceConfiguration> dbconfig, int totalCount)> FindAsync(FindAdminFilter filter);
  }
}
