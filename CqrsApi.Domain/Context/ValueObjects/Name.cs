using CqrsApi.Domain.Context.Entities.Generic;
using FluentValidation;
using static CqrsApi.Domain.Context.ValueObjects.Name;

namespace CqrsApi.Domain.Context.ValueObjects
{
    public class Name : GenericValidator<NameValidator>
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            Validate();
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public override string ToString() => $"{FirstName} {LastName}";

        public class NameValidator : AbstractValidator<Name>
        {
            public NameValidator()
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .WithMessage("Nome inválido")
                    .Length(3, 100)
                    .WithName("Nome deve conter entre 3 e 100 caracteres");
                
                RuleFor(x => x.LastName)
                    .NotEmpty()
                    .WithMessage("Sobrenome inválido")
                    .Length(3, 100)
                    .WithName("Sobrenome deve conter entre 3 e 100 caracteres");
            }
        }
    }
}
