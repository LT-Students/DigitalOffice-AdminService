using System.Linq;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Database;
using LT.DigitalOffice.Kernel.Enums;

namespace LT.DigitalOffice.AdminService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    IQueryable<DbServiceConfiguration> ServicesConfigurationsQueryable { get; }

  }
}
