using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.Trees;

namespace Interviews
{
    public static class Sorting
    {

        public static void MergeSort<T>(T[] input) where T : IComparable
        {
            _MergeSort(input, 0, input.Length - 1);
        }

        private static void _MergeSort<T>(T[] input, int left, int right) where T : IComparable
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                _MergeSort(input, left, mid);
                _MergeSort(input, mid + 1, right);
                Merge(input, left, right, mid + 1);
            }
        }

        /// <summary>
        /// Keys to merge: Use a helper array to store the original values.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="mid"></param>
        private static void Merge<T>(T[] input, int left, int right, int mid) where T : IComparable
        {
            T[] helper = new T[input.Length];
            Array.Copy(input, helper, input.Length);

            var dP = left;
            var lP = left;
            var rP = mid;

            while (lP < mid && rP <= right)
            {
                if (helper[lP].CompareTo(helper[rP]) < 0)
                {
                    input[dP] = helper[lP];
                    dP++;
                    lP++;
                }
                else
                {
                    input[dP] = helper[rP];
                    dP++;
                    rP++;
                }
            }

            // Only need to copy if left side has remaining values, as the right
            // side values are already in the original array.
            while (lP < mid)
            {
                input[dP] = helper[lP];
                lP++;
                dP++;
            }
        }

        // Notes about QuickSort:
        // The partition point guarantees that all values coming before will be less
        // than or equal to all values coming after.  However, it does not mean that
        // the partition value needs to be at the edge.
        public static void QuickSort(int[] input)
        {
            _quickSort(input, 0, input.Length - 1);
        }

        private static void _quickSort(int[] input, int left, int right)
        {
            var pivotIndex = partition(input, left, right);

            if (left < pivotIndex - 1)
            {
                _quickSort(input, left, pivotIndex - 1);
            }
            if (pivotIndex < right)
            {
                _quickSort(input, pivotIndex, right);
            }
        }

        private static int partition(int[] input, int left, int right)
        {
            int pivot = input[(left + right) / 2];

            while (left <= right)
            {
                while (input[left] < pivot)
                {
                    left++;
                }
                while (input[right] > pivot)
                {
                    right--;
                }

                if (left <= right)
                {
                    Swap(input, left, right);
                    left++;
                    right--;
                }
            }

            return left;
        }


        /// <summary>
        /// Selection Rank algorithm. This returns the k-th smallest element
        /// in an unsorted list in O(n) time. It is based on doing a partial
        /// quicksort on partitions of the array.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SelectionRank(int[] input, int k)
        {
            k--; // Arrays are Zero Indexed.

            int left = 0;
            int right = input.Length - 1;
            while (left < right)
            {
                var partitionPoint = selectionRankPartition(input, left, right);

                if (partitionPoint == k)
                {
                    return input[k];
                }
                else if (partitionPoint < k)
                {
                    left = partitionPoint + 1;
                }
                else
                {
                    right = partitionPoint - 1;
                }
            }

            return input[left];
        }

        public static int selectionRankPartition(int[] input, int left, int right)
        {
            int mid = (left + right) / 2;

            int value = input[mid];

            int lp = left; int rp = right - 1;

            Swap(input, right, mid);

            while (lp <= rp)
            {
                if (input[lp] <= value)
                {
                    lp++;
                }
                else if (input[rp] > value)
                {
                    rp--;
                }
                else
                {
                    Swap(input, lp, rp);
                    lp++;
                    rp--;
                }
            }

            Swap(input, lp, right);
            return lp;
        }


        /// <summary>
        /// Alternative implementation of QuickSort.  In this version, the partition value IS 
        /// located at the interface between the two partitions.  (This type of partitioning is
        /// required for Selection Rank).
        /// </summary>
        /// <param name="input"></param>
        public static void QuickSortTwo(int[] input)
        {
            partitionTwo(input, 0, input.Length - 1);
        }

        private static void partitionTwo(int[] input, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int mid = (left + right) / 2;

            int value = input[mid];

            int lp = left; int rp = right - 1;

            Swap(input, right, mid);

            while (lp <= rp)
            {
                if (input[lp] <= value)
                {
                    lp++;
                }
                else if (input[rp] > value)
                {
                    rp--;
                }
                else
                {
                    Swap(input, lp, rp);
                    lp++;
                    rp--;
                }
            }

            Swap(input, lp, right);
            partitionTwo(input, left, lp - 1);
            partitionTwo(input, lp + 1, right);
        }


        private static void Swap(int[] input, int a, int b)
        {
            var temp = input[a];
            input[a] = input[b];
            input[b] = temp;
        }


        /// <summary>
        /// Counting Sort is useful if the input contains a small number of 
        /// values ({1,2,3}). Use a BST to count the instances of each key,
        /// then just write each value to its count # into the array.
        /// Complexity is nLog(k), where n is array length, k is key count.
        /// </summary>
        /// <param name="input"></param>
        public static void CountingSort(int[] input)
        {
            if (input.Length <= 0)
            {
                return;
            }

            var newKVP = new KVP<int,int>(input[0], 1);

            // Create the tree and insert all values into it.
            var tree = new BinarySearchTree<KVP<int,int>>(newKVP);

            for (int i = 1; i < input.Length; i++)
            {
                var newNode = new KVP<int, int>(input[i], 1);
                var node = tree.Find(newNode);

                if (node == null)
                {
                    tree.Insert(newNode);
                }
                else
                {
                    node.value.value++;
                }
            }
            
            // Now iterate through the tree and write out the values to 
            // the array.
            int index = 0;

            SearchAndWrite(tree, input, ref index);
        }


        /// <summary>
        /// Helper method that does in order traversal on the BST from CountingSort,
        /// and writes the node's 'key' value to the array 'value' number of times
        /// to the array at the specified index.
        /// </summary>
        /// <param name="node">current node of the BST tree</param>
        /// <param name="input">the result array</param>
        /// <param name="index">the current write index</param>
        private static void SearchAndWrite(BinarySearchTree<KVP<int,int>> node, int[] input, ref int index)
        {
            // Standard In-Order Traversal.
            if (node.Left != null)
            {
                SearchAndWrite(node.Left, input, ref index);
            }

            for (var i = index; i < index + node.value.value; i++)
            {
                input[i] = node.value.key;
            }
            index += node.value.value;

            if (node.Right != null)
            {
                SearchAndWrite(node.Right, input, ref index);
            }
        }
    }

    class KVP<K, V> : IComparable where K : IComparable
    {
        public K key;
        public V value;

        public KVP(K key, V value)
        {
            this.key = key;
            this.value = value;
        }

        public int CompareTo(object obj)
        {
            return key.CompareTo((obj as KVP<K, V>).key);
        }
    }
}
