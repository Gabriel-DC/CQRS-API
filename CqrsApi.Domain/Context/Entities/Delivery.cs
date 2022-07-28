using CqrsApi.Domain.Context.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Entities
{
    public class Delivery //: EntityValidator<DeliveryValidator>
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.Waiting;
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
    }
}