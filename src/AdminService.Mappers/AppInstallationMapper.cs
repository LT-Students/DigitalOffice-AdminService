using System;
using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.AdminService.Mappers
{
  public class AppInstallationMapper : IAppInstallationMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppInstallationMapper(
        IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }
    public DbServiceConfiguration Map(InstallAppRequest request)
    {
      if (request == null)
      {
        return null;
      }

      return new DbServiceConfiguration
      {
        Id = Guid.NewGuid(),
        IsActive = false,
        ModifiedAtUtc = DateTime.UtcNow,
        ModifiedBy = _httpContextAccessor.HttpContext.GetUserId()
      };
    }
  }
}
