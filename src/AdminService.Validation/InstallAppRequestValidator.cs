using System.Text.RegularExpressions;
using FluentValidation;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.AdminService.Validation.Interfaces;
using System.Linq;

namespace LT.DigitalOffice.AdminService.Validation
{
  public class InstallAppRequestValidator : AbstractValidator<InstallAppRequest>, IInstallAppRequestValidator
  {
    private static Regex NumberRegex = new(@"\d");
    private static Regex SpecialCharactersRegex = new(@"[$&+,:;=?@#|<>.^*()%!]");
    private static Regex NameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+)$");
    private static Regex PasswordRegex = new(@"(?=.*[.,:;?!*+%\-<>@[\]{}/\\_{}$#])");

    public InstallAppRequestValidator()
    {
      RuleFor(request => request.AdminInfo)
        .NotNull().WithMessage("Admin information can't be null.");

      #region AdminInfo

      RuleFor(request => request.AdminInfo.FirstName)
        .NotEmpty().WithMessage("First name cannot be empty.")
        .Must(x => !NumberRegex.IsMatch(x))
        .WithMessage("First name must not contain numbers.")
        .Must(x => !SpecialCharactersRegex.IsMatch(x))
        .WithMessage("First name must not contain special characters.")
        .MaximumLength(45).WithMessage("First name is too long.")
        .Must(x => NameRegex.IsMatch(x.Trim()))
        .WithMessage("First name contains invalid characters.");

      RuleFor(request => request.AdminInfo.LastName)
        .NotEmpty().WithMessage("First name cannot be empty.")
        .Must(x => !NumberRegex.IsMatch(x))
        .WithMessage("First name must not contain numbers.")
        .Must(x => !SpecialCharactersRegex.IsMatch(x))
        .WithMessage("First name must not contain special characters.")
        .MaximumLength(45).WithMessage("First name is too long.")
        .Must(x => NameRegex.IsMatch(x.Trim()))
        .WithMessage("First name contains invalid characters.");

      When(
        request => !string.IsNullOrEmpty(request.AdminInfo.MiddleName),
        () =>
          RuleFor(request => request.AdminInfo.MiddleName)
            .Must(x => !NumberRegex.IsMatch(x))
            .WithMessage("Middle name must not contain numbers.")
            .Must(x => !SpecialCharactersRegex.IsMatch(x))
            .WithMessage("Middle name must not contain special characters.")
            .MaximumLength(45).WithMessage("Middle name is too long.")
            .Must(x => NameRegex.IsMatch(x.Trim()))
            .WithMessage("Middle name contains invalid characters."));

      When(
        request => !string.IsNullOrEmpty(request.AdminInfo.Password),
        () =>
          RuleFor(request => request.AdminInfo.Password)
            .MinimumLength(8).WithMessage("Password is too short.")
            .MaximumLength(14).WithMessage("Password is too long.")
            .Must(x => PasswordRegex.IsMatch(x))
            .WithMessage("The password must contain at least one special character.")
            .Must(x => !x.Contains(' ')).WithMessage("Password must not contain space."));

      When(
        request => !string.IsNullOrEmpty(request.AdminInfo.Login),
        () =>
          RuleFor(request => request.AdminInfo.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login can't be empty.")
            .Must(x => char.IsLetter(x[0])).WithMessage("Login must start with a letter.")
            .MinimumLength(3).WithMessage("Login is too short.")
            .MaximumLength(15).WithMessage("Login is too long")
            .Must(x => x.All(char.IsLetterOrDigit))
            .WithMessage("Login must contain only letters or digits."));

      RuleFor(request => request.AdminInfo.Email)
        .NotEmpty().WithMessage("Email can't be empty.");

      #endregion

      RuleFor(request => request.SmtpInfo)
        .NotNull().WithMessage("Smtp information can't be null.");

      #region SmtpInfo

      RuleFor(request => request.SmtpInfo.Host)
        .NotEmpty().WithMessage("Host can't be empty.");

      RuleFor(request => request.SmtpInfo.Port)
        .NotNull().WithMessage("Port can't be null.");

      RuleFor(request => request.SmtpInfo.EnableSsl)
        .NotNull().WithMessage("Smtp property EnableSsl can't be null.");

      RuleFor(request => request.SmtpInfo.Email)
        .NotEmpty().WithMessage("Email can't be empty.");

      RuleFor(request => request.SmtpInfo.Password)
        .NotEmpty().WithMessage("Password can't be empty.");
      #endregion

      RuleFor(request => request.WorkDaysApiUrl)
        .NotEmpty()
        .WithMessage("Work days api url can't be empty");

      RuleFor(request => request.ServicesToDisable)
        .Cascade(CascadeMode.Stop)
        .MustAsync
    }
  }
}
