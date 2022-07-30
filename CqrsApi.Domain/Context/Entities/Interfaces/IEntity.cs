using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Entities.Interfaces
{
    public interface IEntity
    {
        IReadOnlyCollection<ValidationFailure> Notifications { get; }
        bool IsValid { get; }
    }
}
