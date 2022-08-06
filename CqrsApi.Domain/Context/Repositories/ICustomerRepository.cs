using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Queries.CustomerQueries;
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
        CustomerOrdersCountResult GetCustomerOrdersCount(string document);

        GetCustomerQuery GetCustomerById(Guid id);

        GetCustomerQuery GetCustomerByDocument(string document);

        IEnumerable<IndexCustomersQuery> GetAll();

        IEnumerable<IndexCustomerOrdersQuery> GetOrders(Guid id);

        GetCustomerQuery UpdateCustomer(Customer customer);

        void Delete(Guid id);
    }
}
