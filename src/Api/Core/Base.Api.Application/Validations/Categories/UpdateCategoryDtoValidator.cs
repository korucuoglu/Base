using FluentValidation;
using Base.Api.Application.Dtos.Categories;

namespace Base.Api.Application.Validations.Categories;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
    }
}