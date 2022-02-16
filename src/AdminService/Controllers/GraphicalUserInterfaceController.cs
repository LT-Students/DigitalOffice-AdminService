using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.GraphicalUserInterface.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.AdminService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class GraphicalUserInterfaceController : ControllerBase
  {
    [HttpGet("get")]
    public async Task<OperationResultResponse<GuiInfo>> GetAsync(
      [FromServices] IGetGraphicalUserInterfaceCommand command)
    {
      return await command.ExecuteAsync();
    }
  }
}
