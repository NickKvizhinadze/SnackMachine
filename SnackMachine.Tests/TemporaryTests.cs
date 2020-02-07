using NHibernate;
using Xunit;
using SnackMachine.Logic;

namespace SnackMachine.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            SessionFactory.Init(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SnackMachine;Integrated Security=True");
            using(ISession session = SessionFactory.OpenSession())
            {
                long id = 1;
                var snackMachine = session.Get<Logic.SnackMachine>(id);
            }
        }
    }
}
