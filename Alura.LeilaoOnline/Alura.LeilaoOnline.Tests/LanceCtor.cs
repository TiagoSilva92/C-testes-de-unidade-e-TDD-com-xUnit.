using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionValorNegativo()
        {
            // Arranje - Cenário.
            var valorNegativo = -100;

            //Assert.
            Assert.Throws<ArgumentException>(
                //Action
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
