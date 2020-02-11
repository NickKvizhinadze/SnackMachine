using System.Linq;
using System.Collections.Generic;
using NHibernate.Linq;
using SnackMachine.Logic.Common;
using SnackMachine.Logic.Utils;

namespace SnackMachine.Logic.SnackMachines
{
    public class SnackMachineRepository: Repository<SnackMachine>
    {
        public IReadOnlyList<SnackMachineDto> GetSnackMachineList()
        {
            using (var session = SessionFactory.OpenSession())
            {
                return session.Query<SnackMachine>()
                    .ToList() // Fetch data into memory
                    .Select(x => new SnackMachineDto(x.Id, x.MoneyInside.Amount))
                    .ToList();
            }
        }
    }
}
