using NHibernate;
using SnackMachine.Logic;

namespace SnackMachine.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            Logic.SnackMachine snackMachine;
            using(ISession session = SessionFactory.OpenSession())
            {
                snackMachine = session.Get<Logic.SnackMachine>(1L);
            }
            var viewModel = new SnackMachineViewModel(snackMachine);
            _dialogService.ShowDialog(viewModel);
        }
    }
}
