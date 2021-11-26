using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Mappers
{
  internal class ConfigurationServicesInfoMapper : IConfigurationServicesInfoMapper
  {
    public ConfigurationServicesInfo Map(DbServiceConfiguration dbconfig)
    {
      if(dbconfig == null)
      {
        return null;
      }

      return new ConfigurationServicesInfo
      {
        Id = dbconfig.Id,
        ServiceName = dbconfig.ServiceName,
        IsActive = dbconfig.IsActive
      };
    }
  }
}
