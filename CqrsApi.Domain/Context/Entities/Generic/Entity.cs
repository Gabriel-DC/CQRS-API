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
    // CLASSE DESCARTADA TEMPORARIAMENTE
    public abstract class Entity<TypeValidator> : GenericValidator<TypeValidator>
        where TypeValidator : new()
    {
        protected Entity()
        {

        }

        public ValidationResult Validate<T>()
        {
            var typeArgument = GetType();
            var genericType = typeof(AbstractValidator<>);

            var validatorType = genericType.MakeGenericType(typeArgument)!;

            var myMethod = validatorType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name == "Validate")!;

            return (ValidationResult)myMethod!.Invoke(Activator.CreateInstance(typeof(T)), new object[] { this })!;
        }
    }
}
