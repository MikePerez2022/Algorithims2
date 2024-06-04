

using System.Text;

namespace DataStructuresLibrary
{
    public class QueueList<T> where T : IComparable<T>
    {
        public DLNode<T>? Head;
        public DLNode<T>? tail;
        public int count;

        /// <summary>
        /// Takes in an index to find the value<br/><br/>
        /// Checks if the index is less than the fist half of the queue.
        /// If it is start at the head and work down otherwise start at the tail and go back.<br/>
        /// Through Either way it searches through the queue to find the node value at the provided index.<br/><br/>
        /// If the index is out of bounds an exception is thrown.<br/><br/>
        /// Big O Notation: O(n)
        /// 
        /// </summary>
        /// <param name="index">The location of the node holding a value</param>
        /// <returns>The value in the node found from the index</returns>
        /// <exception cref="IndexOutOfRangeException">If the index is out of bounds this is thrown</exception>
        public T Get(int index)//O(n)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();

            DLNode<T> currentNode;
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
                    currentNode = currentNode.prev;
                }
            }

            return currentNode.data;
        }

        /// <summary>
        /// Takes in a value to check if it is contained<br/><br/>
        /// Loops though the queue and if there is a node containing the value it will return true<br/>
        /// otherwise it will return false<br/><br/>
        /// Big O Notation: O(n)
        /// </summary>
        /// <param name="value">The value being searched for</param>
        /// <returns>A boolean (True/False) based on whether the valuse is contained</returns>
        public bool Contains(T value)//O(n)
        {
            var currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.data.CompareTo(value) == 0) return true;
                currentNode = currentNode.next;
            }
            return false;
        }

        /// <summary>
        /// Looks at the next item (node) to be removed and returns it.<br/><br/>
        /// Big O Notation: O(1)
        /// </summary>
        /// <returns>The head node (first item in line)</returns>
        public DLNode<T> Peek()//O(1)
        {
            return Head;
        }

        /// <summary>
        /// This removes and returns the head of the queue because it operates first in first out.
        /// It then sets the head to the next node and decreases the count.<br/><br/>
        /// Big O Notation: O(1)
        /// </summary>
        /// <returns>The node being removed</returns>
        public DLNode<T>? Dequeue()//O(1)
        {
            if (Head == null) return default;

            if(tail == Head) tail = null;

            var itemRemoved = Head;
            Head = Head.next;
            if(Head != null) Head.prev = null;
            count--;

            return itemRemoved;
        }

        /// <summary>
        /// Takes in a value for the new node data<br/><br/>
        /// This adds new items (nodes) to the tail ensuring the head is always the first item added and is at the front.<br/><br/>
        /// Big O Notation: O(1)
        /// </summary>
        /// <param name="value">The value for the new node</param>
        public void Enqueue(T value)//O(1)
        {
            var newNode = new DLNode<T>(value);
            count++;

            if (Head == null)
            {
                Head = newNode;
                tail = newNode;
                return;
            }

            newNode.prev = tail;
            tail.next = newNode;
            tail = newNode;
        }
    }
}
