using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Commands.OrderCommands.Inputs
{
    public class PlaceOrderCommand
    {
        public Guid CustomerId { get; set; }
        //public List<OrderItemCommand> Items { get; set; }
    }
}
