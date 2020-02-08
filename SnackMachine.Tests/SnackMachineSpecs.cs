using System;
using Xunit;
using FluentAssertions;
using static SnackMachine.Logic.Money;
using System.Linq;
using SnackMachine.Logic;

namespace SnackMachine.Tests
{
    public class SnackMachineSpecs
    {
        [Fact]
        public void Return_money_empties_money_in_transaction()
        {
            var snackMachine = new Logic.SnackMachine();
            snackMachine.InsertMoney(Dollar);

            snackMachine.ReturnMonay();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0);
        }

        [Fact]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            var snackMachine = new Logic.SnackMachine();
            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [Fact]
        public void Cannot_insert_more_than_one_coint_or_note_at_a_time()
        {
            var snackMachine = new Logic.SnackMachine();
            var twoCent = Cent + Cent;
            Action action = () =>
            {
                snackMachine.InsertMoney(twoCent);
            };

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void BuySnack_trades_inserted_money_for_a_snack()
        {
            var snackMachine = new Logic.SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 1m, 10));
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Amount.Should().Be(0);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
            snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }

        [Fact]
        public void Cannot_make_purchase_when_there_is_no_snacks()
        {
            var snackMachine = new Logic.SnackMachine();
            snackMachine.InsertMoney(Dollar);

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cannot_make_purchase_if_not_enought_money_inserted()
        {
            var snackMachine = new Logic.SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 2m, 10));
            snackMachine.InsertMoney(Dollar);

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }
    }
}
