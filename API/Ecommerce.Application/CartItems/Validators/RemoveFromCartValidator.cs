using Ecommerce.Application.CartItems.Commands;
using FluentValidation;

namespace Ecommerce.Application.CartItems.Validators;

public class RemoveFromCartValidator : AbstractValidator<RemoveFromCart.Command>
{
    public RemoveFromCartValidator()
    {
        RuleFor(x => x.ItemDto.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}
