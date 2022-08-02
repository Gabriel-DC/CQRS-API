using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CqrsApi.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        [TestCategory("Commands")]
        public void ShouldBeValidWhenCommandRunsValidation()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Gabriel",
                LastName = "Almeida",
                Document = "87766408090",
                Email = "gabriel@gabriel.com",
                Phone = "31986540305"
            };

            Assert.IsTrue(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void ShouldBeInvalidWhenCommandRunsValidation()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "",
                LastName = "",
                Document = "",
                Email = "",
                Phone = ""
            };
           
            Assert.IsFalse(command.Validate().IsValid);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void ShouldBeInvalidWhenCustomerOfCommandHasNoName()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "",
                LastName = "Almeida",
                Document = "87766408090",
                Email = "gabriel@gabriel.com",
                Phone = "31986540305"
            };

            Assert.IsFalse(command.Validate().IsValid);
        }
    }
}