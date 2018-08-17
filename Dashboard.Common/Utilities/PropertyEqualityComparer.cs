using System;
using System.Collections.Generic;

namespace Dashboard.Common.Utilities
{
    public class PropertyEqualityComparer<TType, TProp> : IEqualityComparer<TType>
    {
        private Func<TType, TProp> _fnProperty;

        public PropertyEqualityComparer(Func<TType, TProp> property)
        {
            _fnProperty = property;
        }

        public bool Equals(TType x, TType y)
        {
            if (!typeof(TType).IsValueType)
            {
                if (object.ReferenceEquals(x, y))
                    return true;
                else if (object.ReferenceEquals(x, null) ^ object.ReferenceEquals(y, null))
                    return false;
            }

            return _fnProperty(x).Equals(_fnProperty(y));
        }

        public int GetHashCode(TType obj)
        {
            return _fnProperty(obj).GetHashCode();
        }
    }
}
