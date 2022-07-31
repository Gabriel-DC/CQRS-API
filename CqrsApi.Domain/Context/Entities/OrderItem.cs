using CqrsApi.Domain.Context.Entities.Generic;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Entities
{
    public class OrderItem : EntityValidator<OrderItemValidator>
    {
        public OrderItem(Product product, decimal quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = product?.Price ?? 0;

            product?.DecreaseQuantity(quantity);

            Validate();
        }

        public Product Product { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
    
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(e => e.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantidade deve ser maior que 0");
            
            RuleFor(e => e.Price)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que 0");
            
            RuleFor(e => e.Product)
                .NotNull()
                .WithMessage("Produto obrigatório")
                .SetValidator(new Product.ProductValidator());

            When(e => e.Product != null, () =>
            {
                RuleFor(e => e.Product.QuantityOnHand)
                    .GreaterThanOrEqualTo(e => e.Quantity)
                    .WithMessage(obj => $@"Estoque insuficiente para o produto {obj.Product.Title ?? "informado"}.
Estoque Atual: {obj.Product.QuantityOnHand}
Pedido: {obj.Quantity}");
            });        
        } 
    }
}