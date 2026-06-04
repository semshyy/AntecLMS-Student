using FluentValidation;

namespace AntecLMS.Application.Features.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
  public CreateUserValidator()
  {
    RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    RuleFor(x => x.Surname).NotEmpty().MaximumLength(100);
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password)
      .NotEmpty()
      .MinimumLength(6)
      .WithMessage("Şifrə minimum 6 simvol olmalıdır.");
    RuleFor(x => x.Role)
      .NotEmpty()
      .Must(r => new[] { "admin", "teacher", "student" }.Contains(r.ToLower()))
      .WithMessage("Rol yalnız admin, teacher və ya student ola bilər.");
  }
}
