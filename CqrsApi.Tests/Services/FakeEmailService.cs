using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Tests.Services
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string subject, string body) => _ = "";
    }
}
