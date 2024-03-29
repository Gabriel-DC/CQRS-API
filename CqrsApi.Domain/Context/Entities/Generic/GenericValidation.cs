using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CqrsApi.Domain.Context.Entities.Generic
{
    public class GenericValidation : AbstractValidator<object>
    {
        public GenericValidation()
        {
        }

        public GenericValidation AddRule<T>(T input, Action<IRuleBuilderInitial<object, T>> action)
        {
            action(RuleFor(x => input));

            return this;
        }

        public ValidationResult ValidateRules()
            => Validate(new object());

        public ValidationResult ValidateRuleSets(params string[] ruleSets)
            => this.Validate(
                new object(),
                options => options.IncludeRuleSets(ruleSets));

        public ValidationResult ValidateProperties(params string[] properties)
            => this.Validate(
                new object(),
                options => options.IncludeProperties(properties));
    }
}