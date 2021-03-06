using System.Collections.Generic;
using System.Linq;
using System;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDopregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }
        public double ValorDestino { get; }

        public Leilao(string peca, double valorDestino = 0)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDopregao;
            ValorDestino = valorDestino;
        }

        private bool NovoLanceEAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento) && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
                throw new InvalidOperationException();

            if (ValorDestino > 0)
            {
                //modalidade oferta superior mais próxima
                Ganhador = Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .Where(l => l.Valor > ValorDestino)
                    .OrderBy(l => l.Valor)
                    .FirstOrDefault();
            }
            else
            {
                //modalidade maior valor
                Ganhador = Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .OrderBy(l => l.Valor)
                    .LastOrDefault();
            }

            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
