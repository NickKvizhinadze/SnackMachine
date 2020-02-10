using System.Reflection;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using NHibernate.Event;

namespace SnackMachine.Logic.Utils
{
    public class SessionFactory
    {
        #region Fields
        private static ISessionFactory _factory;
        #endregion

        #region Methods
        public static ISession OpenSession()
        {
            return _factory.OpenSession();
        }

        public static void Init(string connectionString)
        {
            _factory = BuildSessionFactory(connectionString);
        }
        #endregion

        #region Private Methods
        private static ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(
                        ForeignKey.EndsWith("ID"),
                        ConventionBuilder.Property
                        .When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()))
                    .Conventions.Add<TableNameConvention>()
                    .Conventions.Add<HiloConvention>()
                )
                .ExposeConfiguration(x =>
                {
                    x.EventListeners.PostCommitUpdateEventListeners =
                        new IPostUpdateEventListener[] { new EventListener() };
                    x.EventListeners.PostCommitInsertEventListeners =
                        new IPostInsertEventListener[] { new EventListener() };
                    x.EventListeners.PostCommitDeleteEventListeners = 
                        new IPostDeleteEventListener[] { new EventListener() };
                    x.EventListeners.PostCollectionUpdateEventListeners =
                        new IPostCollectionUpdateEventListener[] { new EventListener() };
                });

            return configuration.BuildSessionFactory();
        }
        #endregion

        #region Classes
        public class TableNameConvention : IClassConvention
        {
            public void Apply(IClassInstance instance)
            {
                instance.Table("[dbo].[" + instance.EntityType.Name + "]");
            }
        }

        public class HiloConvention : IIdConvention
        {
            public void Apply(IIdentityInstance instance)
            {
                instance.Column(instance.EntityType.Name + "ID");
                instance.GeneratedBy.HiLo("[dbo][Ids]", "NextHigh", "9", "EntityName = '" + instance.EntityType.Name + "'");
            }
        }
        #endregion
    }
}
