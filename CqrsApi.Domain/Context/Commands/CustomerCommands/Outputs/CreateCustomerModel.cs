using CqrsApi.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Commands.CustomerCommands.Outputs
{
    public class CreateCustomerModel
    {
        public CreateCustomerModel()
        {
        }
        
        public CreateCustomerModel(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
