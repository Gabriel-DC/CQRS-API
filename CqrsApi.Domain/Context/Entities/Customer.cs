using CqrsApi.Domain.Context.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Entities
{
    public class Customer //: EntityValidator<CustomerValidator>
    {
        private readonly IList<Address> _addresses;

        public Customer(
            Name name,
            Document document,
            Email email,
            string phone)
        {
            Name = name;
            Document = document;
            Email = email;
            Phone = phone;
            _addresses = new List<Address>();
        }

        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }

        public IReadOnlyCollection<Address> GetAddresses() => _addresses.ToArray();

        public void AddAddress(Address address)
        {
            //valida
            //adiciona
        }

        public override string ToString() => Name.ToString();
    }
}