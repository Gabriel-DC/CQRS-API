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
                    .MinimumLength(3)
                    .WithMessage("Nome deve conter no mínimo 3 caracteres")
                    .MaximumLength(100)
                    .WithName("Nome deve conter no máximo 100 caracteres");                
                
                RuleFor(x => x.LastName)
                    .NotEmpty()
                    .WithMessage("Sobrenome inválido")
                    .MinimumLength(3)
                    .WithMessage("Sobrenome deve conter no mínimo 3 caracteres")
                    .MaximumLength(100)
                    .WithName("Sobrenome deve conter no máximo 100 caracteres");
            }
        }
    }
}
