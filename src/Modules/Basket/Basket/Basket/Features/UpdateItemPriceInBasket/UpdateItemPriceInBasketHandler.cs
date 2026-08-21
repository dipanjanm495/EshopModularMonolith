using Basket.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Basket.Features.UpdateItemPriceInBasket
{
    public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal Price) : ICommand<UpdateItemPriceInBasketResult>;

    public record UpdateItemPriceInBasketResult(bool Success);

    public class UpdateItemPriceInBasketCommandValidator : AbstractValidator<UpdateItemPriceInBasketCommand>
    {
        public UpdateItemPriceInBasketCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("BasketId is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("ItemId is required.");
        }
    }
    public class UpdateItemPriceInBasketHandler(BasketDbContext dbContext) : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
    {
        public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command, CancellationToken cancellationToken)
        {
            var itemsToUpdate =await dbContext.ShoppingCartItems.Where(i => i.ProductId == command.ProductId).ToListAsync(cancellationToken);
            if(itemsToUpdate == null || !itemsToUpdate.Any())
            {
                return new UpdateItemPriceInBasketResult(false);
            }
            foreach(var item in itemsToUpdate)
            {
                item.UpdatePrice(command.Price);
            }
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateItemPriceInBasketResult(true);
        }
    }
}
