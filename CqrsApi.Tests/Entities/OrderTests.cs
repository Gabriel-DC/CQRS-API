using CqrsApi.Domain.Context.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CqrsApi.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrder()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsTrue(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidCustomer()
        {
            var order = new Order(
                    new Customer(
                        new Name("", ""),
                        new Document(""),
                        new Email(""),
                        ""
                    )
                );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenCustomerHasLastNameEmpty()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", ""),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
            );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidCustomerDocument()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document(""),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidCustomerWithNoEmail()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email(""),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidCustomerWithNoName()
        {
            var order = new Order(
                    new Customer(
                        new Name("", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidCustomerWithNoNameAndNoEmail()
        {
            var order = new Order(
                    new Customer(
                        new Name("", ""),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidOrderItem()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("", "Um produto", "image.png", 0, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidOrderItemWithNoName()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                          new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("", "", "image.png", 10m, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidOrderItemWithNoPrice()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 0, 100), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidOrderItemWithNoQuantity()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 0), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithInvalidOrderItemWithNoQuantityAndNoPrice()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 0, 0), 10));
            order.Validate();

            Assert.IsFalse(order.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnOrderWithValidItems()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                        )
                    );

            Assert.AreEqual(EOrderStatus.Created, order.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToChangeOrderStatusToPaid()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            order.AddItem(new OrderItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10));

            order.Pay(order.TotalValue());
            Assert.AreEqual(EOrderStatus.Paid, order.Status);
        }
        
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCancelAnOrder()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, order.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToPlaceAnOrder()
        {
            var order = new Order(
                     new Customer(
                         new Name("John", "Doe"),
                         new Document("76772258029"),
                         new Email("gabriel@gabriel.com"),
                         "123456789"
                     )
                 );

            order.Place();
            Assert.IsNotNull(order.Number);
        }        
    }
}