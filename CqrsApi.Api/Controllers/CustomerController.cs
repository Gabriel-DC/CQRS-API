using CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs;
using CqrsApi.Domain.Context.Commands.CustomerCommands.Outputs;
using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Handlers;
using CqrsApi.Domain.Context.Queries.CustomerQueries;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Domain.Context.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CreateCustomerHandler _customerHandler;

        public CustomerController(ICustomerRepository customerRepository, CreateCustomerHandler createCustomerHandler)
        {
            _customerRepository = customerRepository;
            _customerHandler = createCustomerHandler;
        }

        [HttpGet]
        public List<IndexCustomersQuery> Get() => _customerRepository.GetAll()
                .ToList();

        [HttpGet("document/{document}")]
        public GetCustomerQuery? GetByDocument(string document) => _customerRepository.GetCustomerByDocument(document);

        [HttpGet("{id}")]
        public GetCustomerQuery? GetById(Guid id) => _customerRepository.GetCustomerById(id);
        

        [HttpGet("{id}/orders")]
        public List<IndexCustomerOrdersQuery> GetOrders(Guid id)
        {
            return default!;
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody] CreateCustomerCommand command
        )
        {
            var result = _customerHandler.Handle(command);
            return Ok(result);
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
