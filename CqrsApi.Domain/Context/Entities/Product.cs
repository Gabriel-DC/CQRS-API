using CqrsApi.Domain.Context.Entities.Generic;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CqrsApi.Domain.Context.Entities.Product;

namespace CqrsApi.Domain.Context.Entities
{
    public class Product : EntityValidator<ProductValidator>
    {
        public Product(
            string title,
            string description,
            string image,
            decimal price,
            decimal quantityOnHand)
        {
            Title = title;
            Description = description;
            Image = image;
            Price = price;
            QuantityOnHand = quantityOnHand;

            Validate();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public decimal QuantityOnHand { get; private set; }

        public override string ToString() => $"{Title}";

        public void DecreaseQuantity(decimal quantity)
        {
            //if (QuantityOnHand - quantity < 0)
            //    throw new InvalidOperationException("Não há quantidade suficiente em estoque");

            QuantityOnHand -= quantity;
        }

        public class ProductValidator : AbstractValidator<Product>
        {
            public ProductValidator()
            {
                RuleFor(r => r.Title)
                    .NotEmpty()
                    .WithMessage("Título do produto obrigatório");

                RuleFor(r => r.Price)
                    .GreaterThan(0)
                    .WithMessage("Preço do produto deve ser maior que zero");

                RuleFor(r => r.QuantityOnHand)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Quantidade em estoque deve ser maior ou igual a zero");
            }
        }
    }    
}