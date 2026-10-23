using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEleicao.Console
{
    public class Eleitor
    {
        public string Identificador { get; }

        public bool JaVotou { get; private set; }

        public Eleitor(string identificador)
        {
            if (string.IsNullOrWhiteSpace(identificador))
                throw new System.ArgumentException(
                    "O identificador é obrigatório.");

            Identificador = identificador;
        }

        public void RegistrarVoto()
        {
            JaVotou = true;
        }
    }
}