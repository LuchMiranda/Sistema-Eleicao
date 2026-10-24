# Sistema de Eleição

Aplicação de console em C# (.NET) que simula uma eleição: cadastro de candidatos e eleitores, votação e apuração dos resultados. Desenvolvida para a disciplina de Garantia da Qualidade de Software, com testes unitários em xUnit e histórico de commits seguindo o padrão Conventional Commits.

## Funcionalidades

- **Cadastrar candidato:** nome e número, sem permitir números duplicados.
- **Cadastrar eleitor:** identificador (CPF ou título simulado) para controlar quem já votou.
- **Registrar voto:** voto em candidato, voto em branco ou voto nulo. Cada eleitor só pode votar uma vez.
- **Relatório da eleição:** votos por candidato, votos válidos, nulos, brancos, total de votos e candidato vencedor.

### Regras de negócio

- Não é possível cadastrar dois candidatos com o mesmo número.
- Um eleitor não pode votar duas vezes.
- Só eleitores cadastrados podem votar.
- Voto em número de candidato inexistente é contabilizado como nulo.

## Estrutura do projeto

```
Sistema-Eleicao/
├── SistemaEleicao.Console/    # Aplicação de console (regras de negócio e menu)
│   ├── Program.cs
│   ├── EleicaoManager.cs
│   ├── CadastroCandidato.cs
│   └── CadastroEleitor.cs
├── SistemaEleicao.Tests/      # Testes unitários (xUnit)
│   ├── CadastroCandidatoTests.cs
│   └── VotacaoTests.cs
└── SistemaEleicao.slnx
```

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- Git

## Como executar

Clone o repositório e entre na pasta:

```bash
git clone <URL_DO_REPOSITORIO>
cd Sistema-Eleicao
```

Execute a aplicação:

```bash
dotnet run --project SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

### Menu

```
===== SISTEMA DE ELEIÇÃO =====
1 - Cadastrar candidato
2 - Cadastrar eleitor
3 - Registrar voto
4 - Exibir relatório da eleição
0 - Sair
```

### Exemplo de uso

1. Opção `1`: cadastre um ou mais candidatos (nome e número).
2. Opção `2`: cadastre os eleitores com o identificador.
3. Opção `3`: informe o identificador do eleitor e escolha o tipo de voto.
4. Opção `4`: veja o relatório com a apuração.

> Observação: o identificador informado na votação deve ser exatamente o mesmo usado no cadastro do eleitor.

## Como rodar os testes

Na raiz do projeto:

```bash
dotnet test
```

## Padrão de commits

O histórico segue o [Conventional Commits](https://www.conventionalcommits.org/): `feat`, `fix`, `test`, `refactor`, `docs` e `chore`, por exemplo:

```
feat(candidato): implement candidate entity and validation logic
test(votacao): add unit tests for voter registration and voting rules
```

## Autor

Lucas Henrique Miranda, estudante de Análise e Desenvolvimento de Sistemas (UNA).