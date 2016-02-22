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
        public void ProperParentheses()
        {
            var result = DynamicProgramming.ProperParentheses(1);

            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(result.Contains("()"));

            result = DynamicProgramming.ProperParentheses(2);

            Assert.AreEqual(result.Count, 2);
            Assert.IsTrue(result.Contains("()()"));
            Assert.IsTrue(result.Contains("(())"));

            result = DynamicProgramming.ProperParentheses(3);

            Assert.AreEqual(result.Count, 5);
            Assert.IsTrue(result.Contains("()()()"));
            Assert.IsTrue(result.Contains("((()))"));
            Assert.IsTrue(result.Contains("(())()"));
            Assert.IsTrue(result.Contains("()(())"));
            Assert.IsTrue(result.Contains("(()())"));
        }

        [TestMethod]
        public void RepresentingNCents()
        {
            var result = DynamicProgramming.RepresentingNCents(1);

            Assert.AreEqual(result, 1);

            result = DynamicProgramming.RepresentingNCents(5);

            Assert.AreEqual(result, 2);

            result = DynamicProgramming.RepresentingNCents(10);

            Assert.AreEqual(result, 4);

            result = DynamicProgramming.RepresentingNCents(12);

            Assert.AreEqual(result, 4);

            result = DynamicProgramming.RepresentingNCents(15);

            Assert.AreEqual(result, 6);

            result = DynamicProgramming.RepresentingNCents(25);

            Assert.AreEqual(result, 13);

            result = DynamicProgramming.RepresentingNCents(27);

            Assert.AreEqual(result, 13);
        }

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
