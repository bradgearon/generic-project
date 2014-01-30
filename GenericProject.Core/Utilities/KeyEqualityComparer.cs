using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericProject.Core.Utilities
{
    public class KeyEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _Comparer;

        private readonly Func<T, object> _KeyExtractor;

        // Allows us to simply specify the key to compare with: y => y.CustomerID
        public KeyEqualityComparer(Func<T, object> keyExtractor) : this(keyExtractor, null) { }

        // Allows us to tell if two objects are equal: (x, y) => y.CustomerID == x.CustomerID
        public KeyEqualityComparer(Func<T, T, bool> comparer) : this(null, comparer) { }

        public KeyEqualityComparer(Func<T, object> keyExtractor, Func<T, T, bool> comparer)
        {
            _KeyExtractor = keyExtractor;
            _Comparer = comparer;
        }

        public bool Equals(T x, T y)
        {
            if (_Comparer != null)
                return _Comparer(x, y);
            var valX = _KeyExtractor(x);
            var objects = valX as IEnumerable<object>;
            return objects != null
                ? objects.SequenceEqual((IEnumerable<object>)_KeyExtractor(y))
                : valX.Equals(_KeyExtractor(y));
        }

        public int GetHashCode(T obj)
        {
            if (_KeyExtractor == null)
                return obj.ToString().ToLower().GetHashCode();
            var val = _KeyExtractor(obj);
            var objects = val as IEnumerable<object>;
            return objects != null
                ? (int)objects.Aggregate((x, y) => x.GetHashCode() ^ y.GetHashCode())
                : val.GetHashCode();
        }
    }
}