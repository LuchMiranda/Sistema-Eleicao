
using Xunit;
using SistemaEleicao.Console;

namespace SistemaEleicao.Tests
{
    public class CadastroCandidatoTests
    {
        [Fact]
        public void CadastrarCandidato_DadosValidos_RetornaTrue()
        {
            // Arrange
            var eleicao = new EleicaoManager();

            // Act
            bool resultado = eleicao.CadastrarCandidato("Lucas", 10);

            // Assert
            Assert.True(resultado);
            Assert.Single(eleicao.Candidatos);
        }

        [Fact]
        public void CadastrarCandidato_NumeroDuplicado_RetornaFalse()
        {
            // Arrange
            var eleicao = new EleicaoManager();
            eleicao.CadastrarCandidato("Lucas", 10);

            // Act
            bool resultado = eleicao.CadastrarCandidato("Gabriel", 10);

            // Assert
            Assert.False(resultado);
            Assert.Single(eleicao.Candidatos);
        }

        [Fact]
        public void CadastrarCandidato_NumerosDiferentes_CadastraAmbos()
        {
            // Arrange
            var eleicao = new EleicaoManager();

            // Act
            eleicao.CadastrarCandidato("Lucas", 10);
            eleicao.CadastrarCandidato("Gabriel", 20);

            // Assert
            Assert.Equal(2, eleicao.Candidatos.Count);
        }

        [Fact]
        public void Candidato_NomeVazio_LancaExcecao()
        {
            // Act e Assert
            Assert.Throws<System.ArgumentException>(
                () => new Candidato("", 10)
            );
        }

        [Fact]
        public void Candidato_NumeroZero_LancaExcecao()
        {
            // Act e Assert
            Assert.Throws<System.ArgumentException>(
                () => new Candidato("Lucas", 0)
            );
        }
    }
}

