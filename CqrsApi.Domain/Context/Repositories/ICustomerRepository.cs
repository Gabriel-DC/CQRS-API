using CqrsApi.Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Repositories
{
    public interface ICustomerRepository
    {
        bool CheckDocumentExists(string document);
        bool CheckEmailExists(string email);
        bool Save(Customer customer);
    }
}
