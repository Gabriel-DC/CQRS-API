using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Queries.CustomerQueries.CommonModels
{
    public class CommonCustomer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }       
    }
}
