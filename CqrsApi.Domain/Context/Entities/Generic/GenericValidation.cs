using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Entities.Generic
{
    public class GenericValidation : AbstractValidator<object>
    {        
        public GenericValidation()
        {
            
        }        

        public ValidationResult ValidateRules() => Validate(new object ());

        public ValidationResult ValidateRuleSets(params string[] ruleSets)        
            => this.Validate(new object(), options => options.IncludeRuleSets(ruleSets));                   
    }
}
