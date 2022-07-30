using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Entities.Generic
{
    public abstract class GenericValidator<TypeValidator>
        where TypeValidator : new()
    {
        private readonly TypeValidator? _validator;

        protected GenericValidator()
        {
            if (typeof(object) != typeof(TypeValidator))
                _validator = new TypeValidator();
            
            ValidationResult = new ValidationResult();
        }

        //Adiciona notificacao em validationresult
        public void AddNotification(string propertyName, string errorMessage)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
        }

        private ValidationResult ValidationResult { get; set; }

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

        public void ClearNotifications()
        {
            ValidationResult = new ValidationResult();
        }
    }
}
