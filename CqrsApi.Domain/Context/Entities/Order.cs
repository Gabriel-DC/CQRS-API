using CqrsApi.Domain.Context.Entities.Generic;
using CqrsApi.Domain.Context.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using static CqrsApi.Domain.Context.Entities.Order;

namespace CqrsApi.Domain.Context.Entities
{
    public class Order : EntityValidator<OrderValidator>
    {
        private readonly IList<OrderItem> _items;
        private readonly IList<Delivery> _deliveries;
        
        public Order(Customer customer)
        {
            Customer = customer;
            Createdate = DateTime.Now;
            Status = EOrderStatus.Created;
            _items = new List<OrderItem>();
            _deliveries = new List<Delivery>();
        }
        
        public Customer Customer { get; private set; }
        public string Number { get; private set; }
        public DateTime Createdate { get; private set; }
        public EOrderStatus Status { get; private set; }
        
        public IReadOnlyCollection<OrderItem> GetItems() => _items.ToArray();
        public IReadOnlyCollection<Delivery> GetDeliveries() => _deliveries.ToArray();

        public void AddItem(OrderItem item)
        {
            item.Validate();
            _items.Add(item);
        }

        public void AddItem(Product product, decimal quantity)
        {
            var item = new OrderItem(product, quantity);
            item.Validate();
            _items.Add(item);
        }

        public void AddDelivery(Delivery delivery)
        {
            _deliveries.Add(delivery);
        }

        public void Place()
        {
            Number = Guid.NewGuid().ToString("N")[..8].ToUpper();

            Validate();
        }

        public void Pay(decimal amount)
        {
            if (ValidatePayment(amount).IsValid)
                Status = EOrderStatus.Paid;
        }

        public void Ship()
        {
            var count = 1;
            var deliveries = new List<Delivery>() { new Delivery(DateTime.Now.AddDays(5)) };
            
            foreach (var item in _items)
            {
                if(count == 5)
                {
                    count = 1;
                    deliveries.Add(new Delivery(DateTime.Now.AddDays(5)));
                }
                
                count++;
            }

            deliveries.ForEach(d => d.Ship());
            deliveries.ForEach(d => _deliveries.Add(d));
        }
        
        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
            _deliveries.ToList().ForEach(d => d.Cancel());
        }

        public decimal TotalValue() => _items.Sum(x => x.Price);

        public ValidationResult ValidatePayment(decimal payment)
            => new OrderValidator(payment).Validate(this, options => options.IncludeRuleSets("Payment"));        

        public class OrderValidator : AbstractValidator<Order>
        {
            public OrderValidator()
            {
                RuleFor(r => r._items)
                    .NotEmpty()
                    .WithMessage("O pedido não possui itens")
                    .ForEach(c => c.SetValidator(new OrderItemValidator()));
            }

            public OrderValidator(decimal amount)
            {
                RuleSet("Payment", () =>
                {
                    RuleFor(r => r.Status)
                        .Equal(EOrderStatus.Created)
                        .WithMessage("O pedido não está em status de criação");

                    RuleFor(r => r.TotalValue())
                        .GreaterThan(0)
                        .WithMessage("O valor total do pedido deve ser maior que zero");

                    RuleFor(r => amount)
                        .GreaterThan(0)
                        .LessThanOrEqualTo(r => r.TotalValue())
                        .Configure(rule => rule.MessageBuilder = _ => "O valor pago deve ser menor ou igual ao valor total do pedido");
                });
            }
        }
    }    
}