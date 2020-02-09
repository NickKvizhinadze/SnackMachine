using SnackMachine.UI.SnackMachines;
using SnackMachine.Logic.SnackMachines;
using SnackMachine.UI.Atms;
using SnackMachine.Logic.Atms;

namespace SnackMachine.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            //Logic.SnackMachines.SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
            //var viewModel = new SnackMachineViewModel(snackMachine);
            
            Atm atm = new AtmRepository().GetById(1);
            var viewModel = new AtmViewModel(atm);
            
            _dialogService.ShowDialog(viewModel);
        }
    }
}
