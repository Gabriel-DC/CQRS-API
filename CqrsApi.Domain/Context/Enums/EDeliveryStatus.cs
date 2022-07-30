using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Enums
{
    public enum EDeliveryStatus
    {
        Waiting,
        Shipped,
        Delivered,
        Canceled
    }
}
