﻿using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Mappers.Interfaces
{
  [AutoInject]
  public interface IEndpointInfoMapper
  {
    public EndpointInfo Map(DbServiceEndpoint endpoint);
  }
}