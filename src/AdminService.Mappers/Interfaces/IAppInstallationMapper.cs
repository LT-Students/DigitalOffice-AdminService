using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Mappers.Interfaces
{
  [AutoInject]
  public interface IAppInstallationMapper
  {
    DbServiceConfiguration Map(InstallAppRequest request);
  }
}
