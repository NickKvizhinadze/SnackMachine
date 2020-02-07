using System;
using NHibernate.Proxy;

namespace SnackMachine.Logic
{
    public abstract class Entity
    {
        #region Properties
        public virtual long Id { get; protected set; }
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity entity1, Entity entity2)
        {
            if (entity1 is null && entity2 is null)
                return true;

            if (entity1 is null || entity2 is null)
                return false;

            return entity1.Equals(entity2);
        }

        public static bool operator !=(Entity entity1, Entity entity2)
        {
            return !(entity1 == entity2);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }
        #endregion

        #region Private Metods

        private Type GetRealType()
        {
            return NHibernateProxyHelper.GetClassWithoutInitializingProxy(this);
        }

        #endregion
    }
}
