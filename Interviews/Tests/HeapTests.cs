using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class HeapTests
    {
        [TestMethod]
        public void MinHeapBasic()
        {
            const int iterations = 50;
            var minHeap = new MinHeap<int>();

            for (int i = 1; i <= iterations; i++)
            {
                minHeap.Insert(i);
            }

            Assert.AreEqual(iterations, minHeap.Count);


            Assert.AreEqual(1, minHeap.Peek());
            Assert.AreEqual(1, minHeap.Peek());

            for (int i = 1; i <= iterations; i++)
            {
                Assert.AreEqual(i, minHeap.ExtractMin());
            }

            Assert.AreEqual(0, minHeap.Count);
        }

        [TestMethod]
        public void MinHeapRandom()
        {
            int iterations = 10000;
            Random r = new Random();

            var minHeap = new MinHeap<int>();

            for (int i = 0; i < iterations; i++)
            {
                var j = r.Next(1000);

                minHeap.Insert(j);
            }

            //Test Insert Function:
            for (int i = 0; i < iterations; i++)
            {
                var left = 2 * i + 1;
                var right = 2 * i + 2;

                if (left < minHeap.Count)
                {
                    Assert.IsTrue(minHeap.Peek(i) <= minHeap.Peek(left));
                }

                if (right < minHeap.Count)
                {
                    Assert.IsTrue(minHeap.Peek(i) <= minHeap.Peek(right));
                }
            }


            var prev = -1;

            while (minHeap.Count > 0)
            {
                var cur = minHeap.ExtractMin();

                Assert.IsTrue(prev <= cur);

                prev = cur;
            }
        }

        [TestMethod]
        public void MaxHeapBasic()
        {
            const int iterations = 50;
            var maxHeap = new MaxHeap<int>();

            for (int i = 1; i <= iterations; i++)
            {
                maxHeap.Insert(i);
            }

            Assert.AreEqual(iterations, maxHeap.Count);


            Assert.AreEqual(iterations, maxHeap.Peek());
            Assert.AreEqual(iterations, maxHeap.Peek());

            for (int i = iterations; i > 0 ; i--)
            {
                Assert.AreEqual(i, maxHeap.ExtractMax());
            }

            Assert.AreEqual(0, maxHeap.Count);
        }

        [TestMethod]
        public void MaxHeapRandom()
        {
            int iterations = 10000;
            Random r = new Random();

            var maxHeap = new MaxHeap<int>();

            for (int i = 0; i < iterations; i++)
            {
                var j = r.Next(1000);

                maxHeap.Insert(j);
            }

            //Test Insert Function:
            for (int i = 0; i < iterations; i++)
            {
                var left = 2 * i + 1;
                var right = 2 * i + 2;

                if (left < maxHeap.Count)
                {
                    Assert.IsTrue(maxHeap.Peek(i) >= maxHeap.Peek(left));
                }

                if (right < maxHeap.Count)
                {
                    Assert.IsTrue(maxHeap.Peek(i) >= maxHeap.Peek(right));
                }
            }


            var prev = Int32.MaxValue;

            while (maxHeap.Count > 0)
            {
                var cur = maxHeap.ExtractMax();

                Assert.IsTrue(prev >= cur);

                prev = cur;
            }
        }

        //Takes integers from n sorted sources, and combines them in sorted order.
        [TestMethod]
        public void CollateFiles()
        {
            var rand = new Random();

            int sourceLength = 10;
            int sourceCount = 10;

            var sourceList = new List<int>[sourceCount];

            for (int i = 0; i < sourceCount; i++ )
            {
                sourceList[i] = new List<int>();
            }

                foreach (var s in sourceList)
                {
                    var m = rand.Next(5);

                    for (int i = 0; i < sourceLength; i++)
                    {
                        s.Add(i * m);
                    }
                }

            var results = new List<int>();
            var minHeap = new MinHeap<tuple>();

            for (int i = 0; i < sourceLength; i++)
            {
                minHeap.Insert(new tuple(i, sourceList[i][0]));
                sourceList[i].RemoveAt(0);
            }

            while (results.Count < sourceLength * sourceCount)
            {
                var res = minHeap.ExtractMin();
                results.Add(res.value);

                if (sourceList[res.index].Count != 0)
                {
                    minHeap.Insert(new tuple(res.index, sourceList[res.index][0]));
                    sourceList[res.index].RemoveAt(0);
                }
            }
        }

        [TestMethod]
        /// <summary>
        /// Calculates the k closest stars to earth. 
        /// </summary>
        public void ClosestKStars()
        {
            var iterations = 1000;
            var k = 20;

            var distances = new int[1000];

            var rand = new Random();

            for (int i = 0; i < iterations; i++)
            {
                distances[i] = rand.Next(1000);
            }

            var kClosest = new MaxHeap<int>();

            for (int i = 0; i < k; i++)
            {
                kClosest.Insert(distances[i]);
            }

            for (int i = k; i < iterations; i++)
            {
                if (distances[i] < kClosest.Peek())
                {
                    kClosest.ExtractMax();
                    kClosest.Insert(distances[i]);
                }
            }

            var res = new int[k];

            for (int i = 0; i < k; i++)
            {
                res[i] = kClosest.ExtractMax();
            }
        }
    }

    internal class tuple : IComparable
    {
        public int index;
        public int value;

        public tuple(int index, int value)
        {
            this.index = index;
            this.value = value;
        }

        public int CompareTo(object obj)
        {
            if (this.value > ((tuple)obj).value)
            {
                return 1;
            }
            else if (this.value == ((tuple)obj).value)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
