using FluentValidation;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.AdminService.Validation.Interfaces;

namespace LT.DigitalOffice.AdminService.Validation
{
  public class InstallAppRequestValidator : AbstractValidator<InstallAppRequest>, IInstallAppRequestValidator
  {
    public InstallAppRequestValidator()
    {
      RuleFor(request => request.AdminInfo)
        .NotNull()
        .WithMessage("Admin information can't be null.");

      RuleFor(request => request.SmtpInfo)
        .NotNull()
        .WithMessage("Smtp information can't be null.");

      RuleFor(request => request.ServicesToDisable)
        .NotEmpty()
        .WithMessage("Information about services to disable can't be empty.");
    }
  }
}
