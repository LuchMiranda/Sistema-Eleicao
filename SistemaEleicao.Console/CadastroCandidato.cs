using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEleicao.Console
{
    public class Candidato
    {
        public int Numero { get; set; }
        public string Nome { get; set; }

        public Candidato(string nome, int numero)
        {
            Nome = nome;
            Numero = numero;
        }
    }
}
