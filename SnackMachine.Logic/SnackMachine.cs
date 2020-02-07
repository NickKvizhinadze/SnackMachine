using System;
using System.Collections.Generic;
using System.Linq;
using static SnackMachine.Logic.Money;

namespace SnackMachine.Logic
{
    public class SnackMachine : Entity
    {
        #region Constructor

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = None;
            Slots = new List<Slot>
                {
                    new Slot(this, null, 0, 0m, 1),
                    new Slot(this, null, 0, 0m, 2),
                    new Slot(this, null, 0, 0m, 3)
                };
        }

        #endregion

        #region Properties

        public virtual Money MoneyInside { get; protected set; }
        public virtual Money MoneyInTransaction { get; protected set; }
        public IList<Slot> Slots { get; set; }

        #endregion

        #region Methods

        public virtual void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public virtual void ReturnMonay()
        {
            MoneyInTransaction = None;
        }

        public virtual void BuySnack(int position)
        {
            Slot slot = Slots.Single(x => x.Position == position);
            slot.Quantity--;

            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public virtual void LoadSnacks(int position, Snack snack, int quantity, decimal price)
        {
            var slot = Slots.Single(s => s.Position == position);
            slot.Snack = snack;
            slot.Quantity = quantity;
            slot.Price = price;
        }
        #endregion
    }
}
