using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NãoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arranje - Cenário.
            //Dado leilão iniciado e interessado X realizou o ultimo lance
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(fulano, 800);

            //Act - método sobre teste.
            //Quando mesmo interessado X realiza o próximo lance
            leilao.RecebeLance(fulano, 800);

            //Assert.
            //Então leilão não aceita o segundo lance
            var qtdeEsperada = 1;

            var qtdeobtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeobtida);
        }

        [Theory]
        [InlineData(4, new double[] { 100, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDAdoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            //Arranje - Cenário.
            //Dado o leilão Finalizado com X lances
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
