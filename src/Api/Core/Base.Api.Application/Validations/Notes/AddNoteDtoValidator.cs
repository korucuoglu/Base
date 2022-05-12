using Base.Api.Application.Dtos.Notes;
using FluentValidation;

namespace Base.Api.Application.Validations.Notes;

public class AddNoteDtoValidator : AbstractValidator<AddNoteDto>
{
    public AddNoteDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
    }
}