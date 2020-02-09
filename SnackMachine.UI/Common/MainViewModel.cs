using NHibernate;
using SnackMachine.Logic;

namespace SnackMachine.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            Logic.SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}
