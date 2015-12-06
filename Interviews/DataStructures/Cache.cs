using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public abstract class Cache<Q,R> where Q : IEquatable<Q>
    {
        private List<Q> mostRecentlyUsed = new List<Q>();

        private Dictionary<Q, R> _cache;

        private int _capacity;

        protected Cache(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentException("Invalid Capacity for Cache; Must be >= 1");
            }

            _capacity = capacity;
            mostRecentlyUsed = new List<Q>();
            _cache = new Dictionary<Q, R>(new QComparer<Q>());
        }

        public R Lookup(Q query)
        {
            if (_cache.ContainsKey(query))
            {
                // Update the mostRecentlyUsed information

                //ToDo: This call makes this not quite an O(1) operation.  Not sure how to solve...
                // http://www.geeksforgeeks.org/implement-lru-cache/ 
                var res = mostRecentlyUsed.First<Q>(q => q.Equals(query));
                mostRecentlyUsed.Remove(res);
                mostRecentlyUsed.Add(query);

                return _cache[query];
            }

            var result = NonCacheLookup(query);
            AddToCache(query, result);
            return result;
        }

        private void AddToCache(Q query, R result)
        {
            if (mostRecentlyUsed.Count >= _capacity)
            {
                var toRemove = mostRecentlyUsed[0];
                mostRecentlyUsed.RemoveAt(0);
                _cache.Remove(toRemove);
            }

            mostRecentlyUsed.Add(query);
            _cache.Add(query, result);
        }

        protected abstract R NonCacheLookup(Q query);
    }

    class QComparer<Q> : IEqualityComparer<Q> where Q:IEquatable<Q>
    {
        public bool Equals(Q x, Q y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Q obj)
        {
            return obj.GetHashCode();
        }
    }
}
