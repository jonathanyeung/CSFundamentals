using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees
{
    public class Node<T>
    {
        public T Value;
        public Node<T> left;
        public Node<T> right;

        public Node(T value)
        {
            this.Value = value;
        }
    }

    public class BinaryTree<T>
    {
        /// <summary>
        /// Represents the root node
        /// </summary>
        public Node<T> root
        {
            get; private set;
        }

        /// <summary>
        /// Adds the value in the next available spot in the tree
        /// </summary>
        /// <param name="t"></param>
        public void Add(T t)
        {
            Queue < Node < T >> Queue = new Queue<Node<T>>();

            Queue.Enqueue(root);

            while (Queue.Count != 0)
            {
                var cur = Queue.Dequeue();
                if (cur.left == null)
                {
                    cur.left = new Node<T>(t);
                    return;
                }
                else if (cur.right == null)
                {
                    cur.right = new Node<T>(t);
                    return;
                }
                else
                {
                    Queue.Enqueue(cur);
                }
            }
        }

        public void RemoveFirst(T t)
        {
            removeFirstInOrder(t, root);
        }

        private void removeFirstInOrder(T t, Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            throw new NotImplementedException();
        }
    }
}
