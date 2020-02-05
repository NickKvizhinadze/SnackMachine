﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public class Money
    {
        #region Constructor

        public Money(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        #endregion

        #region Properties
        public int OneCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarCount { get; private set; }
        public int TwentyDollarCount { get; private set; }
        #endregion

        #region Methods

        public static Money operator +(Money money1, Money money2)
        {
            return new Money(
                money1.OneCentCount += money2.OneCentCount,
                money1.TenCentCount += money2.TenCentCount,
                money1.QuarterCount += money2.QuarterCount,
                money1.OneDollarCount += money2.OneDollarCount,
                money1.FiveDollarCount += money2.FiveDollarCount,
                money1.TwentyDollarCount += money2.TwentyDollarCount
            );
        }

        #endregion
    }
}
