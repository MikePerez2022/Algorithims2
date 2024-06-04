using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchTreeLibrary;

namespace SearchTreeLibrary
{
    public class BST<T> where T : IComparable<T>
    {
        internal Node<T>? Root { get; set; }

        public int count { get; set; }

        //I decided since there is only one difference in the traverse orders I would use an enume
        private enum eOrder
        {
            InOrder,
            PreOrder,
            PostOrder
        }

        /// <summary>
        /// Takes in generic type value and returns nothing<br/><br/>
        /// Adds a node to the tree in the correct location using the traverse and add private methods set a<br/>
        /// new node called add location to the result of travers and call add with the addlocation and value.<br/><br/>
        /// Big O Notation: O(Log(n))
        /// </summary>
        /// <param name="value">Generic type value to be set to the new nodes value</param>
        public void Add(T value)//Big O Notation: O(Log(n)) 
        {
            Node<T>? addLocaton = Traverse(Root, value, false);
            add(addLocaton, value);
        }

        /// <summary>
        /// Takes in generic node type node and generic type value and returns nothing<br/><br/>
        /// Using traverse recursion to find the best location to add the new node and increment the count by 1<br/><br/>
        /// Big O Notation: O(Log(n))
        /// </summary>
        /// <param name="nodeLocation">The node loaction where the new node can be set from</param>
        /// <param name="value">Generic type value to be set to the new nodes value</param>
        private void add(Node<T>? nodeLocation, T value)//Big O Notation: O(Log(n))
        {
            var newNode = new Node<T>(value);
            newNode.Height = 0;
            if (nodeLocation == null) Root = newNode;
            else
            {
                var compareToValue = nodeLocation.Value.CompareTo(value);
                if (compareToValue == 0)
                {
                    if (this is AVL<T>) return;

                    if (nodeLocation.Left == null) nodeLocation.Left = newNode;
                    else
                    {
                        nodeLocation = Traverse(nodeLocation.Left, value, false);
                        add(nodeLocation, value);
                        return;
                    }
                }
                if (compareToValue < 0) nodeLocation.Right = newNode;
                else nodeLocation.Left = newNode;
            }
            count++;
        }


        

        /// <summary>
        /// Takes in generic node type node, generic type value, and bool that activates a function <br/><br/>
        /// Traverse the tree to find the node or parent node that contains the value and returns it unless<br/>
        /// the best location is null and returns the current node so it can be used if needed<br/><br/>
        /// Big O Notation: O(Log(n))
        /// </summary>
        /// <param name="currentNode">The current node being traversed through</param>
        /// <param name="value">Generic type value to check if there is a node with that value</param>
        /// <param name="getParent">Bool to ascertain if it is retrieving the parent node </param>
        /// <returns></returns>
        private Node<T>? Traverse(Node<T>? currentNode, T value, bool getParent)//Big O Notation: O(Log(n)) 
        {
            if(currentNode == null) return currentNode;
            var compareValue = currentNode.Value.CompareTo(value);

            if (compareValue == 0) return currentNode;

            if (getParent)
            {
                if (currentNode.Left != null && currentNode.Left.Value.CompareTo(value) == 0 
                    || currentNode.Right != null && currentNode.Right.Value.CompareTo(value) == 0) return currentNode;
            }
                

            if (compareValue < 0)
            {
                return currentNode.Right == null ? currentNode : Traverse(currentNode.Right, value, getParent);
            }
            else
            {
                return currentNode.Left == null ? currentNode : Traverse(currentNode.Left, value, getParent);
            }
        }

        /// <summary>
        /// Takes no imput and returns nothing<br/><br/>
        /// Set root to null then count to 0 and let the garbage collector take care of it.<br/><br/>
        /// Big O Notation: O(1) 
        /// </summary>
        public void Clear()
        {
            Root = null;
            count = 0;
        }

        /// <summary>
        /// Takes in a value to find the node connected to it.<br/><br/>
        /// Calls the traverse method and gets a node and if the returned node doesn't hold the value return false.<br/><br/>
        /// Big O Notation: <br/> O(Log(n))
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)//log(n)
        {
            var checkNode = Traverse(Root, value, false);
            return (checkNode == null || checkNode.Value.CompareTo(value) != 0) ? false: true;
        }

        /// <summary>
        /// Takes in the value to be removed and returns nothing<br/><br/>
        /// Traverses and finds the parent node of the node being removed.<br/>
        /// Then checks if the node is a valid node to remove if not end the methof.<br/>
        /// Finally call the private remove fuction that will remove the node from the tree.<br/><br/>
        /// Big O Notation: <br/> Average case: O(log(n)) <br/> Worst case: O(n)
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)// Average case: log(n) Worst case: O(n)
        {
            var parentNode = Traverse(Root, value, true);
            if(parentNode == null || parentNode.Left == null && parentNode.Right == null && parentNode.Value.CompareTo(value) != 0) return;
            remove(parentNode, value);
        }

        /// <summary>
        /// Takes in a the remove node's parent node and the remove value and returns nothing<br/><br/>
        /// First it checks if the node being removed is the root node and if so it goes through the same proccess.
        /// Then it checks which side of the parent node is being altered and useing that determines the remove node.
        /// It checks if the remove node has a left node if so it will go to the furthest right on that left node.
        /// Until it reaches null and appends the remove node's right on to that right pointer then sets the parents pointer to
        /// the remove node's left. The check on the parent node is used again to see which parent node side needs altering.<br/>
        /// Although if there is no right then the parent node altered side is set to the right of the remove node.
        /// <br/><br/>
        /// Big O Notation: <br/> Average case: O(log(n)) <br/> Worst case: O(n)
        /// </summary>
        /// <param name="parentNode">The node before the remove node</param>
        /// <param name="value">The value of the remove node</param>
        private void remove(Node<T>? parentNode, T value)
        {
            if(parentNode == null) return;
            count--;

            //improved code below based on review with shull

            if (parentNode == Root && parentNode.Value.CompareTo(value) == 0)
            {
                if (parentNode.Left != null)
                {
                    parentNode = parentNode.Left;
                    while (parentNode.Right != null)
                    {
                        parentNode = parentNode.Right;
                    }

                    parentNode.Right = Root.Right;
                    Root = Root.Left;
                }
                else Root = Root.Right;

                return;
            }

            bool removeLeft = (parentNode.Left != null && parentNode.Left.Value.CompareTo(value) == 0) ? true : false;
            var removeNode = (removeLeft) ? parentNode.Left : parentNode.Right;

            if (removeNode.Left != null)
            {
                var current = removeNode.Left;
                while (current.Right != null)
                {
                    current = current.Right;
                }

                current.Right = removeNode.Right;

                if (removeLeft) parentNode.Left = removeNode.Left;
                else parentNode.Right = removeNode.Left;
            }
            else
            {
                if(removeLeft) parentNode.Left = removeNode.Right;
                else parentNode.Right = removeNode.Right;
            }

            //Old code
            #region OLD
            //if (removeNode.Left != null)
            //{
            //    removeNode.Left.Right = removeNode.Right;
            //    if (removeNode == parentNode.Left) parentNode.Left = removeNode.Left;
            //    else parentNode.Right = removeNode.Right;
            //}
            //else if(removeNode.Right != null)
            //{
            //    if (removeNode == parentNode.Left) parentNode.Left = removeNode.Right;
            //    else parentNode.Right = removeNode.Right;
            //}
            //else
            //{
            //    if (removeNode == parentNode.Left) parentNode.Left = null;
            //    else parentNode.Right = null;
            //}

            #endregion
        }

        /// <summary>
        /// Takes in nothing<br/><br/>
        /// Checks if the root is null or no nodes in the tree and returns 0 if so.<br/>
        /// Creates a height, leftHeight, and RightHeight to store the current height and the max left and right heights.<br/>
        /// Recursively call traverse height on the left and right on the root node and returns the heighest heights.<br/><br/>
        /// Big O Notation: <br/> O(Log(n))
        /// </summary>
        /// <returns>The height of the tree</returns>
        public int Height()
        {
            if(Root == null || count == 0) return 0;
            int height = 1;
            int LeftHeight = 0;
            int RightHeight = 0;

            TraverseHeight(Root.Left, ref height, ref LeftHeight);
            TraverseHeight(Root.Right, ref height, ref RightHeight);

            return (LeftHeight > RightHeight) ? LeftHeight : RightHeight;
        }

        /// <summary>
        /// Takes in nothing.<br/><br/>
        /// Declares an array of the size count and creates an index variable to add the values to the array.<br/>
        /// Traverse order is called giving in the parameters and using the InOrder enum to get the correct order.<br/>
        /// Using the returned array pass it into the string converter method to return it as a formatted string list.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <returns>The string formatted as a list of all the tree node values in InOrder</returns>
        public string InOrder()//O(n)
        {
            T[] result = new T[count];
            int index = 0;
            result = TraverseOrderToArray(Root, result, eOrder.InOrder, ref index);
            return ConvertToString(result);
        }

        /// <summary>
        /// Takes in nothing.<br/><br/>
        /// Declares an array of the size count and creates an index variable to add the values to the array.<br/>
        /// Traverse order is called giving in the parameters and using the InOrder enum to get the correct order.<br/><br/>
        /// Big O Notation: <br/> O(Log(n))
        /// </summary>
        /// <returns>The the array of all the tree node values in InOrder</returns>
        public T[] ToArray()
        {
            T[] result = new T[count];
            int index = 0;
            result = TraverseOrderToArray(Root, result, eOrder.InOrder, ref index);
            return result;
        }

        /// <summary>
        /// Takes in nothing.<br/><br/>
        /// Declares an array of the size count and creates an index variable to add the values to the array.<br/>
        /// Traverse order is called giving in the parameters and using the PreOrder enum to get the correct order.<br/>
        /// Using the returned array pass it into the string converter method to return it as a formatted string list.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <returns>The string formatted as a list of all the tree node values in PreOrder</returns>
        public string PreOrder()//O(n)
        {
            T[] result = new T[count];
            int index = 0;
            result = TraverseOrderToArray(Root, result, eOrder.PreOrder, ref index);
            return ConvertToString(result);
        }

        /// <summary>
        /// Takes in nothing.<br/><br/>
        /// Declares an array of the size count and creates an index variable to add the values to the array.<br/>
        /// Traverse order is called giving in the parameters and using the PostOrder enum to get the correct order.<br/>
        /// Using the returned array pass it into the string converter method to return it as a formatted string list.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <returns>The string formatted as a list of all the tree node values in PostOrder</returns>
        public string PostOrder()//O(n)
        {
            T[] result = new T[count];
            int index = 0;
            result = TraverseOrderToArray(Root, result, eOrder.PostOrder, ref index);
            return ConvertToString(result);
        }

        /// <summary>
        /// Takes in the current node, the current height, and the height tracker. Returns nothing.<br/><br/>
        /// Checks if the node is null if so it backtracks and each time it enters validly the height is increased.<br/>
        /// It then checks the current height and the tracker and sets the tracker if the current height id higher.<br/>
        /// Recursively calls TravseHeight on the left and right of the current node and enters all parameters.<br/>
        /// if the recursive checks are finished for a subtree the height is decreased and the method is backtracked.<br/><br/>
        /// Big O Notation: <br/> O(Log(n))
        /// </summary>
        /// <param name="currentNode">The node being traversed</param>
        /// <param name="currentHeight">The current height of the node in the tree</param>
        /// <param name="tracker">The tracker for the tree height</param>
        private void TraverseHeight(Node<T>? currentNode, ref int currentHeight, ref int tracker)
        {
            if (currentNode == null) return;
            currentHeight++;

            if (tracker < currentHeight) tracker = currentHeight;
            
            TraverseHeight(currentNode.Left, ref currentHeight, ref tracker);
            TraverseHeight(currentNode.Right, ref currentHeight, ref tracker);

            currentHeight--;
            return;
        }

        /// <summary>
        /// Takes in the current node, the array being output, the type of order, and the array index.<br/>
        /// Checks if the current node is null and if so returns the result. Then recursively calls the <br/>
        /// method on the left and right of the current node. As it goes through there are appropritaely<br/>
        /// placed checks that check what type of travserse order is being used and adds the value accordingly.<br/><br/>
        /// Big O Notation: <br/> O(Log(n))
        /// </summary>
        /// <param name="currentNode">The node being traversed</param>
        /// <param name="result">The array being added to</param>
        /// <param name="order">An enum selecting what traverse order will be in place</param>
        /// <param name="index">The place to add the value into the array</param>
        /// <returns></returns>
        private T[] TraverseOrderToArray(Node<T>? currentNode, T[] result, eOrder order, ref int index)//Log(n)
        {
            if (currentNode == null) return result;

            if (order == eOrder.PreOrder) result[index++] = currentNode.Value;

            TraverseOrderToArray(currentNode.Left, result, order, ref index);

            if (order == eOrder.InOrder) result[index++] = currentNode.Value;

            TraverseOrderToArray(currentNode.Right, result, order, ref index);

            if (order == eOrder.PostOrder) result[index++] = currentNode.Value;

            return result;
        }

        /// <summary>
        /// Takes in an array to make a string and returns a string<br/><br/>
        /// Checks of the array is null to stop it if so and creates a temperary string.<br/>
        /// Loops through the entire array and adds the values to a string in the form of a list.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="array">The array turned into the string</param>
        /// <returns>The string list from the array values</returns>
        private string? ConvertToString(T[] array)//O(n)
        {
            if(array == null) return "";

            string result = "";
            for(int i = 0; i < count; i++)
            {
                result += (i != count-1) ? array[i] + ", " : array[i]; 
            }

            return result;
        }
    }

    //AVL subclass
    public class AVL<T> : BST<T> where T : IComparable<T>
    {
        /// <summary>
        /// This method overloads its base class method, but still calls it and passes in the value to use its behavior.<br/>
        /// Then the calculate height function is called to calaculate the height before the auto balance function checks<br/>
        /// the heights for balancing. Then calculate height function is called again in case it was auto balanced.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="value">The value to create a new node</param>
        public new void Add(T value)
        {
            base.Add(value); 
            CalculateHeight();
            AutoBalance();
            CalculateHeight();
        }

        /// <summary>
        /// This method overloads its base class method, but still calls it and passes in the value to use its behavior.<br/>
        /// Then the calculate height function is called to calaculate the height before the auto balance function checks<br/>
        /// the heights for balancing. Then calculate height function is called again in case it was auto balanced.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="value">The value used to find the removed node</param>
        public new void Remove(T value)
        {
            base.Remove(value);
            CalculateHeight();
            AutoBalance();
            CalculateHeight();
        }

        /// <summary>
        /// This method calls the private caclulate height method used for travsersal and height setting.<br/>
        /// In the method it passes in the root so it traverses the entire array.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <exception cref="NullReferenceException">Throws exception if root is null</exception>
        private void CalculateHeight()
        {
            if (Root == null || count == 0) throw new NullReferenceException();
            //int height = 0;
            //int? LeftHeight = 0;
            //int? RightHeight = 0;

            //LeftHeight = calculateHeight(Root.Left);
            //RightHeight = calculateHeight(Root.Right);
            calculateHeight(Root);

            //Root.Height = (LeftHeight > RightHeight) ? LeftHeight : RightHeight;
        }

        /// <summary>
        /// This is a private method for traversing the entire tree and after finding the leaf node it sets its height to zero<br/>
        /// then as it back tracks the current nodes height is increased by 1 and returned to be set as the previous node's left ir right height.<br/>
        /// After both left and right height of a node are found they are compared and the higher value is set as the new height.<br/>
        /// This is repeated untill it reaches the first node passed in.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="currentNode">The node being traversed</param>
        /// <returns>The current node's height increased by one</returns>
        private int? calculateHeight(Node<T>? currentNode)
        {
            //My orgiginal code was like my height calculation for the BST and didn't work.
            //So, with help from chat GPT and Gemini I made this function and cut out what was unessesary.
            if (currentNode == null) return 0;

            var LeftHeight = calculateHeight(currentNode.Left);
            var RightHeight = calculateHeight(currentNode.Right);

            if(LeftHeight == null) LeftHeight = 0;
            if(RightHeight == null) RightHeight = 0;

            currentNode.Height = (LeftHeight > RightHeight) ? LeftHeight : RightHeight;
            //currentHeight++;
            return currentNode.Height + 1;
        }

        /// <summary>
        /// This method uses the passed in node to calculate the balance factor. First it has an edge case in it is null.<br/>
        /// Then it checks both the left and right of the passed in node if they are null using a tunurary. If it is null <br/>
        /// the value on that side is set to zero otherwise it is set the that nodes height plus one to account for the current node.<br/>
        /// Then the left value is subtracted from the right and returned as the new balance factor.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="currentNode">The current node used to calculate the balance factor</param>
        /// <returns>The calculated balance factor</returns>
        private int CalculateBalance(Node<T> currentNode)//O(1)
        {
            if(currentNode == null) return 0;
            var Left = (currentNode.Left == null) ? 0 : currentNode.Left.Height + 1;
            var Right = (currentNode.Right == null) ? 0 : currentNode.Right.Height + 1;

            return (int)(Left - Right);
        }

        /// <summary>
        /// This method is the auto rotation method used to automatically balance the tree when adding or removing nodes.<br/>
        /// First it checks if it is unbalanced by checking the absolute value of the balance factor. If so the pivot is <br/>
        /// determined and set based on that factor and the second balance factor is made using the pivot to see if 2 rotations are needed.<br/>
        /// The it goes through 4 if and else checks to see what type of rotation should be used.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        private void AutoBalance()//O(1)
        {
            var balanceFactor = (int)CalculateBalance(Root);
            if (Math.Abs(balanceFactor) <= 1) return;

            var pivot = (balanceFactor < 0) ? Root.Right : Root.Left;
            var balanceFactor2 = CalculateBalance(pivot);

            if (balanceFactor < 0 && balanceFactor2 > 0) Root = rotateLeftRight(Root);
            else if (balanceFactor > 0 && balanceFactor2 < 0) Root = rotateRightLeft(Root);
            else if (balanceFactor < 0) Root = rotateLeft(Root);
            else Root = rotateRight(Root);
        }

        /// <summary>
        /// This rotate right function checkc if it is a valid move and if so creates a temp holder for the pivot.<br/>
        /// To rotate it right the pivot has to be left of the parent node. Then the parent node's left is set to the <br/>
        /// right of the pivot as it is less than the parent node and the right of the pivot will be reassigned. <br/>
        /// The pivot right is set to the parent and is returned to be set as the root.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="ParentNode">Takes in the parent node of the pivot node</param>
        /// <returns>The pivot node after the changes were made and is to be set to the subtree root</returns>
        /// <exception cref="NullReferenceException">If there is no pivot node it throws this</exception>
        private Node<T> rotateRight(Node<T> ParentNode)
        {
            if (ParentNode.Left == null) throw new NullReferenceException();

            var pivot = ParentNode.Left;
            ParentNode.Left = pivot.Right;
            pivot.Right = ParentNode;

            //set pivot to parent
            return pivot;
        }

        /// <summary>
        /// This rotate left function checks if it is a valid move and if so creates a temp holder for the pivot.<br/>
        /// To rotate it left the pivot has to be right of the parent node. Then the parent node's right is sent to the<br/>
        /// left of the pivot as it is bigger than the parent node and the left of the pivot will be reassigned. <br/>
        /// The pivot left is set to the parent and is returned to be set as the root.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="ParentNode">Takes in the parent node of the pivot node</param>
        /// <returns>The pivot node after the changes were made and is to be set to the subtree root</returns>
        /// <exception cref="NullReferenceException">If there is no pivot node it throws this</exception>
        private Node<T> rotateLeft(Node<T> ParentNode)
        {
            if (ParentNode.Right == null) throw new NullReferenceException();

            var pivot = ParentNode.Right;
            ParentNode.Right = pivot.Left;
            pivot.Left = ParentNode;

            return pivot;
        }

        /// <summary>
        /// This rotate right left function checks if it is a valid move and if so calls the left rotation on the pivot.<br/>
        /// Then as it returns and is set as the new pivot node which is ParentNode.left you call the right rotation using the<br/>
        /// parent node and that method's return is set as the parent node (subtree root).<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="ParentNode">Takes in the parent node of the pivot node</param>
        /// <returns>The parent node after the changes were made and is to be set to the subtree root</returns>
        /// <exception cref="NullReferenceException">If there is no pivot node it throws this</exception>
        private Node<T> rotateRightLeft(Node<T> ParentNode)
        {
            if (ParentNode.Left == null) throw new NullReferenceException();

            ParentNode.Left = rotateLeft(ParentNode.Left);
            ParentNode = rotateRight(ParentNode);

            return ParentNode;
        }

        /// <summary>
        /// This rotate left right function checks if it is a valid move and if so calls the right rotation on the pivot.<br/>
        /// Then as it returns and is set as the new pivot node which is ParentNode.right you call the left rotation using the<br/>
        /// parent node and that method's return is set as the parent node (subtree root).<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="ParentNode">Takes in the parent node of the pivot node</param>
        /// <returns>The parent node after the changes were made and is to be set to the subtree root</returns>
        /// <exception cref="NullReferenceException">If there is no pivot node it throws this</exception>
        private Node<T> rotateLeftRight(Node<T> ParentNode)
        {
            if (ParentNode.Right == null) throw new NullReferenceException();

            ParentNode.Right = rotateRight(ParentNode.Right);
            ParentNode = rotateLeft(ParentNode);

            return ParentNode;
        }

        /// <summary>
        /// Takes in nothing<br/>
        /// This method uses a breadth travsersal to travel to every node once row by row.<br/>
        /// Using a queue starting with the root node and creating a result array that holds the dequeued values.<br/>
        /// It iterates through all nodes and adds dequeued item values the the array and enqueues the item's left and right.<br/>
        /// It continues until no node is left.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <returns>The array result from breadth traverse</returns>
        /// <exception cref="ArgumentNullException">Throws a null argument exception if the root is null</exception>
        public new T[] ToArray()
        {
            if (Root == null) throw new ArgumentNullException();

            Queue<Node<T>> queue = new Queue<Node<T>>();
            T[] result = new T[count];
            queue.Enqueue(Root);

            for (int i = 0; i < count; i++)
            {
                var dequedItem = queue.Dequeue();
                result[i] = dequedItem.Value;
                if (dequedItem.Left != null) queue.Enqueue(dequedItem.Left);
                if (dequedItem.Right != null) queue.Enqueue(dequedItem.Right);
            }

            return result;
        }
    }
}
