using CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs;
using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        [HttpGet]
        public List<Customer> Get()
        {
            return default!;
        }

        [HttpGet("{id}")]
        public Customer GetById(Guid id)
        {
            return default!;
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
            return Ok("");
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
