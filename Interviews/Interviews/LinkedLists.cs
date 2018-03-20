using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{

    public class Node<T>
    {
        public T value;
        public Node<T> next;

        public Node(T value)
        {
            this.value = value;
            next = null;
        }

        public void Append(Node<T> node)
        {
            var cur = this;

            while(cur.next != null)
            {
                cur = cur.next;
            }

            cur.next = node;
        }
    }

    public static class LinkedLists
    {
        public static Node<T> ReverseList<T>(Node<T> head)
        {
            Node<T> cur = head;
            Node<T> next = head;
            Node<T> prev = null;

            while (cur != null)
            {
                next = cur.next;
                cur.next = prev;
                prev = cur;
                cur = next;
            }

            return prev;
        }

        public static Node<T> RemoveDuplicates<T>(Node<T> head)
        {
            var cur = head;
            var test = head.next;
            var test_Prev = head;
            while (cur != null)
            {
                while (test != null)
                {
                    if (test.value.Equals(cur.value))
                    {
                        test_Prev.next = test.next;
                        test = test.next;
                    }
                    else
                    {
                        test = test.next;
                        test_Prev = test_Prev.next;
                    }
                }
                cur = cur.next;
            }

            return head;
        }



    }
}
