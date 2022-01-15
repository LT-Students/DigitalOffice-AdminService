using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.AdminService.Models.Dto.Models
{
  public record ServiceEndpointsInfo
  {
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    public List<EndpointInfo> Endpoints { get; set; }
  }
}
