using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs
{
    public class CreateCustomerCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
        {
            public CreateCustomerCommandValidator()
            {
                RuleFor(r => r.FirstName)
                    .NotEmpty()
                    .WithMessage("O nome não pode ser vazio");

                RuleFor(r => r.LastName)
                    .NotEmpty()
                    .WithMessage("O sobrenome não pode ser vazio");

                RuleFor(r => r.Document)
                    .NotEmpty()
                    .WithMessage("O documento não pode ser vazio");

                RuleFor(r => r.Email)
                    .NotEmpty()
                    .WithMessage("O email não pode ser vazio")
                    .EmailAddress()
                    .WithMessage("O email não é válido");
            }
        }
    }
}
