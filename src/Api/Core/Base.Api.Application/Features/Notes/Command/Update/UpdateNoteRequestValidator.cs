using FluentValidation;

namespace Base.Api.Application.Features.Notes;

public class UpdateNoteRequestValidator : AbstractValidator<UpdateNoteRequest>
{
    public UpdateNoteRequestValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
    }
}