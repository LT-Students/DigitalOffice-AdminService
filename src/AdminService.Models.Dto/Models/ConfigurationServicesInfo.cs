using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.AdminService.Models.Dto.Models
{
  public class ConfigurationServicesInfo
  {
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public bool IsActive { get; set; }

  }
}
