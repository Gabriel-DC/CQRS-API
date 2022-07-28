using CqrsApi.Domain.Context.Entities.Generic;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CqrsApi.Domain.Context.ValueObjects.Document;

namespace CqrsApi.Domain.Context.ValueObjects
{
    public class Document : GenericValidator<DocumentValidator>
    {
        public Document(string number)
        {
            Number = number;

            Validate();
        }

        public string Number { get; private set; }

        public override string ToString() => Number;

        public class DocumentValidator : AbstractValidator<Document>
        {
            public DocumentValidator()
            {
                RuleFor(r => r.Number)
                    .NotEmpty()
                    .WithMessage("Documento obrigatório")
                    .Must(BeValidDocument)
                    .WithMessage("Documento inválido");
            }

            public static bool BeValidDocument(string number)
            {
                if (string.IsNullOrWhiteSpace(number))
                    return false;

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;

                string cpf = number.Trim().Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf[..9];
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf += digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito += resto.ToString();
                return cpf.EndsWith(digito);
            }
        }
    }
}
