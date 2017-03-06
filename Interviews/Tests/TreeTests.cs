using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Trees;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void GenericFunctionsTest()
        {
            var root = new BinarySearchTree<int>(0);
            for (int i = 1; i <= 10; i++)
            {
                root.Insert(i);
            }

            Debug.WriteLine("Original:");
            PrintTree(root);
            BinarySearchTree<int>.Balance(ref root);
            Debug.WriteLine("Balanced:");
            PrintTree(root);

            for (int i = 1; i <= 10; i++)
            {
                Assert.IsNotNull(root.Find(i));
            }

            Assert.IsNull((root.Find(0)).InOrderPredecessor());
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual((root.Find(i)).InOrderSuccessor().value, i + 1);
            }

            Assert.IsNull((root.Find(10)).InOrderSuccessor());
            for (int i = 1; i <= 10; i++)
            {
                Assert.AreEqual((root.Find(i)).InOrderPredecessor().value, i - 1);
            }

            for (int i = 1; i < 10; i++)
            {
                Debug.WriteLine("Deleting {0}", i);
                root.Delete(i);
                PrintTree(root);
                Assert.AreEqual((root.Find(0)).InOrderSuccessor().value, i + 1);
            }

            root.Delete(10);

            Assert.IsNull(root.InOrderPredecessor());
            Assert.IsNull(root.InOrderSuccessor());

            //root.Delete(0);
            //Assert.IsNull(root);
        }

        [TestMethod]
        public void TestBalancing()
        {
            // Need to manually verify the balancing through Debug output.
            for (int i = 0; i < 5; i++)
            {
                var root = CreateTree(8);
                Debug.WriteLine("Original:");
                PrintTree(root);
                BinarySearchTree<int>.Balance(ref root);
                Debug.WriteLine("Balanced:");
                PrintTree(root);
            }
        }

        [TestMethod]
        public void IsBalancedTest()
        {
            var root = CreateTree(10);
            BinarySearchTree<int>.Balance(ref root);
            Debug.WriteLine("Balanced:");
            PrintTree(root);
            Assert.IsTrue(BinarySearchTree<int>.IsBalanced(root));

            var curNode = root;
            while (curNode.Left != null)
            {
                curNode = curNode.Left;
            }

            curNode.Left = new BinarySearchTree<int>(int.MinValue);
            curNode.Left.Left = new BinarySearchTree<int>(int.MinValue);
            curNode.Left.Left.Left = new BinarySearchTree<int>(int.MinValue);

            Debug.WriteLine("Unbalanced:");
            PrintTree(root);
            Assert.IsFalse(BinarySearchTree<int>.IsBalanced(root));
        }


        private BinarySearchTree<int> CreateTree(int elementCount, int seed = int.MaxValue)
        {
            if (seed == int.MaxValue)
            {
                seed = DateTime.UtcNow.GetHashCode();
            }

            Debug.WriteLine("Seed to create tree is: {0}", seed);

            var rand = new Random(seed);
           
            var init = rand.Next(0, elementCount);
            var root = new BinarySearchTree<int>(init);

            for (int i = 0; i < elementCount; i++)
            {
                root.Insert(rand.Next(0, elementCount));
            }

            return root;

        }

        private void PrintTree<T>(BinarySearchTree<T> node) where T:IComparable
        {
            if (node == null)
            {
                return;
            }

            var nodeList = new List<List<BinarySearchTree<T>>>();
            var nodeQueue = new Queue<BinarySearchTree<T>>();

            nodeQueue.Enqueue(node);

            bool HasChildren = true;
            while (nodeQueue.Count > 0 && HasChildren)
            {
                HasChildren = false;
                var rowList = new List<BinarySearchTree<T>>();

                for (int i = 0; i < Math.Pow(2, nodeList.Count); i++)
                {
                    var curNode = nodeQueue.Dequeue();
                    rowList.Add(curNode);

                    if (curNode == null)
                    {
                        nodeQueue.Enqueue(null);
                        nodeQueue.Enqueue(null);
                    }
                    else
                    {
                        HasChildren = true;
                        nodeQueue.Enqueue(curNode.Left);
                        nodeQueue.Enqueue(curNode.Right);
                    }
                }

                if (HasChildren)
                {
                    nodeList.Add(rowList);
                }
            }

            for(int i = 0; i < nodeList.Count; i++)
            {
                var outputStr = "";

                outputStr = outputStr.PadRight((int)Math.Pow(2, nodeList.Count - i - 1) - 1);

                foreach (var element in nodeList[i])
                {
                    if (element == null)
                    {
                        outputStr += 'x';
                    }
                    else
                    {
                        outputStr += element.value.ToString();
                    }

                    outputStr = outputStr.PadRight(outputStr.Length + (int)Math.Pow(2, nodeList.Count - i) - 1);
                }

                outputStr = i + "|  " + outputStr;
                Debug.WriteLine(outputStr);
            }
        }
    }
}
