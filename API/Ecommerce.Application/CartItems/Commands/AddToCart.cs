using Ecommerce.Application.CartItems.DTOs;
using Ecommerce.Application.Core;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain;
using Ecommerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.CartItems.Commands;

public static class AddToCart
{
    public class Command : IRequest<Result<Unit>>
    {
        public required AddToCartDto ItemDto { get; set; }
    }

    public class Handler(AppDbContext dbContext, IUserAccessor userAccessor)
        : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserAsync();

            var product = await dbContext.Products.FirstOrDefaultAsync(
                p => p.Id == request.ItemDto.ProductId,
                cancellationToken
            );

            if (product == null)
                return Result<Unit>.Failure("Product not found", 404);

            if (product.Quantity < request.ItemDto.Quantity)
                return Result<Unit>.Failure("Not enough product in stock", 400);

            var cartItem = await dbContext.CartItems.FirstOrDefaultAsync(
                ci => ci.ProductId == request.ItemDto.ProductId && ci.UserId == user.Id,
                cancellationToken
            );

            if (cartItem != null)
            {
                cartItem.Quantity += request.ItemDto.Quantity;
                if (cartItem.Quantity > product.Quantity)
                {
                    cartItem.Quantity = product.Quantity;
                }
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = request.ItemDto.ProductId,
                    UserId = user.Id,
                    Quantity = request.ItemDto.Quantity,
                };
                dbContext.CartItems.Add(cartItem);
            }

            var result = await dbContext.SaveChangesAsync(cancellationToken) > 0;

            return result
                ? Result<Unit>.Success(Unit.Value)
                : Result<Unit>.Failure("Failed to add item to cart", 400);
        }
    }
}
