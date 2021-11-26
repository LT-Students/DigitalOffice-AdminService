using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests.Filtres;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands
{
  public class FindAdminCommand : IFindAdminCommand
  {
    public async Task<FindResultResponse<ConfigurationServicesInfo>> ExecuteAsync(FindAdminFilter filter)
    {
            
      return null;
    }
  }
}
