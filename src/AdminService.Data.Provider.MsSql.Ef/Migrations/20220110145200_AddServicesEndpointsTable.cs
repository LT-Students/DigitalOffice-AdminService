using System;
using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(AdminServiceDbContext))]
  [Migration("20220110145200_AddServicesEndpointsTable")]

  public class AddServicesEndpointsTable : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbServiceEndpoint.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          ServiceId = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbServiceEndpoint.TableName}", x => x.Id);
        });
    }
  }
}
