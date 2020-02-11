using System.Linq;
using System.Collections.Generic;
using NHibernate.Linq;
using SnackMachine.Logic.Utils;
using SnackMachine.Logic.Common;

namespace SnackMachine.Logic.Atms
{
    public class AtmRepository : Repository<Atm>
    {
        public IReadOnlyList<AtmDto> GetAtmList()
        {
            using (var session = SessionFactory.OpenSession())
            {
                return session.Query<Atm>()
                    .ToList() // Fetch data into memory
                    .Select(x => new AtmDto(x.Id, x.MoneyInside.Amount))
                    .ToList();
            }
        }
    }
}
