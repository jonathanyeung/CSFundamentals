using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class MaxHeap<T> where T : IComparable
    {
        private List<T> data = new List<T>();

        public void Insert(T obj)
        {
            data.Add(obj);

            var index = this.Count - 1;

            var j = index - 1;

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

        private void Heapify(int i)
        {
            var left = 2 * i + 1;
            var right = 2 * i + 2;
            var largest = i;

            if (left < data.Count && data[left].CompareTo(data[largest]) > 0)
            {
                largest = left;
            }
            if (right < data.Count && data[right].CompareTo(data[largest]) > 0)
            {
                largest = right;
            }
            if (largest != i)
            {
                var tmp = data[largest];
                data[largest] = data[i];
                data[i] = tmp;
                Heapify(largest);                
            }
        }

    }
}
