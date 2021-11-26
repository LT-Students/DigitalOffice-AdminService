﻿using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests.Filtres;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.Interfaces
{
  [AutoInject] //??????? настроить файндфилтр нормально
  public interface IFindAdminCommand
  {
    Task<FindResultResponse<ConfigurationServicesInfo>> ExecuteAsync(FindAdminFilter filter);
  }
}
