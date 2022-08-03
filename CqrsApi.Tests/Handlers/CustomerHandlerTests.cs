using CqrsApi.Tests.Services;
using CqrsApi.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CqrsApi.Domain.Context.Commands.CustomerCommands.Outputs;

namespace CqrsApi.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        private readonly ICustomerRepository _customerRepository = new FakeCustomerRepository();
        private readonly IEmailService _emailService = new FakeEmailService();

        [TestMethod]
        [TestCategory("Handlers")]
        public void ShouldCreateCustomerWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Gabriel",
                LastName = "Almeida",
                Document = "87766408090",
                Email = "gabriel@almeida.com",
                Phone = "31986540305"
            };

            var handler = new CreateCustomerHandler(_customerRepository, _emailService);

            var result = handler.Handle(command) as CreateCustomerModel;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionWhenCommandIsInvalid()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Gabriel",
                LastName = "Almeida",
                Document = "",
                Email = "gabriel@almeida.com",
                Phone = "31986540305"
            };

            var handler = new CreateCustomerHandler(_customerRepository, _emailService);

            handler.Handle(command);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionWhenEmailIsAlreadyTaken()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Gabriel",
                LastName = "Almeida",
                Document = "87766408090",
                Email = "gabriel@gabriel.com",
                Phone = "31986540305"
            };

            var handler = new CreateCustomerHandler(_customerRepository, _emailService);

            handler.Handle(command);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionWhenDocumentIsAlreadyTaken()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Gabriel",
                LastName = "Almeida",
                Document = "72236799055",
                Email = "gabriel@gabriel.com",
                Phone = "31986540305"
            };

            var handler = new CreateCustomerHandler(_customerRepository, _emailService);

            handler.Handle(command);
        }
    }
}