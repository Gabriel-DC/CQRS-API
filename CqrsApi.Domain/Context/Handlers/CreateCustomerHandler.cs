using CqrsApi.Domain.Context.Commands.CustomerCommands.Inputs;
using CqrsApi.Domain.Context.Commands.CustomerCommands.Outputs;
using CqrsApi.Domain.Context.Entities;
using CqrsApi.Domain.Context.Repositories;
using CqrsApi.Domain.Context.Services;
using CqrsApi.Domain.Context.ValueObjects;
using CqrsApi.Shared.Commands;

namespace CqrsApi.Domain.Context.Handlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, CreateCustomerModel>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IEmailService _emailService;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }

        public CreateCustomerModel Handle(CreateCustomerCommand command)
        {
            var validationResult = command?.Validate();
            if (!(validationResult?.IsValid ?? false))
                throw new InvalidOperationException(
                    string.Join('\n', validationResult?.Errors?.Select(e => e.ErrorMessage) ?? null!) ??
                    "Informações inválidas");

            if (_customerRepository.CheckEmailExists(command!.Email))
                throw new InvalidOperationException("Email já cadastrado");

            if (_customerRepository.CheckDocumentExists(command.Document))
                throw new InvalidOperationException("Documento já cadastrado");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            var customer = new Customer(name, document, email, command.Phone);

            if (!customer.Validate().IsValid)
                throw new InvalidOperationException(
                    "Ocorreu um erro no cadastro! Verifique os dados e tente novamente");

            if (_customerRepository.Save(customer))
                _emailService.Send(customer.Email.ToString(), "Bem-vindo ao CqrsApi",
                    "Sua conta foi criada com sucesso!");

            return new CreateCustomerModel(customer.Id, name.ToString(), email.ToString());
        }
    }
}