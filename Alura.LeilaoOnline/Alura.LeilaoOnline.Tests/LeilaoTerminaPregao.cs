using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arranje - Cenário.
            //Dado leilao com lances sem ordemd e valor 
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if ((i % 2) == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }

            //Act - método sobre teste.
            //Quando o pregão/leilão termina
            leilao.TerminaPregao();

            //Assert.
            //Então o valor esperado é o maior valor
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - Cenário.
            var leilao = new Leilao("Van Gogh");

            //Assert.
           var excecaoObtida = Assert.Throws<InvalidOperationException>(
                //Act - método sobre teste.
                () => leilao.TerminaPregao()
            );

            var msgEsperada = "Não é possivel terminar o pregão sem que ele tenha começado,Para isso utilize o métodop IniciaPregao()";

            Assert.Equal(msgEsperada, excecaoObtida.Message);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arranje - Cenário.
            //Dado o leilão sem qualquer lance
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            //Act - método sobre teste.
            //Quando o pregão/leilão termina
            leilao.TerminaPregao();

            //Assert.
            //Então o valor do lance ganhador é zero
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}