using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CqrsApi.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        //Should be able to create an order
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrder()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Do"),
                        new Document("16884403721"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            Assert.IsFalse(order.IsValid);
        }
    }
}