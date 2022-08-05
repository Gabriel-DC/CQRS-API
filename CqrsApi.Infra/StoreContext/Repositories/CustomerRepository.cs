using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Queries;
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

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document) => _context.Connection
                .QueryFirstOrDefault<CustomerOrdersCountResult>(
                    "GetCustomerOrdersCount",
                    new { Document = document },
                    commandType: CommandType.StoredProcedure);

        public bool Save(Customer customer)
        {
            try
            {
                using var transaction = _context.Connection.BeginTransaction();
                var result = _context.Connection.Execute(
                    @"Insert into Customers (Name, LastName, Document, Email, Phone) 
                values (@Name, @LastName, @Document, @Email, @Phone)",
                    new
                    {
                        Name = customer.Name.FirstName,
                        customer.Name.LastName,
                        Document = customer.Document.Number,
                        Email = customer.Email.Address,
                        customer.Phone
                    });

                foreach (var address in customer.GetAddresses())
                {
                    result += _context.Connection.Execute(
                        @"Insert into Addresses (CustomerId, Type, Street, Number, Complement, District, City, State, Country, ZipCode) 
                    values (@CustomerId, @Type, @Street, @Number, @Complement, @District, @City, @State, @Country, @ZipCode)",
                        new
                        {
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
                        });
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
