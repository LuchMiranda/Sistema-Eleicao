
using System.Collections.Generic;
using System.Linq;

namespace SistemaEleicao.Console
{
    public class EleicaoManager
    {
        private readonly List<Candidato> candidatos = new();

        public IReadOnlyList<Candidato> Candidatos => candidatos.AsReadOnly();

        public bool CadastrarCandidato(string nome, int numero)
        {
            if (candidatos.Any(c => c.Numero == numero))
                return false;

            candidatos.Add(new Candidato(nome, numero));
            return true;
        }
    }
}
