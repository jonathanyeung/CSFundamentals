using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    public class Trees
    {
        public static void PrintSubtree(bNode root)
        {
            if (root == null)
            {
                return;
            }

            List<List<bNode>> depths = new List<List<bNode>>();

            var current = new List<bNode>();

            var q = new Queue<bNode>();

            int nodeCountAtDepth = 1;
            int nextNodeCount = 0;

            q.Enqueue(root);

            while (q.Count > 0)
            {
                bNode cur = q.Dequeue();
                nodeCountAtDepth--;
                current.Add(cur);

                if (cur.left != null)
                {
                    q.Enqueue(cur.left);
                    nextNodeCount++;
                }
                if (cur.right != null)
                {
                    q.Enqueue(cur.right);
                    nextNodeCount++;
                }

                if (nodeCountAtDepth == 0)
                {
                    depths.Add(current);

                    if (nextNodeCount > 0)
                    {
                        current = new List<bNode>();
                        nodeCountAtDepth = nextNodeCount;
                        nextNodeCount = 0;
                    }
                }
            }

            foreach (var depth in depths)
            {
                string line = "";

                foreach(var node in depth)
                {
                    line += node.value;
                    line += " ";
                }

                Console.WriteLine(line);
            }
        }

        public bNode ConstructBST(int[] input)
        {
            Array.Sort(input);
            return construction(input, 0, input.Length - 1);
        }

        public static bool isBST(bNode root)
        {
            var res = traverse(root, int.MinValue);
            return (res != int.MaxValue);
        }

        private static int traverse(bNode root, int prevVal)
        {
            if (prevVal == Int16.MaxValue)
            {
                return prevVal;
            }
            if (root == null)
            {
                return prevVal;
            }

            var val = traverse(root.left, prevVal);

            if (root.value < val)
            {
                return prevVal;
            }
            
            return traverse(root.right, root.value);
        }

        private bNode construction(int[] input, int left, int right)
        {
            if (left == right)
            {
                return new bNode() { value = input[left] };
            }
            else if (right - left == 1)
            {
                var newNode = new bNode();
                newNode.value = input[right];
                newNode.left = construction(input, left, right - 1);
                return newNode;
            }
            else
            {
                bNode newNode = new bNode();
                var mid = (left + right) / 2;
                newNode.value = input[mid];
                newNode.left = construction(input, left, mid - 1);
                newNode.right = construction(input, mid + 1, right);
                return newNode;
            }
        }

        public void InOrderTraversal(bNode root)
        {
            if (root == null)
            {
                return;
            }

            InOrderTraversal(root.left);
            Console.WriteLine("visited node with value {0}", root.value);
            InOrderTraversal(root.right);
        }

        public void PreOrderTraversal(bNode root)
        {
            if (root == null)
            {
                return;
            }

            Console.WriteLine("visited node with value {0}", root.value);
            PreOrderTraversal(root.left);
            PreOrderTraversal(root.right);
        }

        public void PostOrderTraversal(bNode root)
        {
            if (root == null)
            {
                return;
            }
            PostOrderTraversal(root.left);
            PostOrderTraversal(root.right);
            Console.WriteLine("visited node with value {0}", root.value);
        }

        public bNode LeastCommonAncestor(bNode root, int left, int right)
        {
            var leftNode = LeastCommonAncestor(root.left, left, right);

            var rightNode = LeastCommonAncestor(root.right, left, right);

            if (leftNode == null && rightNode == null)
            {
                return null;
            }
            throw new NotImplementedException();
        }
    }

    // Generic node
    public class lNode<T>
    {
        public T value;

        public List<lNode<T>> children;
    }

    /// <summary>
    /// Node for binary tree
    /// </summary>
    public class bNode
    {
        public int value;

        public bNode left;

        public bNode right;
    }

    public class pNode
    {
        public int value;

        public List<pNode> children;

        public pNode parent;
    }
}
