using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.User;

namespace LT.DigitalOffice.AdminService.Models.Dto.Configuration
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    public string UpdateSmtpCredentialsEndpoint { get; set; }

    [AutoInjectRequest(typeof(ICreateAdminRequest))]
    public string CreateAdminEndpoint { get; set; }
  }
}

