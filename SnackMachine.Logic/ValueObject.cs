using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public abstract class ValueObject<T> where T: ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            if (valueObject == null)
                return false;

            return EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> entity1, ValueObject<T> entity2)
        {
            if (entity1 is null && entity2 is null)
                return true;

            if (entity1 is null || entity2 is null)
                return false;

            return entity1.Equals(entity2);
        }

        public static bool operator !=(ValueObject<T> entity1, ValueObject<T> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
