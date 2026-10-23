using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaEleicao.Console
{
    public class Candidato
    {
        public int Numero { get; private set; }
        public string Nome { get; private set; }

        public Candidato(string nome, int numero)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome é obrigatório.");

            if (numero <= 0)
                throw new ArgumentException("O número deve ser positivo.");

            Nome = nome;
            Numero = numero;
        }
    }
}

