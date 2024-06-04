

using System.Text;

namespace DataStructuresLibrary
{
    public class StackList<T> where T : IComparable<T>
    {
        public Node<T>? Head;
        public Node<T>? tail;
        public int count;

        /// <summary>
        /// Takes in an index to find the value<br/><br/>
        /// Searches through the stack to find the value at the index. <br/><br/>
        /// If the index is out of bounds an exception is thrown.<br/><br/>
        /// Big O Notation: O(n)
        /// </summary>
        /// <param name="index">The index to find the node</param>
        /// <returns>The value that is in the node at the provided index</returns>
        /// <exception cref="IndexOutOfRangeException">If the index is out of bounds this is thrown</exception>
        public T Get(int index)//O(n)
        {
            if (index >= count || index < 0) throw new IndexOutOfRangeException();

            Node<T> currentNode = Head;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.next;
            }

            return currentNode.data;
        }

        /// <summary>
        /// Takes in a value to check if it is contained<br/><br/>
        /// Loops though the entire list and if there is a node containing that value it will return true<br/><br/>
        /// otherwise it will return false<br/><br/>
        /// Big O Notation: O(n)
        /// </summary>
        /// <param name="value">The data checked against the data in the node</param>
        /// <returns>A Boolean on if the value is contained</returns>
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
        /// Looks at the item next in line to be removed (Top of stack)
        /// and because of the unique push method the top of the stack is the head<br/><br/>
        /// Big O Notation: O(1)
        /// </summary>
        /// <returns>The top of the stack. The Head node.</returns>
        public Node<T> Peek()//O(1)
        {
            return Head;
        }

        /// <summary>
        /// Due to the push method the top of the stack is the head so all this does is remove the head.<br/>
        /// Then it sets the head to the next head and if it is null or the tail the method returns null or tail will be set to null.<br/><br/>
        /// Big O Notation: O(1)
        /// </summary>
        /// <returns>The item (Node) that is removed</returns>
        public Node<T>? Pop()//O(1)
        {
            if (Head == null) return default;

            if (tail == Head) tail = null;

            var itemRemoved = Head;
            Head = Head.next;
            count--;

            return itemRemoved;
        }

        /// <summary>
        /// Takes in a value for the new node data<br/><br/>
        /// Adds an item to the stack by making the new item the head. 
        /// Instead of adding it to the tail, this makes the head always the top of the stack.<br/><br/>
        /// Big O Notation: O(1)
        /// </summary>
        /// <param name="value">The value that will be contained in the node</param>
        public void Push(T value)//O(1)
        {
            var newNode = new Node<T>(value);
            count++;

            if (Head == null)
            {
                Head = newNode;
                tail = newNode;
                return;
            }

            newNode.next = Head;
            Head = newNode;
        }
    }
}
