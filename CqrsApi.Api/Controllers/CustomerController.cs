using CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs;
using CqrsApi.Domain.Context.Commands.CustomerCommands.Outputs;
using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Handlers;
using CqrsApi.Domain.Context.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        [HttpGet]
        public List<CreateCustomerModel> Get()
        {           
            return new List<Customer>()
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
            }
            .Select(c => new CreateCustomerModel
            {
                Id = c.Id,
                Email = c.Email.Address,
                Name = c.Name.ToString(),
            })
            .ToList();
        }

        [HttpGet("{document}")]
        public CreateCustomerModel? GetByDOcument(string document)
        {
            return new List<Customer>()
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
            }.Where(c => c.Document.Number == document)
            .Select(c => new CreateCustomerModel
            {
                Id = c.Id,
                Email = c.Email.Address,
                Name = c.Name.ToString(),
            })
            .FirstOrDefault();
        }

        [HttpGet("{id}/orders")]
        public List<Order> GetOrders(Guid id)
        {
            return default!;
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody] CreateCustomerCommand command
        )
        {
            var validation = command.Validate();
            if (!validation.IsValid)
                return BadRequest(new { erros = validation.Errors.Select(e => e.ErrorMessage) });
            
            return Ok(command);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id)
        {
            return Ok("");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok("");
        }
    }
}
