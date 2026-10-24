using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEleicao.Console;

namespace SistemaEleicao.Tests
{
    public class VotacaoTests
    {
        [Fact]
        public void CadastrarEleitor_IdentificadorValido_RetornaTrue()
        {
            var eleicao = new EleicaoManager();

            bool resultado =
                eleicao.CadastrarEleitor("123456");

            Assert.True(resultado);
        }

        [Fact]
        public void CadastrarEleitor_IdentificadorDuplicado_RetornaFalse()
        {
            var eleicao = new EleicaoManager();

            eleicao.CadastrarEleitor("123456");

            bool resultado =
                eleicao.CadastrarEleitor("123456");

            Assert.False(resultado);
        }

        [Fact]
        public void RegistrarVoto_CandidatoExistente_ContabilizaVoto()
        {
            var eleicao = new EleicaoManager();

            eleicao.CadastrarCandidato("Lucas", 10);
            eleicao.CadastrarEleitor("123456");

            bool resultado =
                eleicao.RegistrarVoto("123456", 10);

            Assert.True(resultado);
            Assert.Equal(1, eleicao.VotosPorCandidato[10]);
        }

        [Fact]
        public void RegistrarVoto_EleitorQueJaVotou_RetornaFalse()
        {
            var eleicao = new EleicaoManager();

            eleicao.CadastrarCandidato("Lucas", 10);
            eleicao.CadastrarEleitor("123456");

            eleicao.RegistrarVoto("123456", 10);

            bool resultado =
                eleicao.RegistrarVoto("123456", 10);

            Assert.False(resultado);
            Assert.Equal(1, eleicao.VotosPorCandidato[10]);
        }

        [Fact]
        public void RegistrarVoto_CandidatoInexistente_ContabilizaVotoNulo()
        {
            var eleicao = new EleicaoManager();

            eleicao.CadastrarCandidato("Lucas", 10);
            eleicao.CadastrarEleitor("123456");

            bool resultado =
                eleicao.RegistrarVoto("123456", 99);

            Assert.True(resultado);
            Assert.Equal(1, eleicao.VotosNulos);
        }

        [Fact]
        public void RegistrarVoto_VotoBranco_ContabilizaVotoBranco()
        {
            var eleicao = new EleicaoManager();

            eleicao.CadastrarEleitor("123456");

            bool resultado = eleicao.RegistrarVoto(
                "123456", null, votoBranco: true);

            Assert.True(resultado);
            Assert.Equal(1, eleicao.VotosBrancos);
        }

        [Fact]
        public void RegistrarVoto_EleitorNaoCadastrado_RetornaFalse()
        {
            var eleicao = new EleicaoManager();

            eleicao.CadastrarCandidato("Lucas", 10);

            bool resultado =
                eleicao.RegistrarVoto("999999", 10);

            Assert.False(resultado);
            Assert.Equal(0, eleicao.VotosPorCandidato[10]);
        }
    }
}