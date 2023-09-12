using CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs;
using CqrsApi.Domain.Context.Handlers;
using CqrsApi.Domain.Context.Queries.CustomerQueries;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Shared.Commands;
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
        //[ResponseCache(Duration = 30)]
        public List<IndexCustomersQuery> Get() => _customerRepository.GetAll()
                .ToList();

        [HttpGet("document/{document}")]
        public GetCustomerQuery GetByDocument(string document) => _customerRepository.GetCustomerByDocument(document);

        [HttpGet("{id}")]
        public GetCustomerQuery GetById(Guid id) => _customerRepository.GetCustomerById(id);
        

        [HttpGet("{id}/orders")]
        public List<IndexCustomerOrdersQuery> GetOrders(Guid id)
        {
            return default!;
        }

        [HttpPost]
        public ICommandResult Post(
            [FromBody] CreateCustomerCommand command
        )
        {
            try
            {
                var result = _customerHandler.Handle(command);

                var success = true;
                return new CommandResult(success, success ? "" : "Ocorreu um erro ao cadastrar um cliente", result);
            }
            catch(Exception ex)
            {
                return new CommandResult(false, ex.Message, null!);
            }
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
