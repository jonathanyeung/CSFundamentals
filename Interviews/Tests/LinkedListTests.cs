using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;
using System.Collections.Generic;
using Interviews;

namespace Tests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void ReverseListTest()
        {
            // One Value
            var head = new Node<int>(1);

            var res = LinkedLists.ReverseList(head);

            Assert.AreEqual(1, res.value);

            head.Append(new Node<int>(2));

            res = LinkedLists.ReverseList(head);

            Assert.AreEqual(2, res.value);

            Assert.AreEqual(1, res.next.value);

            res = LinkedLists.ReverseList(res);

            res.Append(new Node<int>(3));

            res = LinkedLists.ReverseList(res);

            Assert.AreEqual(3, res.value);

            Assert.AreEqual(2, res.next.value);

            Assert.AreEqual(1, res.next.next.value);
        }
    }
}
