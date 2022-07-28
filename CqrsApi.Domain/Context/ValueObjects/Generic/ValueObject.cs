using CqrsApi.Domain.Context.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.ValueObjects.Generic
{
    public abstract class  ValueObjecty<TypeValidator> : GenericValidator<TypeValidator>
        where TypeValidator : new()
    {
    }
}
