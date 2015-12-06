using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataStructures;

namespace Tests
{
    [TestClass]
    public class CacheTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var c = new StringLengthCounterCache(2);

            c.Lookup("hello");
            c.Lookup("hello");
            c.Lookup("world");
            c.Lookup("How's it going?");
        }

        public class StringLengthCounterCache : Cache<string,int>
        {
            public StringLengthCounterCache(int capacity) : base(capacity)
            {
            }

            protected override int NonCacheLookup(string query)
            {
                return query.Length;
            }
        }
    }
}
