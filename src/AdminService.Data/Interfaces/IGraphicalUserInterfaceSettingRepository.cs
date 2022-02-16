using System;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Data.Interfaces
{
  [AutoInject]
  public interface IGraphicalUserInterfaceSettingRepository
  {
    Task<Guid?> Create(DbGraphicalUserInterfaceSetting request);

    Task<DbGraphicalUserInterfaceSetting> Get();
  }
}
