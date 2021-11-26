﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests.Filtres;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.AdminService.Business.Commands
{
  public class FindAdminCommand : IFindAdminCommand
  {
    private readonly IBaseFindFilterValidator _baseFindValidator;
    private readonly IServiceConfigurationRepository _configrepository;
    private readonly IConfigurationServicesInfoMapper _configmapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FindAdminCommand(
      IBaseFindFilterValidator baseFindValidator,
      IServiceConfigurationRepository configrepository,
      IConfigurationServicesInfoMapper configmapper, 
      IHttpContextAccessor httpContextAccessor)
    {
      _baseFindValidator = baseFindValidator;
      _configrepository = configrepository;
      _configmapper = configmapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<FindResultResponse<ConfigurationServicesInfo>> ExecuteAsync(FindAdminFilter filter)
    {
      if(!_baseFindValidator.ValidateCustom(filter, out List<string> errors))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return new()
        {
          Status = OperationResultStatusType.Failed,
          Errors = errors
        };
      }

      List<DbServiceConfiguration> dbConfig = null; 
      FindResultResponse<ConfigurationServicesInfo> response = new();
    //  response.Body = new();

      (List<DbServiceConfiguration> dbConfig, int TotalCount) findServicesResponse =
        await _configrepository.FindAsync(filter);

      dbConfig = findServicesResponse.dbConfig;
      response.TotalCount = findServicesResponse.TotalCount;
      
      response.Body.AddRange(dbConfig.Select(dbConfig => _configmapper.Map(dbConfig)));

      response.Status = response.Errors.Any() 
        ? OperationResultStatusType.PartialSuccess 
        : OperationResultStatusType.FullSuccess;

      return response;
    }   
  }
}

