using System;
using System.Collections.Generic;
using System.Linq;
using SnackMachine.Logic.Common;
using SnackMachine.Logic.SharedKernel;
using static SnackMachine.Logic.SharedKernel.Money;

namespace SnackMachine.Logic.SnackMachines
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
        protected virtual IList<Slot> Slots { get; }

        #endregion

        #region Methods
        public virtual void LoadMoney(Money money)
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
        
        public virtual string CanBuySnack(int position)
        {
            var snackPile = GetSnackPile(position);

            if (snackPile.Quantity == 0)
                return "The snack pile is empty";

            if (MoneyInTransaction < snackPile.Price)
                return "Not enought money";

            if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
                return "Not enough change";

            return string.Empty;
        }

        public virtual void BuySnack(int position)
        {
            if (CanBuySnack(position) != string.Empty)
                throw new InvalidOperationException();

            Slot slot = GetSlot(position);

            slot.SnackPile = slot.SnackPile.SubstractOne();
            var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);

            MoneyInside -= change;
            MoneyInTransaction = 0; //TODO: insetd of this should Return Money
        }

        public virtual SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }

        public virtual IReadOnlyList<SnackPile> GetAllSnackPiles()
        {
            return Slots
                    .OrderBy(s => s.Position)
                    .Select(s => s.SnackPile)
                    .ToList();
        }

        public virtual Money UnloadMoney()
        {
            if (MoneyInTransaction > 0)
                throw new InvalidOperationException();

            Money money = MoneyInside;
            MoneyInside = Money.None;
            return money;
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
