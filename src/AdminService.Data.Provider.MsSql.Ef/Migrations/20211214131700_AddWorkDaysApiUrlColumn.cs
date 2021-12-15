using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(AdminServiceDbContext))]
  [Migration("20211214131700_AddWorkDaysApiUrlColumn")]
  class AddWorkDaysApiUrlColumn : Migration
  {
    protected override void Up(MigrationBuilder builder)
    {
      builder.AddColumn<string>(
      name: "WorkDaysApiUrl",
      table: DbServiceConfiguration.TableName,
      nullable: false);
    }

    protected override void Down(MigrationBuilder builder)
    {
      builder.DropColumn(
        name: nameof(DbServiceConfiguration.WorkDaysApiUrl),
        table: DbServiceConfiguration.TableName);
    }
  }
}
