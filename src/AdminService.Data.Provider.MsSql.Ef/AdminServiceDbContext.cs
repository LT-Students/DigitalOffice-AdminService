using Microsoft.EntityFrameworkCore;

namespace AdminService.Data.Provider.MsSql.Ef
{
    public class AdminServiceDbContext : DbContext, IDataProvider
    {
    public AdminServiceDbContext(DbContextOptions<AdminServiceDbContext> options)
      : base(options)
    {
    }
    }
}
