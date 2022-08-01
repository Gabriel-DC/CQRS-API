using CqrsApi.Domain.Context.Entities.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Entities.Generic
{
    public abstract class EntityValidator<TypeValidator> : IEntity
        where TypeValidator : new()
    {
        private readonly TypeValidator? _validator;

        protected EntityValidator()
        {
            if (typeof(object) != typeof(TypeValidator))
                _validator = new TypeValidator();

            ValidationResult = new ValidationResult();
        }

        protected ValidationResult ValidationResult { get; set; }

        public IReadOnlyCollection<ValidationFailure> Notifications => ValidationResult.Errors;

        public bool IsValid => ValidationResult.IsValid;

        public ValidationResult Validate()
        {
            var validatorType = typeof(AbstractValidator<>).MakeGenericType(GetType())!;

            var myMethod = validatorType
                .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name == "Validate");

            if (myMethod is not null && _validator is not null)
                ValidationResult = (ValidationResult)myMethod.Invoke(_validator, new object[] { this })!;
            else
                ValidationResult = new ValidationResult();

            return ValidationResult;
        }

        public void ClearNotifications() => ValidationResult = new ValidationResult();
    }
}
