using System.Collections.Generic;

namespace SnackMachine.Logic.Common
{
    public class AggregateRoot : Entity
    {
        #region Fields
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        #endregion

        #region Properties
        public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        #endregion

        #region Protected Methods
        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }
        #endregion

        #region Methods
        public virtual void ClearEvents()
        {
            _domainEvents.Clear();
        }
        #endregion
    }
}
