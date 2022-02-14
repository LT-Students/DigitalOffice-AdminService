using System;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Db;

namespace LT.DigitalOffice.AdminService.Data
{
  public class GraphicalUserInterfaceSettingRepository : IGraphicalUserInterfaceSettingRepository
  {
    private readonly IDataProvider _provider;
    public GraphicalUserInterfaceSettingRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }
    public async Task<Guid?> Create(DbGraphicalUserInterfaceSetting request)
    {
      if (request is null)
      {
        return null;
      }

      _provider.GraphicalUserInterfaceSettings.Add(request);
      await _provider.SaveAsync();

      return request.Id;
    }
  }
}
