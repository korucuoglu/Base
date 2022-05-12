using Base.Api.Application.Dtos.Categories;
using FluentValidation;

namespace Base.Api.Application.Validations.Categories;

public class AddCategoryDtoValidator : AbstractValidator<AddCategoryDto>
{
    public AddCategoryDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
    }
}