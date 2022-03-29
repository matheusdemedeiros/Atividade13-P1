using System;

namespace Atividade10.ConsoleApp
{
    public class Cliente
    {
        public string nome;
        public string endereco;
        public string cpf;
        public int idade;

        public void MudaCPF(string cpf)
        {
            ValidaCPF(cpf);
            this.cpf = cpf;
        }

        private void ValidaCPF(string cpf)
        {
            //Validações
        }
    }
}
