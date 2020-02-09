using System;
using SnackMachine.Logic.Common;
using SnackMachine.Logic.SharedKernel;
using static SnackMachine.Logic.SharedKernel.Money;

namespace SnackMachine.Logic.Atms
{
    public class Atm : AggregateRoot
    {
        #region Constants
        private const decimal CommissionRate = 0.01m;
        #endregion

        #region Properties
        public virtual Money MoneyInside { get; protected set; } = None;
        public virtual decimal MoneyCharged { get; protected set; }
        #endregion

        #region Methods
        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        public virtual string CanTakeMoney(decimal amount)
        {
            if (amount <= 0m)
                return "Invalid amount";

            if (MoneyInside.Amount < amount)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(amount))
                return "Not enough change";
            
            return string.Empty;
        }

        public virtual void TakeMoney(decimal amount)
        {
            if (!MoneyInside.CanAllocate(amount))
                throw new InvalidOperationException();

            var allocatedMoney = MoneyInside.Allocate(amount);
            MoneyInside -= allocatedMoney;
            decimal amountWithCommision = CalculateAmountWithCommision(amount);

            MoneyCharged += amountWithCommision;
        }
        #endregion

        #region Private Methods
        public virtual decimal CalculateAmountWithCommision(decimal amount)
        {
            decimal commission = amount * CommissionRate;
            decimal lessThanCent = commission % 0.01m;
            if (lessThanCent > 0)
                commission = commission - lessThanCent + 0.01m;

            return amount + commission;
        }
        #endregion
    }
}
