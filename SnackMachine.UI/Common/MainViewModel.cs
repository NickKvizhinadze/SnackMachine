using SnackMachine.UI.SnackMachines;
using SnackMachine.Logic.SnackMachines;

namespace SnackMachine.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            Logic.SnackMachines.SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}
