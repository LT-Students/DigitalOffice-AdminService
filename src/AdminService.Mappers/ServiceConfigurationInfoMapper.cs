using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Mappers
{
  public class ServiceConfigurationInfoMapper : IServiceConfigurationInfoMapper
  {
    public ServiceConfigurationInfo Map(DbServiceConfiguration dbServicesConfigurations)
    {
      if (dbServicesConfigurations is null)
      {
        return null;
      }

      return new ServiceConfigurationInfo
      {
        Id = dbServicesConfigurations.Id,
        ServiceName = dbServicesConfigurations.ServiceName,
        IsActive = dbServicesConfigurations.IsActive
      };
    }
  }
}
