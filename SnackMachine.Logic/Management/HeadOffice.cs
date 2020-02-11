using SnackMachine.Logic.Atms;
using SnackMachine.Logic.Common;
using SnackMachine.Logic.SharedKernel;

namespace SnackMachine.Logic.Management
{
    public class HeadOffice : AggregateRoot
    {
        #region Properties
        public virtual decimal Balance { get; protected set; }
        public virtual Money Cash { get; protected set; } = Money.None;
        #endregion

        #region Methods
        public virtual void ChangeBalance(decimal delta)
        {
            Balance += delta;
        }

        public virtual void UnloadCashFromSnackMachine(SnackMachines.SnackMachine snackMachine)
        {
            Money money = snackMachine.UnloadMoney();
            Cash += money;
        }

        public virtual void LoadCashToAtm(Atm atm)
        {
            atm.LoadMoney(Cash);
            Cash = Money.None;
        }
        #endregion
    }
}
