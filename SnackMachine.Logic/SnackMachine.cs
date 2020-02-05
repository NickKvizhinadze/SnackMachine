using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public sealed class SnackMachine
    {
        public Money MoneyInside { get; set; }

        public Money MoneyInTransaction{ get; set; }

        public void InsertMoney(Money money)
        {
            MoneyInTransaction += money;
        }

        public void ReturnMonay()
        {
            //MoneyInTransaction = 0;
        }

        public void BuySnack()
        {
            MoneyInTransaction += MoneyInTransaction;
            //MoneyInTransaction = 0;
        }
    }
}
