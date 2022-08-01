using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Shared.Commands
{
    public interface ICommand
    {
        ValidationResult Validate();
    }
}
