using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.AdminService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    [HttpGet("find")]
    public async Task<FindResultResponse<ServiceConfigurationInfo>> FindAsync(
      [FromServices] IFindServiceConfigurationCommand command,
      [FromQuery] BaseFindFilter filter)
    {
      return await command.ExecuteAsync(filter);
    }
  }
}
