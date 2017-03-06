using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// A generic MaxHeap implementation that uses a List for storing objects.
    /// http://en.wikipedia.org/wiki/Binary_heap
    /// </summary>
    public class MaxHeap<T> where T : IComparable
    {
        /// <summary>
        /// Heap is implemented with a list. Index 0 contains row 0; 1-2 row 1;
        /// 3-7 row 2; etc.  
        /// </summary>
        private List<T> data = new List<T>();

        /// <summary>
        /// Insertion is done by first adding the new object to the end, and
        /// then 'bubbling up'. 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(T obj)
        {
            data.Add(obj);

            var index = this.Count - 1;

            int j = (index - 1) / 2;

            while (index > 0)
            {
                if (obj.CompareTo(data[j]) > 0)
                {
                    var tmp = data[j];
                    data[j] = data[index];
                    data[index] = tmp;

                    index = j;
                    j = (j + 1) / 2 - 1;
                }
                else
                {
                    break;
                }
            }
        }

        public T ExtractMax()
        {
            if (data.Count == 0)
            {
                throw new ArgumentOutOfRangeException("MaxHeap is empty!");
            }

            var retVal = data[0];
            data[0] = data[this.Count - 1];
            data.RemoveAt(this.Count - 1);

            Heapify(0);

            return retVal;
        }

        public T Peek(int index = 0)
        {
            return data[index];
        }

        public int Count
        {
            get { return data.Count; }
        }

        /// <summary>
        /// Recursive 'bubble down'. Compare value at i to its children.
        /// If i is less than one of its children, then swap value at i
        /// with the larger of the children.
        /// </summary>
        /// <param name="i"></param>
        private void Heapify(int i)
        {
            // The left and right children of node i
            var left = 2 * i + 1;
            var right = 2 * i + 2;
            var indexOfLargest = i;

            if (left < data.Count && data[left].CompareTo(data[indexOfLargest]) > 0)
            {
                indexOfLargest = left;
            }
            if (right < data.Count && data[right].CompareTo(data[indexOfLargest]) > 0)
            {
                indexOfLargest = right;
            }
            if (indexOfLargest != i)
            {
                var tmp = data[indexOfLargest];
                data[indexOfLargest] = data[i];
                data[i] = tmp;
                Heapify(indexOfLargest);                
            }
        }

    }
}
