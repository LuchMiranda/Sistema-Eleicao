
using SistemaEleicao.Console;
using Terminal = System.Console;
using System.Linq;

class Program
{
    static void Main()
    {
        EleicaoManager eleicao = new();
        bool executando = true;

        while (executando)
        {
            Terminal.WriteLine("\n===== SISTEMA DE ELEIÇÃO =====");
            Terminal.WriteLine("1 - Cadastrar candidato");
            Terminal.WriteLine("2 - Cadastrar eleitor");
            Terminal.WriteLine("3 - Registrar voto");
            Terminal.WriteLine("4 - Exibir relatório da eleição");
            Terminal.WriteLine("0 - Sair");
            Terminal.Write("Escolha uma opção: ");

            string? opcao = Terminal.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarCandidato(eleicao);
                    break;

                case "2":
                    CadastrarEleitor(eleicao);
                    break;

                case "3":
                    RegistrarVoto(eleicao);
                    break;

                case "4":
                    ExibirRelatorio(eleicao);
                    break;

                case "0":
                    executando = false;
                    Terminal.WriteLine("Sistema encerrado.");
                    break;

                default:
                    Terminal.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void CadastrarCandidato(EleicaoManager eleicao)
    {
        Terminal.Write("Nome do candidato: ");
        string? nome = Terminal.ReadLine();

        Terminal.Write("Número do candidato: ");
        string? entrada = Terminal.ReadLine();

        if (string.IsNullOrWhiteSpace(nome) ||
            !int.TryParse(entrada, out int numero))
        {
            Terminal.WriteLine("Nome ou número inválido.");
            return;
        }

        try
        {
            bool cadastrado = eleicao.CadastrarCandidato(nome, numero);

            Terminal.WriteLine(cadastrado
                ? "Candidato cadastrado com sucesso!"
                : "Já existe um candidato com esse número.");
        }
        catch (ArgumentException ex)
        {
            Terminal.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void CadastrarEleitor(EleicaoManager eleicao)
    {
        Terminal.Write("Identificador do eleitor (CPF ou título simulado): ");
        string? identificador = Terminal.ReadLine();

        if (string.IsNullOrWhiteSpace(identificador))
        {
            Terminal.WriteLine("Identificador inválido.");
            return;
        }

        try
        {
            bool cadastrado = eleicao.CadastrarEleitor(identificador);

            Terminal.WriteLine(cadastrado
                ? "Eleitor cadastrado com sucesso!"
                : "Esse eleitor já está cadastrado.");
        }
        catch (ArgumentException ex)
        {
            Terminal.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void RegistrarVoto(EleicaoManager eleicao)
    {
        Terminal.Write("Identificador do eleitor: ");
        string? identificador = Terminal.ReadLine();

        if (string.IsNullOrWhiteSpace(identificador))
        {
            Terminal.WriteLine("Identificador inválido.");
            return;
        }

        Terminal.WriteLine("\nTipo de voto:");
        Terminal.WriteLine("1 - Votar em um candidato");
        Terminal.WriteLine("2 - Voto em branco");
        Terminal.WriteLine("3 - Voto nulo");
        Terminal.Write("Escolha: ");

        string? tipo = Terminal.ReadLine();

        int? numeroCandidato = null;
        bool votoBranco = false;

        switch (tipo)
        {
            case "1":
                Terminal.Write("Número do candidato: ");

                if (!int.TryParse(Terminal.ReadLine(), out int numero))
                {
                    Terminal.WriteLine("Número inválido. Voto não registrado.");
                    return;
                }

                numeroCandidato = numero;
                break;

            case "2":
                votoBranco = true;
                break;

            case "3":
                // Número null sem marcar votoBranco representa voto nulo.
                break;

            default:
                Terminal.WriteLine("Tipo de voto inválido.");
                return;
        }

        bool registrado = eleicao.RegistrarVoto(
            identificador,
            numeroCandidato,
            votoBranco
        );

        Terminal.WriteLine(registrado
            ? "Voto registrado com sucesso!"
            : "Não foi possível registrar o voto. Verifique o cadastro do eleitor ou se ele já votou.");
    }

    static void ExibirRelatorio(EleicaoManager eleicao)
    {
        Terminal.WriteLine("\n===== RELATÓRIO DA ELEIÇÃO =====");

        if (eleicao.Candidatos.Count == 0)
        {
            Terminal.WriteLine("Nenhum candidato cadastrado.");
        }
        else
        {
            foreach (var candidato in eleicao.Candidatos)
            {
                int votos = eleicao.VotosPorCandidato[candidato.Numero];

                Terminal.WriteLine(
                    $"Candidato: {candidato.Nome} | " +
                    $"Número: {candidato.Numero} | Votos: {votos}"
                );
            }
        }

        int votosValidos = eleicao.VotosPorCandidato.Values.Sum();
        int totalVotos = votosValidos +
                         eleicao.VotosNulos +
                         eleicao.VotosBrancos;

        Terminal.WriteLine($"\nVotos válidos: {votosValidos}");
        Terminal.WriteLine($"Votos nulos: {eleicao.VotosNulos}");
        Terminal.WriteLine($"Votos em branco: {eleicao.VotosBrancos}");
        Terminal.WriteLine($"Total de votos: {totalVotos}");

        if (votosValidos == 0)
        {
            Terminal.WriteLine("Ainda não há votos válidos para apurar um vencedor.");
            return;
        }

        int maiorQuantidade = eleicao.VotosPorCandidato.Values.Max();

        var vencedores = eleicao.Candidatos
            .Where(c => eleicao.VotosPorCandidato[c.Numero] == maiorQuantidade)
            .ToList();

        if (vencedores.Count > 1)
        {
            Terminal.WriteLine("Resultado: empate entre:");

            foreach (var candidato in vencedores)
            {
                Terminal.WriteLine(
                    $"- {candidato.Nome} (número {candidato.Numero})"
                );
            }
        }
        else
        {
            var vencedor = vencedores[0];

            Terminal.WriteLine(
                $"Vencedor: {vencedor.Nome} " +
                $"(número {vencedor.Numero}), com {maiorQuantidade} voto(s)."
            );
        }
    }
}
