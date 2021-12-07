using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.Interfaces
{
  [AutoInject]
  public interface IEditServiceConfigurationCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(List<Guid> servicesId);
  }
}
