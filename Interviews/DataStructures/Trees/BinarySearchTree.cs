using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees
{
    public class BinarySearchTree<T> where T : IComparable
    {
        private BinarySearchTree<T> _left;
        private BinarySearchTree<T> _right;
        private BinarySearchTree<T> _parent;

        public BinarySearchTree(T value, BinarySearchTree<T> parent)
        {
            this.value = value;
            this._parent = parent;
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
                if (_left == null)
                {
                    _left = new BinarySearchTree<T>(value, this);
                }
                else
                {
                    _left.Insert(value);
                }
            }
            else
            {
                if (_right == null)
                {
                    _right = new BinarySearchTree<T>(value, this);
                }
                else
                {
                    _right.Insert(value);
                }
            }
        }

        public void Delete(T value)
        {
            if (value.CompareTo(this.value) < 0)
            {
                if (_left != null)
                {
                    _left.Delete(value);
                }
            }
            else if (value.CompareTo(this.value) > 0)
            {
                if (_right != null)
                {
                    _right.Delete(value);
                }
            }
            else
            {
                // Find the inorder predecessor 
                if (_right == null && _left == null)
                {
                    // How do I delete a node??
                    throw new NotImplementedException();
                }
            }
        }
    }
}
