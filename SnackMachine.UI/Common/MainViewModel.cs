using SnackMachine.UI.Management;

namespace SnackMachine.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public DashboardViewModel Dashboard { get; private set; }

        public MainViewModel()
        {
            Dashboard = new DashboardViewModel();
        }
    }
}
