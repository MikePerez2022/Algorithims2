using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary
{
    public class DLNode<T> where T : IComparable<T>
    {
        public T data { get; set; }
        public DLNode<T>? next;
        public DLNode<T>? prev;

        public DLNode(T value)
        {
            data = value;
            next = default;
            prev = default;
        }
    }
}
