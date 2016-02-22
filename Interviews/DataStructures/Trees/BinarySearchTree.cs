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

        public BinarySearchTree(T value)
        {
            this.value = value;
            this.Parent = null;
        }

        private BinarySearchTree(T value, BinarySearchTree<T> parent)
        {
            this.value = value;
            this.Parent = parent;
        }

        public T value
        {
            get;
            private set;
        }

        public void Insert(T value)
        {
            if (value.CompareTo(value) <= 0)
            {
                if (Left == null)
                {
                    Left = new BinarySearchTree<T>(value, this);
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
                }
                else
                {
                    Right.Insert(value);
                }
            }
        }

        public void Delete(T value)
        {
            if (value.CompareTo(this.value) < 0)
            {
                if (Left != null)
                {
                    Left.Delete(value);
                }
            }
            else if (value.CompareTo(this.value) > 0)
            {
                if (Right != null)
                {
                    Right.Delete(value);
                }
            }
            else
            {
                // Find the inorder predecessor 
                if (Right == null && Left == null)
                {
                    // How do I delete a node??
                    throw new NotImplementedException();
                }
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
    }
}
