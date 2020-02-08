using System;
using FluentAssertions;
using Xunit;
using SnackMachine.Logic;

namespace SnackMachine.Tests
{
    public class SnackPileSpecs
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0.001)]
        public void SnackPile_invalid_price_throws_exception(decimal price)
        {
            Action action = () =>
            {
                new SnackPile(null, price, 1);
            };

            action.Should().Throw<InvalidOperationException>();
        }
    }
}
