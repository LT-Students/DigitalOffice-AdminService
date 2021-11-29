using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Mappers
{
  public class ConfigurationServicesInfoMapper : IConfigurationServicesInfoMapper
  {
    public ServiceConfigurationInfo Map(DbServiceConfiguration dbconfig)
    {
      if(dbconfig is null)
      {
        return null;
      }

      return new ServiceConfigurationInfo
      {
        Id = dbconfig.Id,
        ServiceName = dbconfig.ServiceName,
        IsActive = dbconfig.IsActive
      };
    }
  }
}
