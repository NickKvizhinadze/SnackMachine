﻿using System;

namespace SnackMachine.Logic
{
    public class Money : ValueObject<Money>
    {
        #region Static Properties
        public static Money None => new Money(0, 0, 0, 0, 0, 0);
        public static Money Cent => new Money(1, 0, 0, 0, 0, 0);
        public static Money TenCent => new Money(0, 1, 0, 0, 0, 0);
        public static Money Quarter => new Money(0, 0, 1, 0, 0, 0);
        public static Money Dollar => new Money(0, 0, 0, 1, 0, 0);
        public static Money FiveDollar => new Money(0, 0, 0, 0, 1, 0);
        public static Money TwentyDollar => new Money(0, 0, 0, 0, 0, 1);
        #endregion

        #region Constructor

        public Money()
        {
        }

        public Money(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount) : this()
        {
            if (oneCentCount < 0)
                throw new InvalidOperationException();
            if (tenCentCount < 0)
                throw new InvalidOperationException();
            if (quarterCount < 0)
                throw new InvalidOperationException();
            if (oneDollarCount < 0)
                throw new InvalidOperationException();
            if (fiveDollarCount < 0)
                throw new InvalidOperationException();
            if (twentyDollarCount < 0)
                throw new InvalidOperationException();

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        #endregion

        #region Properties
        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount => OneCentCount * 0.01m +
                                TenCentCount * 0.1m +
                                QuarterCount * 0.25m +
                                OneDollarCount +
                                FiveDollarCount * 5 +
                                TwentyDollarCount * 20;
        #endregion

        #region Methods
        public Money Allocate(decimal amount)
        {
            int twentyDollarCount = Math.Min((int)(amount / 20), TwentyDollarCount);
            amount -= (twentyDollarCount * 20);

            int fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
            amount -= fiveDollarCount * 5;

            int oneDollarCount = Math.Min((int)amount, OneDollarCount);
            amount -= oneDollarCount;

            int quarterCount = Math.Min((int)(amount / 0.25m), QuarterCount);
            amount -= quarterCount * 0.25m;

            int tenCentCount = Math.Min((int)(amount / 0.1m), TenCentCount);
            amount -= tenCentCount * 0.1m;

            int oneCentCount = Math.Min((int)(amount / 0.01m), OneCentCount);

            return new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount
                );
        }

        public static Money operator +(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount
            );
        }
        public static Money operator -(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount
            );
        }
        public static Money operator *(Money money1, int number)
        {
            if (number < 0)
                throw new InvalidOperationException();

            if (number == 0)
                return None;

            return new Money(
                money1.OneCentCount * number,
                money1.TenCentCount * number,
                money1.QuarterCount * number,
                money1.OneDollarCount * number,
                money1.FiveDollarCount * number,
                money1.TwentyDollarCount * number
            );
        }

        public override string ToString()
        {
            if (Amount < 1)
                return "¢" + (Amount * 100).ToString("0");
            return "$" + Amount.ToString("0.00");
        }

        #endregion

        #region Protected Methods

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount
                && TenCentCount == other.TenCentCount
                && QuarterCount == other.QuarterCount
                && OneDollarCount == other.OneDollarCount
                && FiveDollarCount == other.FiveDollarCount
                && TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ QuarterCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;
                return hashCode;
            }
        }

        #endregion
    }
}
