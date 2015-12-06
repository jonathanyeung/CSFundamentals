using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class Recursion
    {
        /// <summary>
        /// Child running up n steps, can hop 1, 2, or 3 steps at a time.
        /// How many ways can the child run up the steps?
        /// </summary>
        [TestMethod]
        public void ChildRunningUpSteps()
        {
            Assert.AreEqual(1, HopCount(1));
            Assert.AreEqual(2, HopCount(2));
            Assert.AreEqual(4, HopCount(3));
        }

        internal int HopCount(int steps)
        {
            if (steps < 0)
            {
                return 0;
            }
            if (steps == 0)
            {
                return 1;
            }
            return HopCount(steps - 1) + HopCount(steps - 2) + HopCount(steps - 3);
        }

        /// <summary>
        /// Given an x by y grid, how many different paths can a robot travel from (0,0)
        /// to (x,y)?  The robot can only travel down and to the right.
        /// </summary>
        [TestMethod]
        public void RobotXY()
        {
            Assert.AreEqual(2, numberOfSteps(1, 1));

            Assert.AreEqual(6, numberOfSteps(2, 2));
        }

        internal int numberOfSteps(int x, int y)
        {
            if (x <= 0 || y <= 0)
            {
                return 1;
            }

            return numberOfSteps(x - 1, y) + numberOfSteps(x, y - 1);
        }

        [TestMethod]
        public void stringPermutations()
        {
            List<string> results = new List<string>();

            permute(results, "", "hello");
        }

        public void permute(List<string> results, string prefix, string suffix)
        {
            if (suffix == "")
            {
                results.Add(prefix);
            }

            foreach (char c in suffix)
            {
                permute(results, prefix + c.ToString(), suffix.Remove(suffix.IndexOf(c), 1));
            }
        }

        [TestMethod]
        public void ValidParentheses()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            addParenthesis(results, 5, 5, "");

            foreach (var k in results.Keys)
            {
                Debug.WriteLine(k);
            }
        }

        internal void addParenthesis(Dictionary<string, int> results, int openRemaining, int closeRemaining, string current)
        {
            if (openRemaining == 0)
            {
                for (int i = 0; i < closeRemaining; i++)
                {
                    current += ')';
                }

                if (results.ContainsKey(current))
                {
                    results[current]++;
                }
                else
                {
                    results.Add(current, 1);
                }
            }

            else
            {
                if (isValid(current + ')'))
                {
                    addParenthesis(results, openRemaining, closeRemaining - 1, current + ')');
                }
                addParenthesis(results, openRemaining - 1, closeRemaining, current + '(');
            }
        }

        internal bool isValid(string s)
        {
            var runningCount = 0;

            foreach (char c in s)
            {
                if (c == '(')
                {
                    runningCount++;
                }
                else if (c == ')')
                {
                    runningCount--;
                }
                if (runningCount < 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Given an amount of money, calculate how many ways there are to represent
        /// that amount of change in quarters, nickels, dimes, and pennies.
        /// </summary>
        [TestMethod]
        public void ComputeChangeCombinations()
        {
            Debug.WriteLine(changeCount(5, coins.Quarter));
            Debug.WriteLine(changeCount(100, coins.Quarter));
            Debug.WriteLine(changeCount(4, coins.Quarter));
            Debug.WriteLine(changeCount(10, coins.Quarter));
            Debug.WriteLine(changeCount(1000, coins.Quarter));
        }

        internal int changeCount(int amount, coins maxDenom)
        {
            coins nextDenom = coins.Penny;

            if (maxDenom == coins.Penny)
            {
                return 1;
            }

            switch (maxDenom)
            {
                case coins.Quarter:
                    nextDenom = coins.Dime;
                    break;
                case coins.Dime:
                    nextDenom = coins.Nickel;
                    break;
                case coins.Nickel:
                    nextDenom = coins.Penny;
                    break;
                default:
                    break;
            }

            int possibilities = 0;

            if (amount >= (int)maxDenom)
            {
                for (int i = 0; i <= amount / (int)maxDenom; i++)
                {
                    possibilities += changeCount(amount - i * (int)maxDenom, nextDenom);
                }
            }
            else
            {
                possibilities = changeCount(amount, nextDenom);
            }

            return possibilities;
        }

        internal enum coins : int
        {
            Quarter = 25,
            Dime = 10,
            Nickel = 5,
            Penny = 1
        }

        public static int recursionCount = 0;
        public static int saves = 0;

        /// <summary>
        /// You can stack boxes on top of each other, only if the bottom
        /// box is larger than the top box in all 3 dimensions.  What is 
        /// the largest stack of boxes you can make?
        /// </summary>
        [TestMethod]
        public void LargestStackOfBoxes()
        {
            var BoxCount = 20;
            var Boxes = new Box[BoxCount];
            var heights = new int[BoxCount];

            for (int i = 0; i < BoxCount; i++ )
            {
                heights[i] = -1;
            }
            for (int i = 0; i < BoxCount; i++)
            {
                Thread.Sleep(500);
                var newBox = new Box();

                Debug.WriteLine("Box {0}: Height={1}, Width={2}, Length={3}", i, newBox.height, newBox.width, newBox.length);

                Boxes[i] = newBox;
            }

            var greatestHeight = 0;

            for (int i = 0; i < BoxCount; i++)
            {
                var height = GetHighestStack(Boxes, heights, i);
                Debug.WriteLine("HeighestStack at index {0} is {1}", i, height);
                if (height > greatestHeight)
                {
                    greatestHeight = height;
                }
            }

            Debug.WriteLine("Heighest Stack is of height {0}", greatestHeight);
            Debug.WriteLine("Recursion Count = {0}, Saves = {1}", recursionCount, saves);

        }

        internal int GetHighestStack(Box[] Boxes, int[] heights, int index)
        {
            if (heights[index] != -1 )
            {
                saves++;
                return heights[index];
            }

            var baseBox = Boxes[index];

            recursionCount++;

            var highestStack = baseBox.height;

            for (int i = 0; i < Boxes.Length; i++ )
            {
                if (Boxes[i].CompareTo(baseBox) < 0)
                {
                    var stackHeight = baseBox.height + GetHighestStack(Boxes, heights, i);

                    if (stackHeight > highestStack)
                    {
                        highestStack = stackHeight;
                    }
                }
            }

            heights[index] = highestStack;

            return highestStack;
        }
    }

    internal class Box : IComparable
    {
        public int height;
        public int width;
        public int length;

        public Box(int i)
        {
            height = i;
            width = i;
            length = i;
        }
        public Box()
        {
            const int MaxValue = 10;
            var Rand = new Random();

            height = Rand.Next(1, MaxValue);
            width = Rand.Next(1, MaxValue);
            length = Rand.Next(1, MaxValue);
        }

        public int CompareTo(object obj)
        {
            var res = (Box)obj;

            if (this.height > res.height && this.width > res.width && this.length > res.length)
            {
                return 1;
            }
            else if (this.height < res.height && this.width < res.width && this.length < res.length)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
