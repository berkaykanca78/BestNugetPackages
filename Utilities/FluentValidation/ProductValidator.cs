using BaseWebApp.Models;
using FluentValidation;

namespace BaseWebApp.Utilities.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ürün adı boş geçilemez!")
                .MinimumLength(3)
                .WithMessage("Ürün adı minimum 3 karakter olmalıdır!");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Fiyat bilgisi boş geçilemez!")
                .GreaterThan(0)
                .WithMessage("Fiyat bilgisi sıfırdan farklı olmalıdır!");
        }
    }
}
