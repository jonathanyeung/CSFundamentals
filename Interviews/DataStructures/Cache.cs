using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// O(1) Least Recently Used Cache.
    /// http://www.geeksforgeeks.org/implement-lru-cache/ 
    /// </summary>
    /// <typeparam name="Q"></typeparam>
    /// <typeparam name="R"></typeparam>
    public abstract class Cache<Q,R> where Q : IEquatable<Q>
    {
        struct Node
        {
            public Q _q;
            public R _r;
        }

        private List<Node> mostRecentlyUsed;

        private Dictionary<Q, Node> _cache;

        private int _capacity;

        protected Cache(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentException("Invalid Capacity for Cache; Must be >= 1");
            }

            _capacity = capacity;
            mostRecentlyUsed = new List<Node>();
            _cache = new Dictionary<Q, Node>(new QComparer<Q>());
        }

        public R Lookup(Q query)
        {
            if (_cache.ContainsKey(query))
            {
                var res = _cache[query];

                // Refresh the position of result in the queue.
                mostRecentlyUsed.Remove(res);
                mostRecentlyUsed.Add(res);

                return res._r;
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
                _cache.Remove(toRemove._q);
            }

            var newNode = new Node() { _q = query, _r = result };
            mostRecentlyUsed.Add(newNode);
            _cache.Add(query, newNode);
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
