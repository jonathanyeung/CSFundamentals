using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees
{
    public class BinarySearchTree<T> where T : IComparable
    {
        public BinarySearchTree<T> Left;
        public BinarySearchTree<T> Right;
        public BinarySearchTree<T> Parent;

        public T value
        {
            get;
            private set;
        }

        public BinarySearchTree(T value)
        {
            this.value = value;
            Parent = null;
        }

        private BinarySearchTree(T value, BinarySearchTree<T> parent)
        {
            this.value = value;
            Parent = parent;
        }

        public void Insert(T value)
        {
            if (value.CompareTo(this.value) <= 0)
            {
                if (Left == null)
                {
                    Left = new BinarySearchTree<T>(value, this);
                    Left.Parent = this;
                }
                else
                {
                    Left.Insert(value);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = new BinarySearchTree<T>(value, this);
                    Right.Parent = this;
                }
                else
                {
                    Right.Insert(value);
                }
            }
        }

        public void Delete(T value)
        {
            var node = this.Find(value);

            if (node != null)
            {
                DeleteNode(ref node);
            }
        }

        public BinarySearchTree<T> Find(T value)
        {
            if (this.value.CompareTo(value) == 0)
            {
                return this;
            }
            else if (value.CompareTo(this.value) < 0 && this.Left != null)
            {
                return Left.Find(value);
            }
            else if (value.CompareTo(this.value) > 0 && this.Right != null)
            {
                return Right.Find(value);
            }
            else
            {
                return null;
            }
        }

        private BinarySearchTree<T> RightMostChild()
        {
            var cur = this;

            while (cur.Right != null)
            {
                cur = cur.Right;
            }
            return cur;
        }

        public BinarySearchTree<T> InOrderPredecessor()
        {
            if (Left != null)
            {
                return Left.RightMostChild();
            }

            if (Parent == null)
            {
                return null;
            }

            var cur = this;
            while (cur.Parent != null && cur == cur.Parent.Left)
            {
                cur = cur.Parent;
            }

            return cur.Parent;
        }

        private BinarySearchTree<T> LeftMostChild()
        {
            var cur = this;

            while (cur.Left != null)
            {
                cur = cur.Left;
            }

            return cur;
        }

        public BinarySearchTree<T> InOrderSuccessor()
        {
            if (Right != null)
            {
                return Right.LeftMostChild();
            }

            if (Parent == null)
            {
                return null;
            }

            var cur = this;
            while (cur.Parent != null && cur == cur.Parent.Right)
            {
                cur = cur.Parent;
            }

            return cur.Parent;
        }

        private void DeleteNode(ref BinarySearchTree<T> node)
        {
            if (node == null)
            {
                return;
            }

            // Find the inorder predecessor 
            if (node.Right == null && node.Left == null)
            {
                if (node.Parent != null)
                {
                    if (node == node.Parent.Left)
                    {
                        node.Parent.Left = null;
                    }
                    else
                    {
                        node.Parent.Right = null;
                    }
                }
                else
                {
                    // Deleting the last element in the tree:
                    node = null;
                }
            }
            // 1/2 of the children are leaf nodes:
            else if (node.Right == null || node.Left == null)
            {
                if (node.Parent != null)
                {
                    if (node == node.Parent.Left)
                    {
                        if (node.Right != null)
                        {
                            node.Parent.Left = node.Right;
                            node.Right.Parent = node.Parent;
                        }
                        else
                        {
                            node.Parent.Left = node.Left;
                            node.Left.Parent = node.Parent;
                        }
                    }
                    else
                    {
                        if (node.Right != null)
                        {
                            node.Parent.Right = node.Right;
                            node.Right.Parent = node.Parent;
                        }
                        else
                        {
                            node.Parent.Right = node.Left;
                            node.Left.Parent = node.Parent;
                        }
                    }
                }
                else
                {
                    var temp = (node.Right == null) ? node.Left : node.Right;
                    node.value = temp.value;
                    node.Left = temp.Left;
                    node.Right = temp.Right;
                }
            }
            // Both children are not leafs. Copy value of in-order predecessor
            // to current node, and then recursively call delete on the value of
            // the in order predecessor.
            else
            {
                var predecessor = node.InOrderPredecessor();
                node.value = predecessor.value;
                node.Left.Delete(predecessor.value);
            }
        }

        public static void Balance(ref BinarySearchTree<T> root)
        {
            if (root == null)
            {
                return;
            }

            List<BinarySearchTree<T>> list = new List<BinarySearchTree<T>>();

            // Traverse the tree in order.
            var searchStack = new Stack<BinarySearchTree<T>>();
            var curNode = root;

            while (true)
            {
                if (curNode == null)
                {
                    if (searchStack.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        var temp = searchStack.Pop();
                        list.Add(temp);
                        curNode = temp.Right;
                    }
                }
                else if (curNode.Left != null)
                {
                    searchStack.Push(curNode);
                    curNode = curNode.Left;
                }
                else
                {
                    list.Add(curNode);
                    curNode = curNode.Right;
                }
            }

            // Now, build the new tree off of the list:
            root = BuildBalancedTree(list, 0, list.Count - 1);
        }

        private static BinarySearchTree<T> BuildBalancedTree(List<BinarySearchTree<T>> list, int left, int right)
        {
            BinarySearchTree<T> returnNode;

            if (left > right)
            {
                return null;
            }

            var mid = (left + right) / 2;
            returnNode = new BinarySearchTree<T>(list[mid].value);
            returnNode.Left = BuildBalancedTree(list, left, mid - 1);
            if (returnNode.Left != null)
            {
                returnNode.Left.Parent = returnNode;
            }

            returnNode.Right = BuildBalancedTree(list, mid + 1, right);
            if (returnNode.Right != null)
            {
                returnNode.Right.Parent = returnNode;
            }

            return returnNode;
        }

        public static bool IsBalanced(BinarySearchTree<T> root)
        {
            return (-1 != _IsBalanced(root));
        }

        private static int _IsBalanced(BinarySearchTree<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            var leftResult = _IsBalanced(node.Left);
            var rightResult = _IsBalanced(node.Right);

            if (leftResult == -1 || rightResult == -1)
            {
                return -1;
            }

            if (Math.Abs(leftResult - rightResult) > 1)
            {
                return -1;
            }

            return 1 + Math.Max(leftResult, rightResult);
        }
    }
}
