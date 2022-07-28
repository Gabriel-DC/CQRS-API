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

            Notifications = new ValidationResult();
        }

        protected ValidationResult Notifications { get; set; }

        public bool IsValid => Notifications.IsValid;

        public ValidationResult Validate()
        {
            var validatorType = typeof(AbstractValidator<>).MakeGenericType(GetType())!;

            var myMethod = validatorType
                .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name == "Validate");

            if (myMethod is not null && _validator is not null)
                Notifications = (ValidationResult)myMethod.Invoke(_validator, new object[] { this })!;
            else
                Notifications = new ValidationResult();

            return Notifications;
        }

        public void ClearNotifications()
        {
            Notifications = new ValidationResult();
        }
    }
}
