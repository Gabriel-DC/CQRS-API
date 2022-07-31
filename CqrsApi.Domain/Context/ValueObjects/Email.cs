using CqrsApi.Domain.Context.Entities.Generic;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CqrsApi.Domain.Context.ValueObjects.Email;

namespace CqrsApi.Domain.Context.ValueObjects
{
    public class Email : GenericValidator<EmailValidator>
    {
        public Email(string address)
        {
            Address = address;

            Validate();
        }

        public string Address { get; private set; }

        public override string ToString() => Address;
        
        public class EmailValidator : AbstractValidator<Email>
        {
            public EmailValidator()
            {
                RuleFor(x => x.Address)
                    .EmailAddress()
                    .WithMessage("Email inválido");
            }
        }    
    }
}
