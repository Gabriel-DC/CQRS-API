using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Queries.CustomerQueries;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Infra.StoreContext.DataContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Infra.StoreContext.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreDbContext _context;

        public CustomerRepository(StoreDbContext context)
        {
            _context = context;
        }

        public bool CheckDocumentExists(string document) => _context.Connection
                .QueryFirstOrDefault<bool>(
                    "spCheckDocumentExists",
                    new { Document = document },
                    commandType: CommandType.StoredProcedure);

        public bool CheckEmailExists(string email) => _context.Connection
                .QueryFirstOrDefault<bool>(
                    "spCheckEmailExists",
                    new { Email = email },
                    commandType: CommandType.StoredProcedure);

        public void Delete(Guid id) => throw new NotImplementedException();

        public IEnumerable<IndexCustomersQuery> GetAll() => _context.Connection
                .Query<IndexCustomersQuery>(
                    "select id, concat(FirstName, ' ', LastName) as [Name], [Document], [Email] from Customer");

        public GetCustomerQuery GetCustomerByDocument(string document) => _context.Connection
                .QueryFirstOrDefault<GetCustomerQuery>(
                        @"select id, concat(FirstName, ' ', LastName) as [Name], [Document], [Email] from Customer c
where c.Document = @Document",
                        new { Document = document });

        public GetCustomerQuery GetCustomerById(Guid id) => _context.Connection
                .QueryFirstOrDefault<GetCustomerQuery>(
                        @"select id, concat(FirstName, ' ', LastName) as [Name], [Document], [Email] from Customer c
where c.Id = @Id",
                        new { Id = id });

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document) => _context.Connection
                .QueryFirstOrDefault<CustomerOrdersCountResult>(
                    "GetCustomerOrdersCount",
                    new { Document = document },
                    commandType: CommandType.StoredProcedure);

        public IEnumerable<IndexCustomerOrdersQuery> GetOrders(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Customer customer)
        {
            try
            {
                using var transaction = _context.Connection.BeginTransaction();
                var result = _context.Connection.Execute(
                    @"Insert into Customer (Id, FirstName, LastName, Document, Email, Phone) 
                values (@Id, @FirstName, @LastName, @Document, @Email, @Phone)",
                    new
                    {
                        customer.Id,
                        customer.Name.FirstName,
                        customer.Name.LastName,
                        Document = customer.Document.Number,
                        Email = customer.Email.Address,
                        customer.Phone
                    }, transaction);

                foreach (var address in customer.GetAddresses())
                {
                    result += _context.Connection.Execute(
                        @"Insert into Address (Id, CustomerId, Type, Street, Number, Complement, District, City, State, Country, ZipCode) 
                    values (@Id, @CustomerId, @Type, @Street, @Number, @Complement, @District, @City, @State, @Country, @ZipCode)",
                        new
                        {
                            address.Id,
                            CustomerId = customer.Id,
                            address.Type,
                            address.Street,
                            address.Number,
                            address.Complement,
                            address.District,
                            address.City,
                            address.State,
                            address.Country,
                            address.ZipCode
                        }, transaction);
                }

                transaction.Commit();
                return true;
            }
            catch
            {                
                return false;
            }
        }

        public GetCustomerQuery UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
