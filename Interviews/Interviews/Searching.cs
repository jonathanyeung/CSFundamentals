using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    public static class Searching
    {
        /// <summary>
        /// Recursive Implementation of Binary Search
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BinarySearch(int[] input, int value)
        {
            Array.Sort(input);

            return _search(input, value, 0, input.Length - 1);
        }

        private static int _search(int[] input, int value, int left, int right)
        {
            if (right < left)
            {
                return -1;
            }

            int mid = (left + right) / 2;

            if (input[mid] == value)
            {
                return mid;
            }
            else if (input[mid] > value)
            {
                return _search(input, value, left, mid - 1);
            }
            else
            {
                return _search(input, value, mid + 1, right);
            }
        }


        /// <summary>
        /// Iterative (non-recursive) implementation of binary search
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IterativeBinarySearch(int[] input, int value)
        {
            Array.Sort(input);

            int left = 0, right = input.Length - 1;

            while (left < right)
            {
                var mid = (left + right) / 2;

                if (input[mid] == value)
                {
                    return mid;
                }

                if (input[mid] > value)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }


        /// <summary>
        /// Find a value in an array that is sorted, but has been rotated by
        /// k values.
        /// http://www.geeksforgeeks.org/search-an-element-in-a-sorted-and-pivoted-array/
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BinarySearchOnRotatedArray(int[] input, int value)
        {
            int left = 0, right = input.Length - 1;

            while (left <= right)
            {
                var mid = (left + right) / 2;

                if (value == input[mid])
                {
                    return mid;
                }

                // If left side is sorted:
                if (input[left] <= input[mid])
                {
                    // if the value falls in the range of the left side, it 
                    // must be in the left side. Else check the right side.
                    if (value <= input[mid] && value >= input[left])
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
                // Else the right side must necessarily be sorted.
                else
                {
                    // if the value falls in the range of the right side, it 
                    // must be in the right side. Else check the left side.
                    if (value >= input[mid] && value <= input[right])
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
            }
            return -1;
        }
    }
}
