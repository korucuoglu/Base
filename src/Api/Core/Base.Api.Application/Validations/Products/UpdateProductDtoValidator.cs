using FluentValidation;
using Base.Api.Application.Dtos.Products;

namespace Base.Api.Application.Validations.Products;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.ShortName).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Image).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
        RuleFor(x => x.Url).NotNull().NotEmpty().WithMessage("{PropertyName} alanı boş olamaz");
    }
}