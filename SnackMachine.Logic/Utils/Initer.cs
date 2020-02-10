using SnackMachine.Logic.Common;
using SnackMachine.Logic.Management;

namespace SnackMachine.Logic.Utils
{
    public static class Initer
    {
        public static void Init(string connectionString)
        {
            SessionFactory.Init(connectionString);
            HeadOfficeInstance.Init();
            DomainEvents.Init();
        }        
    }
}
