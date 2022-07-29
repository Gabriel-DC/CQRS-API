

using CqrsApi.Domain.Context.Entities.Generic;
using CqrsApi.Domain.Context.ValueObjects;
using FluentValidation;
using static CqrsApi.Domain.Context.Entities.Order;
using static CqrsApi.Domain.Context.Entities.OrderItem;

namespace CqrsApi.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var number = 0;

        var validator = new GenericValidation();
        validator.RuleFor(r => number).GreaterThan(5).WithMessage("Rule A");

        validator.RuleSet("Generic", () =>
        {
            validator.RuleFor(x => number).GreaterThanOrEqualTo(10).WithMessage("Rule B");
        });

        var results = validator.ValidateRules();

        var resultset = validator.ValidateRuleSets("Generic");

        var c = new Customer(
            new Name("Gabriel", "Almeida"),
            new Document("12345678910"),
            new Email("gabriel@gabriel.com"),
            "99999999999");

        var orderItem = new OrderItem(
            new Product("Produto 1", "Descrição do produto 1", "image.png", 0m, 10), 0);

        orderItem.Validate();

        var teste2 = new Delivery(DateTime.Now);

        var order = new Order(c);


        order.Place();
        
        order.Pay(order.TotalValue());

        order.Ship();

        order.Cancel();
    }
}