using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// A generic MinHeap implementation that uses a List for storing objects.
    /// http://en.wikipedia.org/wiki/Binary_heap
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinHeap<T> where T : IComparable
    {
        /// <summary>
        /// Implement the heap with a linked list. data[0] is the root, while each
        /// node's left child is 2i + 1; right child is 2i+2.  The parent of a node
        /// is floor((i − 1) ∕ 2).
        /// </summary>
        private List<T> data = new List<T>();

        /// <summary>
        /// Add the new object to the base of the tree (aka end of the list). Then,
        /// perform a 'bubble up' operation 
        /// </summary>
        /// <param name="obj"></param>
        public void Insert(T obj)
        {
            data.Add(obj);
            int index = data.Count - 1;

            int j = (index - 1) / 2;

            while (index > 0)
            {
                if (obj.CompareTo(data[j]) < 0)
                {
                    var tmp = data[j];
                    data[j] = data[index];
                    data[index] = tmp;

                    index = j;
                    // The parent node:
                    j = (j - 1) / 2;
                }
                else
                {
                    break;
                }
            }
        }

        public T ExtractMin()
        {
            if (this.Count == 0)
            {
                throw new ArgumentOutOfRangeException("MinHeap is Empty.");
            }

            // Remove the element at the top (the min value), and then replace
            // it with the element at the end. Now, 'heapify' at element[0].
            T retVal = data[0];
            data[0] = data[Count - 1];
            data.RemoveAt(Count - 1);

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
        /// 'Heapify' shifts the orders of elements in the heap until the 
        /// heap properties of min or max are satisfied again. This is the
        /// "bubble-down" technique.
        /// </summary>
        /// <param name="i">The index where a modification was made.</param>
        private void Heapify(int i)
        {
            var left = 2 * i + 1;
            var right = 2 * i + 2;
            int smallest = i;

            if (left < Count && data[smallest].CompareTo(data[left]) > 0)
            {
                smallest = left;
            }
            if (right < Count && data[smallest].CompareTo(data[right]) > 0)
            {
                smallest = right;
            }
            if (smallest != i)
            {
                T tmp = data[smallest];
                data[smallest] = data[i];
                data[i] = tmp;
                Heapify(smallest);
            }
        }
    }
}
