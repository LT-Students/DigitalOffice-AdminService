using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef.Configuration
{
  public class DbServiceEndpointConfiguration : IEntityTypeConfiguration<DbServiceEndpoint>
  {
    public void Configure(EntityTypeBuilder<DbServiceEndpoint> builder)
    {
      builder
        .ToTable(DbServiceEndpoint.TableName);

      builder
        .HasKey(se => se.Id);
    }
  }
}
