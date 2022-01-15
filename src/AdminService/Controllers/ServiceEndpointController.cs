using System;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.AdminService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ServiceEndpointController : ControllerBase
  {
    [HttpGet("get")]
    public async Task<OperationResultResponse<ServiceEndpointsInfo>> SomeGet(
      [FromServices] IGetServiceEndpointCommand command,
      [FromQuery] Guid serviceId)
    {
      return await command.ExecuteAsync(serviceId);
    }
  }
}
