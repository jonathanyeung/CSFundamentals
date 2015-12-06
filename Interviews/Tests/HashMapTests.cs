using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class HashMapTests
    {
        [TestMethod]
        public void BasicHashMapTests()
        {
            var map = new HashMap<int, string>();

            map.Add(1, "one");

            var value = map[1];

            bool threw = false;
            try
            {
                var v = map[0];
            }
            catch(ArgumentOutOfRangeException)
            {
                threw = true;
            }

            Assert.IsTrue(threw);

            threw = false;

            try
            {
                map.Add(1, "notOne");
            }
            catch(ArgumentException)
            {
                threw = true;
            }
            Assert.IsTrue(threw);
        }

        [TestMethod]
        public void BasicMultiHashMapTests()
        {
            var map = new MultiHashMap<int, int, string>();

            map.Add(1, 1, "one");

            var value = map[1,1];

            bool threw = false;
            try
            {
                var v = map[1,2];
            }
            catch (ArgumentOutOfRangeException)
            {
                threw = true;
            }

            Assert.IsTrue(threw);

            threw = false;

            try
            {
                map.Add(1, 1, "notOne");
            }
            catch (ArgumentException)
            {
                threw = true;
            }
            Assert.IsTrue(threw);

            map.Add(1, 2, "oneTwo");
        }
    }
}
