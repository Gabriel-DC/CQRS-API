using CqrsApi.Domain.Context.Entities.Generic;
using CqrsApi.Domain.Context.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static CqrsApi.Domain.Context.Entities.Customer;

namespace CqrsApi.Domain.Context.Entities
{
    public class Customer : EntityValidator<CustomerValidator>
    {
        private readonly IList<Address> _addresses;

        public Customer(
            Name name,
            Document document,
            Email email,
            string phone)
        {
            Name = name;
            Document = document;
            Email = email;
            Phone = phone;
            _addresses = new List<Address>();

            Validate();
        }

        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }

        public IReadOnlyCollection<Address> GetAddresses() => _addresses.ToArray();

        public void AddAddress(Address address)
        {
            if (address is not null)            
                _addresses.Add(address);
        }

        public override string ToString() => Name.ToString();

        public class CustomerValidator : AbstractValidator<Customer>
        {
            public CustomerValidator()
            {
                RuleFor(x => x.Name)
                    .NotNull()
                    .WithMessage("Nome inválido")
                    .SetValidator(new Name.NameValidator());
                
                RuleFor(x => x.Document)
                    .NotNull()
                    .WithMessage("Documento inválido")
                    .SetValidator(new Document.DocumentValidator());
                
                RuleFor(x => x.Email)
                    .NotNull()
                    .WithMessage("Email inválido")
                    .SetValidator(new Email.EmailValidator());
            }
        }
    }    
}