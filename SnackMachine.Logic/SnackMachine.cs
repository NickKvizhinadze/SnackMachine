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
            MoneyInTransaction = 0;
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
        public virtual decimal MoneyInTransaction { get; protected set; }
        protected IList<Slot> Slots { get; set; }

        #endregion

        #region Methods
        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        public virtual void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public virtual void ReturnMonay()
        {
            var allocatedMoney = MoneyInside.Allocate(MoneyInTransaction);

            MoneyInTransaction = 0;
            MoneyInside -= allocatedMoney;
        }

        public virtual void BuySnack(int position)
        {
            Slot slot = GetSlot(position);
            var snackPile = slot.SnackPile;

            if (snackPile.Price > MoneyInTransaction)
                throw new InvalidOperationException();

            snackPile = snackPile.SubstractOne();

            var change = MoneyInside.Allocate(MoneyInTransaction - snackPile.Price);

            if (change.Amount < MoneyInTransaction - snackPile.Price)
                throw new InvalidOperationException();
            MoneyInside -= change;
            MoneyInTransaction = 0; //TODO: insetd of this should Return Money
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
