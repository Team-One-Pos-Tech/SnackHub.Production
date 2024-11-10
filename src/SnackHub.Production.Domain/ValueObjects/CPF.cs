
using System.IO;

namespace SnackHub.Domain.ValueObjects
{
    public class CPF : ValueObject
    {
        public string Value { get; private set; }

        public CPF(string value)
        {
            string cpf = Sanitize(value);

            Value = cpf;
        }

        private static string Sanitize(string value)
        {
            var cpf = value.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            return cpf;
        }

        // source: https://www.macoratti.net/11/09/c_val1.htm
        public bool IsValid()
        {
            var cpf = Value;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        
        public static bool TryParse(string value, out CPF cpf)
        {
            cpf = new CPF(value);
            
            return cpf.IsValid();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
