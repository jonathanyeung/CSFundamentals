﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Interviews;

namespace Tests
{
    [TestClass]
    public class SortingTests
    {
        [TestMethod]
        public void QuickSortTwo()
        {
            var testInput = new int[] { 8, 4, 6, 7, 2, 7, 5, 3, 9 };
            //var testInput = new int[] { 6,1,3,2,4,5 };

            Sorting.QuickSortTwo(testInput);

        }

        [TestMethod]
        public void SelectionRankTests()
        {
            var sortedInput = new int[] { 2, 3, 4, 5, 6, 7, 7, 8, 9 };
            var testInput = new int[] { 8, 4, 6, 7, 2, 7, 5, 3, 9 };

            for( int i = 0; i < sortedInput.Length; i++)
            {
                Assert.AreEqual(sortedInput[i], Sorting.SelectionRank(testInput, i + 1));
            }

        }
    }
}