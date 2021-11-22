using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef
{
  public class AdminServiceDbContext : DbContext, IDataProvider
  {
    public AdminServiceDbContext(DbContextOptions<AdminServiceDbContext> options)
    : base(options)
    {
    }

    // Fluent API is written here.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.AdminService.Models.Db"));
    }

    public void Save()
    {
      SaveChanges();
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;

      return Entry(obj).State;
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
    }

  }
}
