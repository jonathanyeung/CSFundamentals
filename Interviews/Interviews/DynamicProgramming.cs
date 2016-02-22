using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    class CoinSet
    {

        public CoinSet()
        {
        }
        public CoinDenomination CurrentValue;

        public static int GetValue(CoinDenomination coin)
        {
            int coinValue = 0;
            switch (coin)
            {
                case CoinDenomination.quarter:
                    coinValue = 25;
                    break;
                case CoinDenomination.dime:
                    coinValue = 10;
                    break;
                case CoinDenomination.nickel:
                    coinValue = 5;
                    break;
                case CoinDenomination.penny:
                    coinValue = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    break;
            }

            return coinValue;
        }

        public static CoinDenomination GetNextLowerDenomination(CoinDenomination coin)
        {
            switch (coin)
            {
                case CoinDenomination.quarter:
                    return CoinDenomination.dime;
                    break;
                case CoinDenomination.dime:
                    return CoinDenomination.nickel;
                    break;
                case CoinDenomination.nickel:
                    return CoinDenomination.penny;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    break;
            }
        }
    }

    enum CoinDenomination
    {
        quarter,
        dime,
        nickel,
        penny
    }

    public class DynamicProgramming
    {
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
        /// Cracking the Coding Interview; p 9.8
        /// 
        /// Given n pairs of parentheses, return all valid open-close
        /// combinations of parentheses.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<string> ProperParentheses(int n)
        {
            var results = new List<string>();

            ProperParentheses(n, n, results, "");

            return results;
        }

        private static void ProperParentheses(int remainingOpening, int remainingClosing, List<string> curList, string curString)
        {
            if (remainingClosing == 0)
            {
                curList.Add(curString);
            }

            if (remainingClosing > remainingOpening)
            {
                ProperParentheses(remainingOpening, remainingClosing - 1, curList, curString + ")");
            }

            if (remainingOpening > 0)
            {
                ProperParentheses(remainingOpening - 1, remainingClosing, curList, curString + "(");
            }
        }


        /// <summary>
        /// Cracking the Coding Interview; p 9.7
        /// 
        /// Implement the paint fill function.  Given a point in a picture
        /// (int array), fill in the contiguous area with the specified new
        /// color.
        /// </summary>
        /// <param name="paint"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public static void PaintFill(int[,] paint, int x, int y, int color)
        {
            if (x < 0 || x > paint.Length - 1)
            {
                return;
            }

            if (y < 0 || y > paint.LongLength - 1)
            {
                return;
            }

            int curColor = paint[x, y];

            if (curColor == color)
            {
                return;
            }

            var queue = new Queue<Point>();

            queue.Enqueue(new Point(x, y));

            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();

                //Top Left
                if (cur.x > 0 && cur.y > 0)
                {
                    if (paint[cur.x,cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }

                // Top Mid
                if (cur.y > 0)
                {
                    if (paint[cur.x, cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }

                // Top Right
                if (cur.y > 0 && cur.x < paint.Length - 1)
                {
                    if (paint[cur.x, cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }

                // Mid Right
                if (cur.x < paint.Length - 1)
                {
                    if (paint[cur.x, cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }

                // Bottom Right
                if (cur.y < paint.LongLength - 1 && cur.x < paint.Length - 1)
                {
                    if (paint[cur.x, cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }

                // Bottom Mid
                if (cur.y < paint.LongLength - 1)
                {
                    if (paint[cur.x, cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }

                // Bottom Left
                if (cur.y < paint.LongLength - 1 && cur.x > 0)
                {
                    if (paint[cur.x, cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }

                // Mid Left
                if (cur.x > 0)
                {
                    if (paint[cur.x, cur.y] == curColor)
                    {
                        paint[cur.x, cur.y] = color;

                        queue.Enqueue(cur);
                    }
                }
            }
        }

        private struct Point
        {
            public Point (int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        }


        /// <summary>
        /// Cracking the Coding Interview; p 9.8
        /// 
        /// Given an infinite number of quarters, dimes, nickles, and pennies,
        /// return the number of ways you can represent n cents.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int RepresentingNCents(int n)
        {
            return RepresentingNCents(n, CoinDenomination.quarter);
        }

        private static int RepresentingNCents(int n, CoinDenomination highestCoinDenom)
        {
            if (n < 0)
            {
                return 0;
            }

            int sum = 1;

            CoinDenomination curDenom = highestCoinDenom;


            while (curDenom != CoinDenomination.penny)
            {
                int coinValue = CoinSet.GetValue(curDenom);

                var quotient = n / coinValue;
                var remainder = n % coinValue;

                for (int i = 0; i < quotient; i++)
                {
                    sum += RepresentingNCents(remainder + i * coinValue, CoinSet.GetNextLowerDenomination(curDenom));
                }

                curDenom = CoinSet.GetNextLowerDenomination(curDenom);
            }

            return sum;
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

                    if (values[i, j] > currentHighest)
                    {
                        currentHighest = values[i, j];
                    }
                }
            }

            return currentHighest;
        }
    }
}
