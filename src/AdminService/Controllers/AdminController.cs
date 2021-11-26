using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests.Filtres;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.AdminService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    [HttpGet("find")]
    public async Task<FindResultResponse<ConfigurationServicesInfo>> FindAsync(
      [FromServices] IFindAdminCommand command,
      [FromQuery] FindAdminFilter filter)
    {
      // return await command.ExecuteAsync(filter);
      return null;
    }
  }
}
