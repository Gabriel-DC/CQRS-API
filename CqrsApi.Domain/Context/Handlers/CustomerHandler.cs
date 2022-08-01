using CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs;
using CqrsApi.Domain.Context.Commands.CustomerCommands.Outputs;
using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Domain.Context.Services;
using CqrsApi.Domain.Context.ValueObjects;
using CqrsApi.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsApi.Domain.Context.Handlers
{
    public class CustomerHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IEmailService _emailService;

        public CustomerHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            if (_customerRepository.CheckEmailExists(command.Document))
                throw new InvalidOperationException("Cliente já cadastrado");

            if (_customerRepository.CheckDocumentExists(command.Document))
                throw new InvalidOperationException("Documento já cadastrado");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            var customer = new Customer(name, document, email, command.Phone);

            if (!customer.Validate().IsValid)
                throw new InvalidOperationException("Ocorreu um erro no cadastro! Verifique os dados e tente novamente");
            
            if(_customerRepository.Save(customer))
                _emailService.Send(customer.Email.ToString(), "Bem-vindo ao CqrsApi", "Sua conta foi criada com sucesso!");

            return new CreateCustomerModel(Guid.NewGuid(), name.ToString(), email.ToString());
        }
    }
}
