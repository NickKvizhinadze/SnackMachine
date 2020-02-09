using Xunit;
using SnackMachine.Logic.Atms;
using FluentAssertions;
using static SnackMachine.Logic.SharedKernel.Money;

namespace SnackMachine.Tests
{
    public class AtmSpecs
    {
        [Fact]
        public void Take_money_exchanges_money_with_commision()
        {
            var atm = new Atm();
            atm.LoadMoney(Dollar);
            atm.TakeMoney(1m);

            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [Fact]
        public void Commission_is_at_least_one_cent()
        {
            var atm = new Atm();
            atm.LoadMoney(Cent);
            atm.TakeMoney(0.01m);

            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(0.02m);
        }

        [Fact]
        public void Commission_is_rounded_up_to_next_cent()
        {
            var atm = new Atm();
            atm.LoadMoney(Dollar + TenCent);
            atm.TakeMoney(1.1m);

            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.12m);
        }
    }
}
