using SnackMachine.Logic.Utils;

namespace SnackMachine.Logic.Common
{
    public abstract class Repository<T> where T : AggregateRoot
    {
        #region Methods
        public T GetById(long id)
        {
            using (var session = SessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public void Save(T aggregateRoot)
        {
            using (var session = SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(aggregateRoot);
                transaction.Commit();
            }
        }
        #endregion 
    }
}
