using System.Windows;
using SnackMachine.Logic;

namespace SnackMachine.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Initer.Init(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SnackMachine;Integrated Security=True");
        }
    }
}
