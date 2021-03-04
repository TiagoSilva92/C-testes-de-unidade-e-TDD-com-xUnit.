﻿
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTestes
    {
        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void LeilaoComVariosLances(double valorEsperado, double[] ofertas)
        {
            //Arranje - Cenário.
            //Dado leilao com lances sem ordemd e valor 
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
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
        public void LeilaSemLances()
        {
            //Arranje - Cenário.
            //Dado o leilão sem qualquer lance
            var leilao = new Leilao("Van Gogh");

            //Act - método sobre teste.
            //Quando o pregão/leilão termina
            leilao.TerminaPregao();

            //Assert.
            //Então o valor do lance ganhador [e zero
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
