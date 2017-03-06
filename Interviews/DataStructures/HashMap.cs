using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public struct KVP<K, V>
    {
        public K key;
        public V value;
    }

    internal class KeyComparer<K,V> : IEqualityComparer<KVP<K,V>> where K : IEquatable<K>
    {
        public bool Equals(KVP<K, V> x, KVP<K, V> y)
        {
            return (x.key.Equals(y.key));
        }

        public int GetHashCode(KVP<K, V> obj)
        {
            return obj.GetHashCode();
        }
    }

    public class HashMap<K, V> where K : IEquatable<K>
    {
        private int size = 1000;

        private List<KVP<K,V>>[] data;

        public HashMap()
        {
            data = new List<KVP<K, V>>[size];

            for (int i = 0; i < size; i++)
            {
                data[i] = null;
            }
        }

        /// <summary>
        /// Indexer implementation
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public V this[K key]
        {
            get
            {
                var index = ComputeHash(key);
                var list = data[index];
                if (list == null)
                {
                    throw new ArgumentOutOfRangeException("Key not found!");
                }
                return list.Find(k => k.key.Equals(key)).value;
            }
            set
            {
                var index = ComputeHash(key);
                var list = data[index];
                if (list == null)
                {
                    throw new ArgumentOutOfRangeException("Key not found!");
                }
                var kvp = list.Find(k => k.key.Equals(key));
                kvp.value = value;
            }
        }

        /// <summary>
        /// This simple implementation of computing the hash just uses the built
        /// in C# function. When creating a custom one, consider making it 'rolling',
        /// adding one char to the string to be hashed can be computed easily from
        /// the last result (O(1)).  Ex:
        /// foreach (char c in s) {
        ///     val = (val * 997 + c) % modulus; }
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ComputeHash(K key)
        {
            return key.GetHashCode() % size;
        }

        public void Add(K key, V value)
        {
            var _kvp = new KVP<K,V>(){key = key, value = value};

            var index = ComputeHash(key);

            if (data[index] == null)
            {
                data[index] = new List<KVP<K, V>>();
            }

            if (data[index].Contains(_kvp, new KeyComparer<K,V>()))
            {
                throw new ArgumentException("Key already exists!");
            }

            data[index].Add(new KVP<K, V>() { key = key, value = value });
        }

        public void Remove(K key)
        {
            var index = ComputeHash(key);

            var list = data[index];

            if (list != null)
            {
                list.RemoveAll(k => k.key.Equals(key));
            }
        }

        public bool ContainsKey(K key)
        {
            var _kvp = new KVP<K, V>() { key = key, value = default(V) };

            var index = ComputeHash(key);

            if (data[index] == null)
            {
                return false;
            }
            else if (data[index].Contains(_kvp, new KeyComparer<K, V>()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
