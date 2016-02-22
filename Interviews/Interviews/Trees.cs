using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;
using DataStructures.Trees;

namespace Interviews
{
    public class Trees<T>
    {
        #region Fundamentals

        public void InOrderTraversal(Node<T> root)
        {
            if (root == null)
            {
                return;
            }

            InOrderTraversal(root.left);
            Console.WriteLine("visited node with value {0}", root.Value);
            InOrderTraversal(root.right);
        }

        public void PreOrderTraversal(Node<T> root)
        {
            if (root == null)
            {
                return;
            }

            Console.WriteLine("visited node with value {0}", root.Value);
            PreOrderTraversal(root.left);
            PreOrderTraversal(root.right);
        }

        public void PostOrderTraversal(Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            PostOrderTraversal(root.left);
            PostOrderTraversal(root.right);
            Console.WriteLine("visited node with value {0}", root.Value);
        }

        #endregion

        #region Cracking the Coding Interview

        //NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview; p 4.1
        /// Determine if a tree is balanced.  A balanced tree a difference of at 
        /// most 1 between the heights of its left and right subtrees.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsBalanced(BinaryTree<T> root)
        {
            if (getHeightOrDifference(root.root) == -1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Helper method for IsBalanced()
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static int getHeightOrDifference(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            var leftHeight = getHeightOrDifference(node.left);

            var rightHeight = getHeightOrDifference(node.right);

            if (leftHeight == -1 || rightHeight == -1)
            {
                return -1;
            }

            if (Math.Abs(leftHeight - rightHeight) > 1)
            {
                return -1;
            }

            return (leftHeight > rightHeight) ? leftHeight + 1 : rightHeight + 1;
        }

        //NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview p 4.3
        /// Given a sorted array, write an algorithm to create a binary search
        /// tree with minimal height.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Node<T> ConstructBST(T[] input)
        {
            Array.Sort(input);
            return construction(input, 0, input.Length - 1);
        }

        private Node<T> construction(T[] input, int left, int right)
        {
            if (left == right)
            {
                return new Node<T>(input[left]);
            }
            else if (right - left == 1)
            {
                var newNode = new Node<T>(input[right]);
                newNode.left = construction(input, left, right - 1);
                return newNode;
            }
            else
            {
                var mid = (left + right) / 2;
                var newNode = new Node<T>(input[mid]);
                newNode.left = construction(input, left, mid - 1);
                newNode.right = construction(input, mid + 1, right);
                return newNode;
            }
        }

        //NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview; p 4.5
        /// Check if a binary tree is a binary search tree
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool isBST(bNode root)
        {
            var res = traverse(root, int.MinValue);
            return (res != int.MaxValue);
        }

        private static int traverse(bNode root, int prevVal)
        {
            if (prevVal == int.MaxValue)
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
                return int.MaxValue;
            }

            return traverse(root.right, root.value);
        }

        //NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview; p 4.6
        /// Return the in order successor to a node in a binary tree.
        /// </summary>
        /// <param name="curNode"></param>
        /// <returns></returns>
        public static Node<T> NextNode(Node<T> curNode)
        {
            if (curNode.right != null)
            {
                return LeftMostNode(curNode.right);
            }

            while (curNode.parent != null && curNode == curNode.parent.right)
            {
                curNode = curNode.parent;
            }

            // This means we're at the last node of the tree.
            if (curNode.parent == null)
            {
                return null;
            }

            return curNode;
        }

        /// <summary>
        /// Returns the left most subnode of the tree. Helper method for NextNode()
        /// </summary>
        /// <param name="curNode"></param>
        /// <returns></returns>
        private static Node<T> LeftMostNode(Node<T> curNode)
        {
            if (curNode == null)
            {
                return null;
            }

            while (curNode.left != null)
            {
                curNode = curNode.left;
            }

            return curNode;
        }


        // NOT TESTED
        /// <summary>
        /// Cracking the Coding Interview; p 4.7
        /// Find the least common ancestor of 2 nodes in a binary tree.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Node<T> LeastCommonAncestor(Node<T> root, Node<T> A, Node<T> B)
        {
            bool found;
            var lca = NodeFinder(root, A, B, out found);

            return (found) ? lca : null;
        }

        /// <summary>
        /// Helper method for LCA()
        /// </summary>
        /// <param name="root"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="rootFound"></param>
        /// <returns></returns>
        private static Node<T> NodeFinder(Node<T> root, Node<T> A, Node<T> B, out bool rootFound)
        {
            if (root == null)
            {
                rootFound = false;
                return null;
            }
            if (root == A && root == B)
            {
                rootFound = true;
                return root;
            }

            // Search the left and right sides.
            bool leftFound;
            var left = NodeFinder(root.left, A, B, out leftFound);
            if (leftFound)
            {
                rootFound = true;
                return left;
            }

            bool rightFound;
            var right = NodeFinder(root.right, A, B, out rightFound);
            if (rightFound)
            {
                rootFound = true;
                return right;
            }

            // A and B came from opposite sides; this node is the LCA.
            if (right != null && left != null)
            {
                rootFound = true;
                return root;
            }

            // If one of the values is at this node, then this is the LCA
            // if one of its children contained the opposite value.
            if (root == A)
            {
                if (left == B || right == B)
                {
                    rootFound = true;
                    return root;
                }
            }
            if (root == B)
            {
                if (left == A || right == A)
                {
                    rootFound = true;
                    return root;
                }
            }

            // Otherwise, only one value was found:
            rootFound = false;
            return (left == null) ? right : left;
        }

        #endregion

        #region Helpers
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


#endregion




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
