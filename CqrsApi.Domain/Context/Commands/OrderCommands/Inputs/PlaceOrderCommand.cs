using CqrsApi.Shared.Commands;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CqrsApi.Domain.Context.Commands.OrderCommands.Inputs.OrderItemCommand;

namespace CqrsApi.Domain.Context.Commands.OrderCommands.Inputs
{
    public class PlaceOrderCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public IList<OrderItemCommand> OrderItems { get; set; }

        public ValidationResult Validate() => new PlaceOrderCommandValidator().Validate(this);

        public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
        {
            public PlaceOrderCommandValidator()
            {
                RuleFor(r => r.CustomerId)
                    .NotEmpty()
                    .WithMessage("Cliente inválido");

                RuleFor(r => r.OrderItems)
                    .NotEmpty()
                    .WithMessage("O Pedido não possui Itens")
                    .ForEach(c => c.SetValidator(new OrderItemCommandValidator()));                    
            }
        }
    }
    
    public class OrderItemCommand
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }

        public class OrderItemCommandValidator : AbstractValidator<OrderItemCommand>
        {
            public OrderItemCommandValidator()
            {
                RuleFor(r => r.ProductId)
                    .NotEmpty()
                    .WithMessage("Produto inválido");
                
                RuleFor(r => r.Quantity)
                    .GreaterThan(0)
                    .WithMessage("A quantidade deve ser maior que zero");
            }
        }
    }
}
