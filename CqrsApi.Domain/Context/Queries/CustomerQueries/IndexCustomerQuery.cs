using CqrsApi.Domain.Context.Queries.CustomerQueries.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Queries.CustomerQueries
{
    public class IndexCustomersQuery : CommonCustomer
    {
        public string Email { get; set; }
    }
}
