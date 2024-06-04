

using System.Text;

namespace DataStructuresLibrary
{
    public class DoubleLinkedLists<T> where T : IComparable<T>
    {
        public DoubleNode<T>? Head;
        public DoubleNode<T>? tail;
        public int count;

        // for c# comments use -> ///

        public void Append(T value)
        {
            var newNode = new DoubleNode<T>(value);
            count++;

            if (Head == null)
            {
                Head = newNode;
                tail = newNode;
                return;
            }

            newNode.previous = tail;
            tail.next = newNode;
            tail = newNode;
        }

        public void Insert(T value, int index)
        {
            if(index >= count || index < 0) throw new IndexOutOfRangeException();
            var newNode = new DoubleNode<T>(value);

            DoubleNode<T> currentNode;

            if (index < (count - 1) / 2)
            {
                currentNode = Head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.next;
                }
            }
            else
            {
                currentNode = tail;
                for (int i = count - 1; i > index; i--)
                {
                    currentNode = currentNode.previous;
                }
            }

            count++;
            currentNode.previous.next = newNode;
            newNode.previous = currentNode.previous;
            currentNode.previous = newNode;
            newNode.next = currentNode;
        }

        public T Get(int index)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();

            DoubleNode<T> currentNode;

            if (index < (count - 1) / 2)
            {
                currentNode = Head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.next;
                }
            }
            else
            {
                currentNode = tail;
                for (int i = count - 1; i > index; i--)
                {
                    currentNode = currentNode.previous;
                }
            }

            return currentNode.data;
        }

        public T? Remove()
        {
            if (Head == null) return default;

            if(tail == Head) tail = null;

            T valueRemoved = Head.data;
            Head = Head.next;
            if(Head != null) Head.previous = null;
            count--;

            return valueRemoved;
        }

        public T RemoveAt(int index)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();
            if (index == 0) return Remove();
            if (index == count) return RemoveLast();

            DoubleNode<T> currentNode;

            if (index < (count - 1)/2)
            {
                currentNode = Head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.next;
                }
            }
            else
            {
                currentNode = tail;
                for (int i = count-1; i > index; i--)
                {
                    currentNode = currentNode.previous;
                }
            }

            

            T valueRemoved = currentNode.data;
            currentNode.next.previous = currentNode.previous;
            currentNode.previous.next = currentNode.next;
            currentNode.next = null;
            currentNode.previous = null;
            count--;

            return valueRemoved;
        }

        public T? RemoveLast()
        {
            if (Head == null) return default;

            if (tail == Head) tail = null;

            T valueRemoved = tail.data;
            tail = tail.previous;
            tail.next = null;
            count--;

            return valueRemoved;
        }

        public void RemoveAll(T value)//O(n)
        {
            var currentNode = Head;
            while(currentNode != null)
            {
                if (currentNode.data.CompareTo(value) == 0)
                {
                    if (currentNode == Head)
                    {
                        Head = Head.next;
                        Head.previous = null;
                    }
                    else if (currentNode == tail)
                    {
                        tail = tail.previous;
                        tail.next = null;
                    }
                    else
                    {
                        currentNode.next.previous = currentNode.previous;
                        currentNode.previous.next = currentNode.next;
                    }

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
