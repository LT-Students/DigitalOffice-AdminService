using System;

namespace LT.DigitalOffice.AdminService.Models.Db
{
  public class DbServiceEndpoint
  {
    public const string TableName = "ServicesEndpoints";

    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public string Endpoint { get; set; }

    public DbServiceConfiguration Service { get; set; }

    public DbServiceEndpoint()
    {
      Service = new DbServiceConfiguration();
    }
  }
}
