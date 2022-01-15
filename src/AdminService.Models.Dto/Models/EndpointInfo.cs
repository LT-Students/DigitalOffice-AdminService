using System;

namespace LT.DigitalOffice.AdminService.Models.Dto.Models
{
  public record EndpointInfo
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
  }
}
