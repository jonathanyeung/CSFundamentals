using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    struct kvt<K, J, V>
    {
        public K keyOne;
        public J keyTwo;
        public V value;
    }

    internal class KeyDuoComparer<K, J, V> : IEqualityComparer<kvt<K, J, V>> where K : IEquatable<K> where J: IEquatable<J>
    {
        public bool Equals(kvt<K, J, V> x, kvt<K, J, V> y)
        {
            return (x.keyOne.Equals(y.keyOne) && x.keyTwo.Equals(y.keyTwo));
        }

        public int GetHashCode(kvt<K, J, V> obj)
        {
            return obj.GetHashCode();
        }
    }

    public class MultiHashMap<K, J, V> where K : IEquatable<K> where J : IEquatable<J>
    {
        private int size = 1000;

        private List<kvt<K, J, V>>[] data;

        public MultiHashMap()
        {
            data = new List<kvt<K, J, V>>[size];

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
        public V this[K keyOne, J keyTwo]
        {
            get
            {
                var index = ComputeHash(keyOne, keyTwo);
                var list = data[index];
                if (list == null)
                {
                    throw new ArgumentOutOfRangeException("Key not found!");
                }
                return list.Find(k => k.keyOne.Equals(keyOne) && k.keyTwo.Equals(keyTwo)).value;
            }
            set
            {
                var index = ComputeHash(keyOne, keyTwo);
                var list = data[index];
                if (list == null)
                {
                    throw new ArgumentOutOfRangeException("Key not found!");
                }
                var kvp = list.Find(k => k.keyOne.Equals(keyOne) && k.keyTwo.Equals(keyTwo));
                kvp.value = value;
            }
        }

        private int ComputeHash(K keyOne, J keyTwo)
        {
            return (keyOne.GetHashCode() ^ keyTwo.GetHashCode()) % size;
        }

        public void Add(K keyOne, J keyTwo, V value)
        {
            var _kvt = new kvt<K, J, V>() { keyOne = keyOne, keyTwo = keyTwo, value = value };

            var index = ComputeHash(keyOne, keyTwo);

            if (data[index] == null)
            {
                data[index] = new List<kvt<K, J, V>>();
            }

            if (data[index].Contains(_kvt, new KeyDuoComparer<K, J, V>()))
            {
                throw new ArgumentException("Key already exists!");
            }

            data[index].Add(new kvt<K, J, V>() { keyOne = keyOne, keyTwo = keyTwo, value = value });
        }

        public void Remove(K keyOne, J keyTwo)
        {
            var index = ComputeHash(keyOne, keyTwo);

            var list = data[index];

            if (list != null)
            {
                list.RemoveAll(k => k.keyOne.Equals(keyOne) && k.keyTwo.Equals(keyTwo));
            }
        }
    }
}
