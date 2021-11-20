using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef
{
    public class AdminServiceDbContext : DbContext, IDataProvider
    {
    public AdminServiceDbContext(DbContextOptions<AdminServiceDbContext> options)
      : base(options)
    {
    }
    }
}
