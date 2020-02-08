using System;
using System.Collections.Generic;
using System.Linq;
using static SnackMachine.Logic.Money;

namespace SnackMachine.Logic
{
    public class SnackMachine : AggregateRoot
    {
        #region Constructor

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = None;
            Slots = new List<Slot>
                {
                    new Slot(this, 1),
                    new Slot(this, 2),
                    new Slot(this, 3)
                };
        }

        #endregion

        #region Properties

        public virtual Money MoneyInside { get; protected set; }
        public virtual Money MoneyInTransaction { get; protected set; }
        protected IList<Slot> Slots { get; set; }

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
            Slot slot = GetSlot(position);
            slot.SnackPile = slot.SnackPile.SubstractOne();

            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        public virtual SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }
        #endregion

        #region Private Methods
        private Slot GetSlot(int position)
        {
            return Slots.Single(s => s.Position == position);
        }
        #endregion
    }
}
