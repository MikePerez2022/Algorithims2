using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTreeLibrary
{
    internal class Node<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }
        public int? Height { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
}
