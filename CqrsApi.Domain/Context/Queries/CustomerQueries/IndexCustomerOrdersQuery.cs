using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Queries.CustomerQueries
{
    public class IndexCustomerOrdersQuery : IndexCustomersQuery
    {
        public string Number { get; set; }
        public decimal Total { get; set; }
    }
}
