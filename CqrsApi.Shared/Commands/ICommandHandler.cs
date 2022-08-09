using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Shared.Commands
{
    public interface ICommandHandler<T, T2> where T : ICommand
    {
        T2 Handle(T command);
    }
}
