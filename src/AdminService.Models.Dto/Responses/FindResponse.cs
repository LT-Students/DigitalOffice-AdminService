using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Models.Dto.Responses
{
  public record FindResponse
  {
    public ConfigurationServicesInfo info { get; set; }
  }
}
