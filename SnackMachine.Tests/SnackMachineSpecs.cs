﻿using System;
using Xunit;
using FluentAssertions;
using static SnackMachine.Logic.Money;

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
        public void Money_in_transaction_goes_to_money_inside_after_purchase()
        {
            var snackMachine = new Logic.SnackMachine();
            snackMachine.InsertMoney(Dollar);
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0);
            snackMachine.MoneyInside.Amount.Should().Be(2m);
        }
    }
}