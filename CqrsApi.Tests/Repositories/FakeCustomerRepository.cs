using CqrsApi.Domain.Context.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {        
        public IQueryable<Customer> Customers { get; set; } = new List<Customer>()
        {
            new Customer(
                        new Name("John", "Doe"),
                        new Document("72236799055"),
                        new Email("john@doe.com"),
                        "123456789"
                        ),
            new Customer(
                        new Name("Gabriel", "Almeida"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        ),
            new Customer(
                        new Name("Jeciani", "Gomes"),
                        new Document("36889058062"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        ),
            new Customer(
                        new Name("Joazim", "Serrano"),
                        new Document("85249570003"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
        }.AsQueryable();

        public bool CheckDocumentExists(string document) => Customers.Any(c => c.Document.Number == document);

        public bool CheckEmailExists(string email) => Customers.Any(c => c.Email.Address == email);

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            throw new NotImplementedException();
        }

        public bool Save(Customer customer)
        {
            if(customer.Validate().IsValid)
            {
                Customers = Customers.Concat(new List<Customer>() { customer });
                return true;
            }

            return false;
                
        }
    }
}
