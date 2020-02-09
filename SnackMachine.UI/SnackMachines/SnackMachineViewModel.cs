using System.Linq;
using System.Collections.Generic;
using SnackMachine.UI.Common;
using SnackMachine.Logic.SharedKernel;
using SnackMachine.Logic.SnackMachines;

namespace SnackMachine.UI.SnackMachines
{
    public class SnackMachineViewModel : ViewModel
    {
        #region Fields
        private readonly Logic.SnackMachines.SnackMachine _snackMachine;
        private readonly SnackMachineRepository _repository;

        private string _message = "";
        #endregion

        #region Constructor

        public SnackMachineViewModel(Logic.SnackMachines.SnackMachine snackMachine)
        {
            _snackMachine = snackMachine;
            _repository = new SnackMachineRepository();
            InsertCentCommand = new Command(() => InsertMoney(Money.Cent));
            InsertTenCentCommand = new Command(() => InsertMoney(Money.TenCent));
            InsertQuarterCommand = new Command(() => InsertMoney(Money.Quarter));
            InsertDollarCommand = new Command(() => InsertMoney(Money.Dollar));
            InsertFiveDollarCommand = new Command(() => InsertMoney(Money.FiveDollar));
            InsertTwentyDollarCommand = new Command(() => InsertMoney(Money.TwentyDollar));
            ReturnMoneyCommand = new Command(() => ReturnMoney());
            BuySnackCommand = new Command<string>(BuySnack);
        }

        #endregion

        #region Properties
        public override string Caption => "Snack Machine";

        public string MoneyInTransaction => _snackMachine.MoneyInTransaction.ToString();
        public Money MoneyInside => _snackMachine.MoneyInside;

        public IReadOnlyList<SnackPileViewModel> Piles => _snackMachine
                                                            .GetAllSnackPiles()
                                                            .Select(s => new SnackPileViewModel(s))
                                                            .ToList();

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
        public Command<string> BuySnackCommand { get; private set; }

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

        private void BuySnack(string positionString)
        {
            int position = int.Parse(positionString);

            string error = _snackMachine.CanBuySnack(position);
            if(error != string.Empty)
            {
                NotifyClient(error);
                return;
            }

            _snackMachine.BuySnack(position);
            _repository.Save(_snackMachine);
            NotifyClient("You have bought a snack");
        }

        #endregion

        #region Private Methods
        private void NotifyClient(string message)
        {
            Message = message;
            Notify(nameof(MoneyInTransaction));
            Notify(nameof(MoneyInside));
            Notify(nameof(Piles));
        }
        #endregion
    }
}
