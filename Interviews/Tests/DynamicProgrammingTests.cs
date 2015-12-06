using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interviews;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class DynamicProgrammingTests
    {
        [TestMethod]
        public void Knapsack()
        {
            var result = DynamicProgramming.Knapsack(new int[] { 1, 6, 9 }, 12);

            Assert.AreEqual(result, 12);

            result = DynamicProgramming.Knapsack(new int[] { 3, 6, 9 }, 13);

            Assert.AreEqual(result, 12);

            result = DynamicProgramming.Knapsack(new int[] { 3, 4, 4, 8 }, 9);

            Assert.AreEqual(result, 9);

            result = DynamicProgramming.Knapsack(new int[] { 3, 4, 4, 8 }, 17);

            Assert.AreEqual(result, 16);
        }

        /// <summary>
        /// For a given array, return the longest list of integers from the array,
        /// where the integers are non-decreasing.
        /// </summary>
        [TestMethod]
        public void LongestNonDecreasingSubsequenceTest()
        {

        }

        internal List<int> LongestNonDecreasingSubsequence(int[] input)
        {
            throw new NotImplementedException();
        }
    }
}
