using Interviews;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class SearchingTests
    {
        [TestMethod]
        public void BinarySearchOnRotatedArrayTest()
        {
            var input = new int[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 };

            Assert.AreEqual(-1, Searching.BinarySearchOnRotatedArray(input, 20));

            Assert.AreEqual(0, Searching.BinarySearchOnRotatedArray(input, 7));
            Assert.AreEqual(1, Searching.BinarySearchOnRotatedArray(input, 8));
            Assert.AreEqual(2, Searching.BinarySearchOnRotatedArray(input, 9));
            Assert.AreEqual(3, Searching.BinarySearchOnRotatedArray(input, 1));
            Assert.AreEqual(4, Searching.BinarySearchOnRotatedArray(input, 2));
            Assert.AreEqual(5, Searching.BinarySearchOnRotatedArray(input, 3));
            Assert.AreEqual(6, Searching.BinarySearchOnRotatedArray(input, 4));
            Assert.AreEqual(7, Searching.BinarySearchOnRotatedArray(input, 5));
            Assert.AreEqual(8, Searching.BinarySearchOnRotatedArray(input, 6));

            input = new int[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 };

            Assert.AreEqual(0, Searching.BinarySearchOnRotatedArray(input, 4));
            Assert.AreEqual(1, Searching.BinarySearchOnRotatedArray(input, 5));
            Assert.AreEqual(2, Searching.BinarySearchOnRotatedArray(input, 6));
            Assert.AreEqual(3, Searching.BinarySearchOnRotatedArray(input, 7));
            Assert.AreEqual(4, Searching.BinarySearchOnRotatedArray(input, 8));
            Assert.AreEqual(5, Searching.BinarySearchOnRotatedArray(input, 9));
            Assert.AreEqual(6, Searching.BinarySearchOnRotatedArray(input, 1));
            Assert.AreEqual(7, Searching.BinarySearchOnRotatedArray(input, 2));
            Assert.AreEqual(8, Searching.BinarySearchOnRotatedArray(input, 3));
        }
    }
}
