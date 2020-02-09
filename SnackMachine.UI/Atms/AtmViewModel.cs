using SnackMachine.UI.Common;
using SnackMachine.Logic.Atms;
using SnackMachine.Logic.SharedKernel;

namespace SnackMachine.UI.Atms
{
    public class AtmViewModel : ViewModel
    {
        #region Fields
        private Atm _atm;
        private AtmRepository _repository;
        private PaymentGateway _paymentGateway;
        public string _message;
        #endregion

        #region Constructor
        public AtmViewModel(Atm atm)
        {
            _atm = atm;
            _repository = new AtmRepository();
            _paymentGateway = new PaymentGateway();
            TakeMoneyCommand = new Command<decimal>(x => x > 0, TakeMoney);
        }
        #endregion

        #region Properties
        public override string Caption => "ATM";
        public Money MoneyInside => _atm.MoneyInside;
        public decimal MoneyChanrged => _atm.MoneyCharged;

        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                Notify();
            }
        }

        public Command<decimal> TakeMoneyCommand { get; private set; }
        #endregion

        #region Actions
        private void TakeMoney(decimal amount)
        {
            string error = _atm.CanTakeMoney(amount);
            if (error != string.Empty)
            {
                NotifyClient(error);
            }
            decimal amountWithCommision = _atm.CalculateAmountWithCommision(amount);
            _paymentGateway.ChargePayment(amountWithCommision);
            _atm.TakeMoney(amount);
            _repository.Save(_atm);
            NotifyClient("You have taken " + amount.ToString("C2"));
        }
        #endregion

        #region Private Methods
        private void NotifyClient(string message)
        {
            Message = message;
            Notify(nameof(MoneyChanrged));
            Notify(nameof(MoneyInside));

        }
        #endregion
    }
}
