using System;
using System.Linq;
using static SnackMachine.Logic.Money;

namespace SnackMachine.Logic
{
    public class SnackMachine : Entity
    {
        #region Properties

        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual Money MoneyInTransaction { get; protected set; } = None;

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

        public virtual void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        #endregion
    }
}
