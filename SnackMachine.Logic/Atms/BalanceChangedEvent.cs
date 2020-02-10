using SnackMachine.Logic.Common;

namespace SnackMachine.Logic.Atms
{
    public class BalanceChangedEvent: IDomainEvent
    {
        #region Constructor
        public BalanceChangedEvent(decimal delta)
        {
            Delta = delta;
        }
        #endregion

        #region Properties
        public decimal Delta { get; private set; }
        #endregion
    }
}
