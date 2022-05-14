using FluentValidation;

namespace Base.Api.Application.Features.Notes;

public class AddNoteRequestValidator : AbstractValidator<AddNoteRequest>
{
    public AddNoteRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
    }
}