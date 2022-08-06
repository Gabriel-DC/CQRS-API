using CqrsApi.Domain.Context.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Infra.StoreContext.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string body)
        {            
            //throw new NotImplementedException();
        }
    }
}
