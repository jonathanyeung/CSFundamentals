using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    public class DynamicProgramming
    {
        public static void MaximumSubarray()
        {
            var t = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < t; i++)
            {
                int length = Convert.ToInt32(Console.ReadLine());

                var s_elements = Console.ReadLine().Split(' ');

                var elements = new int[length];

                for (int j = 0; j < length; j++)
                {
                    elements[j] = Convert.ToInt32(s_elements[j]);
                }

                var output = MaximumContiguous(elements).ToString() + MaximumNoncontiguous(elements).ToString();
            }
        }

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

        public static int MaximumNoncontiguous(int[] input)
        {
            int sum = 0;
            int largestNegative = 0;
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
