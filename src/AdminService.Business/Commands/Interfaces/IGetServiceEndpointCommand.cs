using System;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.Interfaces
{
  [AutoInject]
  public interface IGetServiceEndpointCommand
  {
    public Task<OperationResultResponse<ServiceEndpointsInfo>> ExecuteAsync(Guid serviceId);
  }
}
