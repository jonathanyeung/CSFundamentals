using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    public static class Sorting
    {

        public static void PrintArray(int[] input)
        {
            string output = "";

            foreach (var i in input)
            {
                output += i;
                output += " ";
            }

            Console.WriteLine(output);
        }

        public static void MergeSort(int[] input)
        {
            _mergeSort(input, 0, input.Length - 1);
        }

        private static void _mergeSort(int[] input, int left, int right)
        {
            if (left < right)
            {
                var mid = (left + right) / 2;
                _mergeSort(input, left, mid);
                _mergeSort(input, mid + 1, right);
                merge(input, left, mid, right);
            }
        }

        /// <summary>
        /// Keys to merge: Use a helper array to store the original values.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="left"></param>
        /// <param name="mid"></param>
        /// <param name="right"></param>
        private static void merge(int[] input, int left, int mid, int right)
        {
            int[] Helpers = new int[input.Length];

            Array.Copy(input, Helpers, Helpers.Length);

            int lP = left;
            int rP = mid + 1;
            int oP = left;

            while (lP <= mid && rP <= right)
            {
                if (Helpers[lP] <= Helpers[rP])
                {
                    input[oP] = Helpers[lP];
                    oP++;
                    lP++;
                }

                else
                {
                    input[oP] = Helpers[rP];
                    rP++;
                    oP++;
                }
            }
            // Only need to copy if left side has remaining values, as the right
            // side values are already in the original array.
            while (lP <= mid)
            {
                input[oP] = Helpers[lP];
                oP++;
                lP++;
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
    }
}
