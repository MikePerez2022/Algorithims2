using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary;

namespace TestProject
{
    [TestClass]
    public class Stack_QueueTests
    {
        #region STACK_TESTS

        [TestMethod]
        public void TestGet_Stack_HappyPath()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);
            stack.Push(89);
            stack.Push(234);
            stack.Push(21);

            Assert.IsTrue(stack.Get(2).Equals(89));
        }

        [TestMethod]
        public void TestGet_Stack_OutOfBounds()
        {
            StackList<int> stack = new StackList<int>();

            Assert.ThrowsException<IndexOutOfRangeException>(() => stack.Get(5));
        }

        [TestMethod]
        public void TestContains_Stack_HappyPath()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);
            stack.Push(22);
            stack.Push(89);

            Assert.IsTrue(stack.Contains(42));
        }

        [TestMethod]
        public void TestContains_Stack_EmptyList()
        {
            StackList<int> stack = new StackList<int>();

            Assert.IsFalse(stack.Contains(42));
        }

        [TestMethod]
        public void TestPeek_Stack_HappyPath()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);
            stack.Push(22);
            stack.Push(89);

            Assert.IsTrue(stack.Peek().data.Equals(89));
        }

        [TestMethod]
        public void TestPeek_Stack_EmptyList()
        {
            StackList<int> stack = new StackList<int>();

            Assert.IsNull(stack.Peek());
        }

        [TestMethod]
        public void TestPop_Stack_HappyPath()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);
            stack.Push(22);
            stack.Push(89);
            stack.Push(965);

            Assert.IsTrue(stack.Pop().data.Equals(965));
        }

        [TestMethod]
        public void TestPop_Stack_PopLastItem()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);

            Assert.IsTrue(stack.Pop().data.Equals(42));
        }

        [TestMethod]
        public void TestPop_Stack_EmptyList()
        {
            StackList<int> stack = new StackList<int>();

            Assert.IsNull(stack.Pop());
        }

        [TestMethod]
        public void TestPush_Stack_HappyPath()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);

            Assert.IsTrue(stack.Head.data == 42);
        }

        [TestMethod]
        public void TestPush_Stack_Multiple()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);
            stack.Push(22);
            stack.Push(89);

            Assert.IsTrue(stack.tail.data == 42);
        }

        [TestMethod]
        public void TestPush_Stack_Duplicates()
        {
            StackList<int> stack = new StackList<int>();
            stack.Push(42);
            stack.Push(22);
            stack.Push(22);

            Assert.IsTrue(stack.Head.next.data == 22);
        }

        #endregion

        //-------------------------------------------------------------------------------

        #region QUEUE_TESTS
        [TestMethod]
        public void TestGet_Queue_HappyPath()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(42);
            queue.Enqueue(754);
            queue.Enqueue(16);
            queue.Enqueue(21);

            Assert.IsTrue(queue.Get(1).Equals(754));
        }

        [TestMethod]
        public void TestGet_Queue_OutOfBounds()
        {
            QueueList<int> queue = new QueueList<int>();

            Assert.ThrowsException<IndexOutOfRangeException>(() => queue.Get(5));
        }

        [TestMethod]
        public void TestContains_Queue_HappyPath()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(32);
            queue.Enqueue(534);
            queue.Enqueue(88);
            queue.Enqueue(109);

            Assert.IsTrue(queue.Contains(534));
        }

        [TestMethod]
        public void TestContains_Queue_EmptyList()
        {
            QueueList<int> queue = new QueueList<int>();

            Assert.IsFalse(queue.Contains(42));
        }

        [TestMethod]
        public void TestPeek_Queue_HappyPath()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(42);
            queue.Enqueue(22);
            queue.Enqueue(89);

            Assert.IsTrue(queue.Peek().data.Equals(42));
        }

        [TestMethod]
        public void TestPeek_Queue_EmptyList()
        {
            QueueList<int> queue = new QueueList<int>();

            Assert.IsNull(queue.Peek());
        }

        [TestMethod]
        public void TestDequeue_Queue_HappyPath()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(57);
            queue.Enqueue(22);
            queue.Enqueue(89);
            queue.Enqueue(965);

            Assert.IsTrue(queue.Dequeue().data.Equals(57));
        }

        [TestMethod]
        public void TestDequeue_Queue_DequeueLastItem()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(42);

            Assert.IsTrue(queue.Dequeue().data.Equals(42));
        }

        [TestMethod]
        public void TestDequeue_Queue_EmptyList()
        {
            QueueList<int> queue = new QueueList<int>();

            Assert.IsNull(queue.Dequeue());
        }

        [TestMethod]
        public void TestEnqueue_Queue_HappyPath()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(42);

            Assert.IsTrue(queue.Head.data == 42);
        }

        [TestMethod]
        public void TestEnqueue_Queue_Multiple()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(42);
            queue.Enqueue(22);
            queue.Enqueue(67);

            Assert.IsTrue(queue.tail.data == 67);
        }

        [TestMethod]
        public void TestEnqueue_Queue_Duplicates()
        {
            QueueList<int> queue = new QueueList<int>();
            queue.Enqueue(42);
            queue.Enqueue(22);
            queue.Enqueue(22);

            Assert.IsTrue(queue.tail.data == 22);
        }

        #endregion
    }
}
