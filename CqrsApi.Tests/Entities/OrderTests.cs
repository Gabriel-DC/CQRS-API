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

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToAddAValidItemToOrder()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            order.AddItem(new Product("Produto 01", "Um produto", "image.png", 10m, 100), 10);

            Assert.AreEqual(1, order.GetItems().Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToAddAValidDeliveryToOrder()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            order.AddDelivery(new Delivery(DateTime.Now.AddDays(5)));            

            Assert.AreEqual(1, order.GetDeliveries().Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldHasNoNumberBeforeOrderIsPlaced()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            var product = new Product("Um produto", "Um produto maneiro","image.png", 10m, 100);
            order.AddItem(product, 10);

            Assert.IsNull(order.Number);            
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldDecreaseQuantityOfProductAfterAddOrderItem()
        {
            var order = new Order(
                    new Customer(
                        new Name("John", "Doe"),
                        new Document("76772258029"),
                        new Email("gabriel@gabriel.com"),
                        "123456789"
                    )
                );

            var product = new Product("Um produto", "Um produto maneiro", "image.png", 10m, 100);
            order.AddItem(product, 10);

            Assert.AreEqual(90, product.QuantityOnHand);
        }
    }
}