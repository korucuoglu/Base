using Base.Api.Application.Dtos.User;
using FluentValidation;

namespace Base.Api.Application.Validations.User;

public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().NotNull().WithMessage("Güncel şifreniz boş olamaz");
        RuleFor(x => x.NewPassword).NotEmpty().NotNull().WithMessage("Yeni şifreniz boş olamaz");
        RuleFor(x => x.CurrentPassword).NotEqual(x => x.NewPassword).WithMessage("Yeni şifreniz eskisiyle aynı olamaz");
    }
}