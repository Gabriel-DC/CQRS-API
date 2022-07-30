using CqrsApi.Domain.Context.Entities.Generic;
using CqrsApi.Domain.Context.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CqrsApi.Domain.Context.Entities.Delivery;

namespace CqrsApi.Domain.Context.Entities
{
    public class Delivery : EntityValidator<DeliveryValidator>
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.Waiting;

            Validate();
        }
        
        public DateTime CreateDate { get; private set; }

        public DateTime EstimatedDeliveryDate { get; private set; }

        public EDeliveryStatus Status { get; private set; }

        public void Ship() => Status = EDeliveryStatus.Shipped;

        public void Cancel()
        {
            if(!Status.Equals(EDeliveryStatus.Delivered))
                Status = EDeliveryStatus.Canceled;
        }

        public class DeliveryValidator : AbstractValidator<Delivery>
        {
            public DeliveryValidator()
            {
                RuleFor(r => r.EstimatedDeliveryDate.Date)
                    .GreaterThanOrEqualTo(r => r.CreateDate.Date)
                    .WithMessage("A data estimada de entrega deve ser maior ou igual a data de criação");
            }
        }
    }
}