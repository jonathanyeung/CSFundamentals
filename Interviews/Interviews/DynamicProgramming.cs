﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    /// <summary>
    /// Dynamic Programming And Recursion
    /// </summary>
    public class DynamicProgramming
    {
        #region Cracking The Coding Interview

        // NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview; p 9.1
        /// A child is running up a staircase with n steps, and can hop either
        /// 1, 2, or 3 steps at a time.  How many possible ways can the child
        /// run up the stairs?
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ChildRunningUpSteps(int n)
        {
            if (n <= 0 )
            {
                return 0;
            }

            var array = new int[n];

            for (int i = 0; i < n; i++)
            {
                switch (n)
                {
                    case (0):
                        array[0] = 1; // 1
                        break;
                    case (1):
                        array[1] = 1 + array[0]; // 2
                        break;
                    case (2):
                        array[2] = 1 + array[2] + array[1]; // 4
                        break;
                    default:
                        array[n] = array[n - 1] + array[n - 2] + array[n - 3];
                        break;
                }
            }

            return array[n - 1];
        }

        // NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview; p 9.2
        /// How many paths can a robot go from (0,0) to (X,Y)? The robot can 
        /// only move down or right.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PossibleRobotPaths(int x, int y)
        {
            if (x <= 0 || y <= 0)
            {
                return 0;
            }

            var array = new int[x,y];

            // Initialize first row and column
            for (int i = 0; i < x; i++)
            {
                array[i, 0] = i + 1;
            }
            for (int j = 0; j < y; j++)
            {
                array[0, j] = j + 1;
            }

            // Fill out the DP array:
            for (int i = 1; i < x; i++)
            {
                for (int j = 1; j < y; j++)
                {
                    array[i, j] = array[i - 1, j] + array[i, j - 1];
                }
            }

            return array[x - 1, y - 1];
        }


        /// <summary>
        /// Cracking the Coding Interview; p 9.3
        /// A magic index in an array A[0..n-1] is an index where A[i] = i.
        /// Find a magic index, if one exists.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int MagicIndex(int[] input)
        {
            int left = 0;
            int right = input.Length - 1;

            while (left <= right)
            {
                var mid = left + right / 2;

                if (input[mid] == mid)
                {
                    return mid;
                }
                else if (input[mid] > mid)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return -1;
        }


        // NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview; p 9.4
        /// Return all subsets of a set.
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static List<List<int>> SubsetsOfSet(int[] set)
        {
            var ret = new List<List<int>>();

            // Empty Set
            ret.Add(new List<int>() { });

            for (int i = 0; i < set.Length; i++)
            {
                var newLists = new List<List<int>>();
                foreach (var existingList in ret)
                {
                    var newList = new List<int>(existingList);
                    newList.Add(set[i]);
                    newLists.Add(newList);
                }

                foreach (var newList in newLists)
                {
                    ret.Add(newList);
                }
            }

            return ret;
        }


        /// <summary>
        /// Cracking the Coding Interview; p 9.5
        /// Return all permutations of a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> StringPermutations(string input)
        {
            throw new NotImplementedException();
        }

   
        #endregion


        /// <summary>
        /// Return the sum of the values of the maximum contiguous subarray
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int MaximumContiguous(int[] input)
        {
            int largestSum = Int32.MinValue;
            int curSum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var newSum = curSum + input[i];
                if (newSum > 0)
                {
                    curSum = newSum;
                }
                else
                {
                    curSum = 0;
                }
                if (curSum > largestSum)
                {
                    largestSum = curSum;
                }
            }

            return largestSum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int MaximumNoncontiguous(int[] input)
        {
            int sum = 0;
            int largestNegative = int.MinValue;
            bool positiveFound = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] > 0)
                {
                    sum += input[i];
                    positiveFound = true;
                }
                else
                {
                    if (input[i] > largestNegative)
                    {
                        largestNegative = input[i];
                    }
                }
            }
            return (positiveFound) ? sum : largestNegative;
        }

        /// <summary>
        /// Knapsack problem. The key is to build a 2D array, and 
        /// fill in the values from top left to bottom right.
        /// Rows contain the different item values, columns contain
        /// 1 -> k.  Entries into the array contain the max value 
        /// up to k inclusive, using weights of items in the given
        /// row and above.
        /// </summary>
        /// <param name="items">Values & Weights of Items</param>
        /// <param name="k">Target Value, not to be exceeded.</param>
        /// <returns></returns>
        public static int Knapsack(int[] items, int k)
        {
            var values = new int[items.Length, k + 1];

            int currentHighest = 0;

            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; j <= k; j++)
                {
                    var divisor = j / items[i];
                    var remainder = j % items[i];

                    int totalValue = divisor * items[i];

                    if (remainder < j)
                    {
                        totalValue += values[i, remainder];
                    }

                    if (i >= 1)
                    {
                        values[i, j] = (totalValue > values[i - 1, j]) ? totalValue : values[i - 1, j];
                    }
                    else
                    {
                        values[i, j] = totalValue;
                    }

                    if (values[i,j] > currentHighest)
                    {
                        currentHighest = values[i, j];
                    }
                }
            }

            return currentHighest;
        }
    }
}
