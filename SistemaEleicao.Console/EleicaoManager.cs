
using System.Collections.Generic;
using System.Linq;

using System.Collections.Generic;
using System.Linq;

namespace SistemaEleicao.Console
{
    public class EleicaoManager
    {
        private readonly List<Candidato> candidatos = new();

        private readonly Dictionary<string, Eleitor> eleitores = new();

        private readonly Dictionary<int, int> votosPorCandidato = new();

        public IReadOnlyList<Candidato> Candidatos =>
            candidatos.AsReadOnly();

        public IReadOnlyDictionary<int, int> VotosPorCandidato =>
            votosPorCandidato;

        public int VotosNulos { get; private set; }

        public int VotosBrancos { get; private set; }

        public bool CadastrarCandidato(string nome, int numero)
        {
            if (candidatos.Any(c => c.Numero == numero))
                return false;

            var candidato = new Candidato(nome, numero);

            candidatos.Add(candidato);
            votosPorCandidato[numero] = 0;

            return true;
        }

        public bool CadastrarEleitor(string identificador)
        {
            if (string.IsNullOrWhiteSpace(identificador))
                throw new System.ArgumentException(
                    "O identificador é obrigatório.");

            identificador = identificador.Trim();

            if (eleitores.ContainsKey(identificador))
                return false;

            eleitores.Add(
                identificador,
                new Eleitor(identificador));

            return true;
        }

        public bool RegistrarVoto(
            string identificador,
            int? numeroCandidato,
            bool votoBranco = false)
        {
            if (string.IsNullOrWhiteSpace(identificador))
                return false;

            identificador = identificador.Trim();

             if (!eleitores.TryGetValue(identificador, out Eleitor? eleitor))
             return false;

            if (eleitor.JaVotou)
                return false;

            if (votoBranco)
            {
                VotosBrancos++;
            }
            else if (
                numeroCandidato.HasValue &&
                votosPorCandidato.ContainsKey(numeroCandidato.Value))
            {
                votosPorCandidato[numeroCandidato.Value]++;
            }
            else
            {
                VotosNulos++;
            }

            eleitor.RegistrarVoto();

            return true;
        }
    }
}
