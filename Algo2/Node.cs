using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary
{
    public class Node<T> where T : IComparable<T>
    {
        public T data { get; set; }
        public Node<T>? next;

        public Node(T value)
        {
            data = value;
            next = default;
        }
    }

    public class DoubleNode<T> where T : IComparable<T>
    {
        public T data { get; set; }
        public DoubleNode<T>? previous;
        public DoubleNode<T>? next;

        public DoubleNode(T value)
        {
            data = value;
            next = default;
            previous = default;
        }
    }
}
