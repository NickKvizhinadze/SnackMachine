using NHibernate;
using SnackMachine.Logic;
using SnackMachine.UI.Common;

namespace SnackMachine.UI
{
    public class SnackMachineViewModel : ViewModel
    {
        #region Fields
        private readonly Logic.SnackMachine _snackMachine;

        private string _message = "";
        #endregion

        #region Constructor

        public SnackMachineViewModel(Logic.SnackMachine snackMachine)
        {
            _snackMachine = snackMachine;
            InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
            InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
            InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
            InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
            InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
            InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));
            ReturnMoneyCommand = new Command(() => ReturnMoney());
            BuySnackCommand = new Command(() => BuySnack());
        }

        #endregion

        #region Properties
        public override string Caption => "Snack Machine";

        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => _snackMachine.MoneyInside;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                Notify();
            }
        }

        #endregion

        #region Commands

        public Command InsertCentCommand { get; private set; }
        public Command InsertTenCentCommand { get; private set; }
        public Command InsertQuarterCommand { get; private set; }
        public Command InsertDollarCommand { get; private set; }
        public Command InsertFiveDollarCommand { get; private set; }
        public Command InsertTwentyDollarCommand { get; private set; }
        public Command ReturnMoneyCommand { get; private set; }
        public Command BuySnackCommand { get; private set; }

        #endregion

        #region Actions 

        private void InsertMoney(Money coinOrNote)
        {
            _snackMachine.InsertMoney(coinOrNote);
            NotifyClient("You have inserted: " + coinOrNote);
        }

        private void ReturnMoney()
        {
            _snackMachine.ReturnMonay();
            NotifyClient("Money was returned");
        }

        private void BuySnack()
        {
            _snackMachine.BuySnack(1);
            using (ISession session = SessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(_snackMachine);
                transaction.Commit();
            }
            NotifyClient("You have bought a snack");
        }

        #endregion

        #region Private Methods
        private void NotifyClient(string message)
        {
            Notify(nameof(MoneyInTransaction));
            Notify(nameof(MoneyInside));
            Message = message;
        }
        #endregion
    }
}
