using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 100, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDAdoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            //Arranje - Cenário.
            //Dado o leilão Finalizado com X lances
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            leilao.TerminaPregao();

            //Act - método sobre teste.
            //Quando leilão recebe nova oferta de lance
            leilao.RecebeLance(fulano, 100);

            //Assert.
            //Então a quantidade continua sendo X
            var qtdeobtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeobtida);
        }
    }
}
