

using System.Text;

namespace DataStructuresLibrary
{
    public class LinkedLists<T> where T : IComparable<T>
    {
        public Node<T>? Head;
        public Node<T>? tail;
        public int count;

        // for c# comments use -> ///

        public void Append(T value)
        {
            var newNode = new Node<T>(value);
            count++;

            if (Head == null)
            {
                Head = newNode;
                tail = newNode;
                return;
            }

            tail.next = newNode;
            tail = newNode;
        }

        public void Insert(T value, int index)
        {
            if(index >= count || index < 0) throw new IndexOutOfRangeException();
            var newNode = new Node<T>(value);
            count++;

            var currentNode = Head;
            for(int i = 0; i < index-1; i++)
            {
                currentNode = currentNode.next;
            }

            newNode.next = currentNode.next;
            currentNode.next = newNode;
        }

        public T Get(int index)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();

            var currentNode = Head;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.next;
            }

            return currentNode.data;
        }

        public T? Remove()
        {
            if (Head == null) return default;

            if(tail == Head) tail = null;

            T valueRemoved = Head.data;
            Head = Head.next;
            count--;

            return valueRemoved;
        }

        public T RemoveAt(int index)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();
            if (index == 0) return Remove();
            if (index == count) return RemoveLast();

            var currentNode = Head;
            for (int i = 0; i < index - 1; i++)
            {
                currentNode = currentNode.next;
            }

            T valueRemoved = currentNode.next.data;
            currentNode.next = currentNode.next.next;
            count--;

            return valueRemoved;
        }

        public T? RemoveLast()
        {
            var currentNode = Head;
            if (currentNode == null) return default;
            for (int i = 0; i < count - 2; i++)
            {
                currentNode = currentNode.next;
            }

            T valueRemoved = currentNode.next.data;
            currentNode.next = null;
            tail = currentNode;
            count--;

            return valueRemoved;
        }

        public void RemoveAll(T value)//O(n)
        {
            var currentNode = Head;
            while(currentNode != null && currentNode.data.CompareTo(value) == 0)//O(n)
            {
                Head = Head.next;
                currentNode = Head;
                count--;
            }

            while (currentNode != null)//O(n)
            {
                while(currentNode.next != null && currentNode.next.data.CompareTo(value) == 0)//O(n)
                {
                    currentNode.next = currentNode.next.next;
                    count--;
                }
                currentNode = currentNode.next;
            }
        }

        public string ToString()
        {
            if (count == 0) return "";

            var currentNode = Head;
            string result = "";

            while(currentNode != null)
            {
                result += (currentNode.next == null) ? currentNode.data : currentNode.data + ", ";
                currentNode = currentNode.next;
            }

            return result;
        }

        public void Clear()
        {
            while (Head != null)
            {
                Remove();
            }
        }

        public int Search(T value)
        {
            var currentNode = Head;
            for(int i = 0; i < count-1; i++)
            {
                if(currentNode.data.CompareTo(value) == 0) return i;
                currentNode = currentNode.next;
            }
            return -1;
        }
    }
}
