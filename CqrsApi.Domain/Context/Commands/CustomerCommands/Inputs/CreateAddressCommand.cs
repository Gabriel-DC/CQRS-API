using CqrsApi.Domain.Context.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs
{
    public class CreateAddressCommand
    {
        public Guid CustomerId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public EAddressType Type { get; set; }

        public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
        {
            public CreateAddressCommandValidator()
            {
                RuleFor(r => r.Street)
                    .NotEmpty()
                    .WithMessage("A rua não pode ser vazia");

                RuleFor(r => r.CustomerId)
                    .NotEmpty()
                    .WithMessage("Id do cliente obrigatório");

                RuleFor(r => r.Type)
                    .NotEmpty()
                    .WithMessage("Tipo de endereço obrigatório");

                RuleFor(r => r.Number)
                    .NotEmpty()
                    .WithMessage("O número não pode ser vazio");

                RuleFor(r => r.City)
                    .NotEmpty()
                    .WithMessage("A cidade não pode ser vazia");

                RuleFor(r => r.State)
                    .NotEmpty()
                    .WithMessage("O estado não pode ser vazio");

                RuleFor(r => r.Country)
                    .NotEmpty()
                    .WithMessage("O país não pode ser vazio");

                RuleFor(r => r.ZipCode)
                    .NotEmpty()
                    .WithMessage("O CEP não pode ser vazio");
            }
        }
    }
}
