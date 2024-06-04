using DataStructuresLibrary;

namespace TestProject
{
    [TestClass]
    public class LinkedListTests
    {
        #region SINGLE_LINKED_LIST

        [TestMethod]
        public void TestAppend_AddNodeToTail()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(42);

            Assert.IsTrue(lists.Head.data.Equals(42));
        }

        [TestMethod]
        public void TestAppend_NonEmptyList()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(42);
            lists.Append(404);

            Assert.IsTrue(lists.Head.data.Equals(42));
            Assert.IsTrue(lists.Head.next.data.Equals(404));
        }

        [TestMethod]
        public void TestRemoveAll_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(42);
            lists.Append(404);
            lists.Append(42);
            lists.Append(404);
            lists.Append(404);
            lists.Append(42);

            lists.RemoveAll(42);

            Assert.IsTrue(lists.Head.data.Equals(404));
        }

        [TestMethod]
        public void TestRemoveAll_NotInList()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(42);
            lists.Append(404);
            lists.Append(42);
            lists.Append(404);
            lists.Append(404);
            lists.Append(42);

            lists.RemoveAll(55);

            Assert.IsTrue(lists.Head.data.Equals(42));
        }

        [TestMethod]
        public void TestRemove_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(404);
            lists.Append(657);

            lists.Remove();

            Assert.IsTrue(lists.Head.data.Equals(657));
        }

        [TestMethod]
        public void TestRemove_EmptyList()
        {
            LinkedLists<int> lists = new LinkedLists<int>();

            Assert.IsTrue(lists.Remove().Equals(0));
        }

        [TestMethod]
        public void TestRemoveAt_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(65);
            lists.Append(404);
            lists.Append(42);
            lists.Append(898);
            lists.Append(65);
            lists.Append(42);

            lists.RemoveAt(2);

            Assert.IsTrue(lists.Head.next.next.data.Equals(898));
        }

        [TestMethod]
        public void TestRemoveAt_OutOfRange()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(404);
            lists.Append(657);

            Assert.ThrowsException<IndexOutOfRangeException>(() => lists.RemoveAt(2));
        }

        [TestMethod]
        public void TestRemoveLast_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            Assert.IsTrue(lists.RemoveLast().Equals(42));
            Assert.IsTrue(lists.tail.data.Equals(954));
        }

        [TestMethod]
        public void TestRemoveLast_EmptyList()
        {
            LinkedLists<int> lists = new LinkedLists<int>();

            Assert.IsTrue(lists.RemoveLast().Equals(0));
        }

        [TestMethod]
        public void TestInsert_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            lists.Insert(678, 1);

            Assert.IsTrue(lists.Head.next.data.Equals(678));
        }

        [TestMethod]
        public void TestInsert_OutOfRange()
        {
            LinkedLists<int> lists = new LinkedLists<int>();

            Assert.ThrowsException<IndexOutOfRangeException>(() => lists.Insert(23,3));
        }

        [TestMethod]
        public void TestClear_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            lists.Clear();

            Assert.IsNull(lists.Head);
        }

        [TestMethod]
        public void TestClear_EmptyList()
        {
            LinkedLists<int> lists = new LinkedLists<int>();

            lists.Clear();

            Assert.IsNull(lists.Head);
        }

        [TestMethod]
        public void TestGet_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            Assert.IsTrue(lists.Get(1).Equals(954));
        }

        [TestMethod]
        public void TestGet_OutOfRange()
        {
            LinkedLists<int> lists = new LinkedLists<int>();

            Assert.ThrowsException<IndexOutOfRangeException>( () => lists.Get(2));
        }

        [TestMethod]
        public void TestSearch_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(65);
            lists.Append(88);
            lists.Append(42);
            lists.Append(245);
            lists.Append(34);

            Assert.IsTrue(lists.Search(245).Equals(3));
        }

        [TestMethod]
        public void TestSearch_EmptyList()
        {
            LinkedLists<int> lists = new LinkedLists<int>();

            Assert.IsTrue(lists.Search(8).Equals(-1));
        }

        [TestMethod]
        public void TestToString_HappyPath()
        {
            LinkedLists<int> lists = new LinkedLists<int>();
            lists.Append(65);
            lists.Append(88);
            lists.Append(42);
            lists.Append(245);
            lists.Append(34);

            Assert.IsTrue(lists.ToString().Equals("65, 88, 42, 245, 34"));
        }

        [TestMethod]
        public void TestToString_EmptyList()
        {
            LinkedLists<int> lists = new LinkedLists<int>();

            Assert.IsTrue(lists.ToString().Equals(""));
        }

        #endregion

        //-------------------------------------------------------------------------------

        #region Double_LINKED_LIST

        [TestMethod]
        public void DL_TestAppend_AddNodeToTail()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(42);

            Assert.IsTrue(lists.Head.data.Equals(42));
        }

        [TestMethod]
        public void DL_TestAppend_NonEmptyList()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(42);
            lists.Append(404);

            Assert.IsTrue(lists.Head.data.Equals(42));
            Assert.IsTrue(lists.tail.data.Equals(404));
        }

        [TestMethod]
        public void DL_TestRemoveAll_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(42);
            lists.Append(404);
            lists.Append(42);
            lists.Append(404);
            lists.Append(404);
            lists.Append(42);

            lists.RemoveAll(42);

            Assert.IsTrue(lists.Head.data.Equals(404));
            Assert.IsTrue(lists.tail.data.Equals(404));
        }

        [TestMethod]
        public void DL_TestRemoveAll_NotInList()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(42);
            lists.Append(404);
            lists.Append(42);
            lists.Append(404);
            lists.Append(404);
            lists.Append(42);

            lists.RemoveAll(55);

            Assert.IsTrue(lists.Head.data.Equals(42));
        }

        [TestMethod]
        public void DL_TestRemove_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(404);
            lists.Append(657);

            lists.Remove();

            Assert.IsTrue(lists.Head.data.Equals(657));
        }

        [TestMethod]
        public void DL_TestRemove_EmptyList()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();

            Assert.IsTrue(lists.Remove().Equals(0));
        }

        [TestMethod]
        public void DL_TestRemoveAt_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(65);
            lists.Append(404);
            lists.Append(42);
            lists.Append(898);
            lists.Append(65);
            lists.Append(42);

            
            Assert.IsTrue(lists.RemoveAt(4).Equals(65));
            Assert.IsTrue(lists.tail.previous.data.Equals(898));
        }

        [TestMethod]
        public void DL_TestRemoveAt_OutOfRange()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(404);
            lists.Append(657);

            Assert.ThrowsException<IndexOutOfRangeException>(() => lists.RemoveAt(2));
        }

        [TestMethod]
        public void DL_TestRemoveLast_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            Assert.IsTrue(lists.RemoveLast().Equals(42));
            Assert.IsTrue(lists.tail.data.Equals(954));
        }

        [TestMethod]
        public void DL_TestRemoveLast_EmptyList()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();

            Assert.IsTrue(lists.RemoveLast().Equals(0));
        }

        [TestMethod]
        public void DL_TestInsert_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            lists.Insert(678, 2);

            Assert.IsTrue(lists.tail.previous.data.Equals(678));
        }

        [TestMethod]
        public void DL_TestInsert_OutOfRange()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();

            Assert.ThrowsException<IndexOutOfRangeException>(() => lists.Insert(23, 3));
        }

        [TestMethod]
        public void DL_TestClear_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            lists.Clear();

            Assert.IsNull(lists.Head);
        }

        [TestMethod]
        public void DL_TestClear_EmptyList()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();

            lists.Clear();

            Assert.IsNull(lists.Head);
        }

        [TestMethod]
        public void DL_TestGet_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(65);
            lists.Append(954);
            lists.Append(42);

            Assert.IsTrue(lists.Get(2).Equals(42));
        }

        [TestMethod]
        public void DL_TestGet_OutOfRange()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();

            Assert.ThrowsException<IndexOutOfRangeException>(() => lists.Get(2));
        }

        [TestMethod]
        public void DL_TestSearch_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(65);
            lists.Append(88);
            lists.Append(42);
            lists.Append(245);
            lists.Append(34);

            Assert.IsTrue(lists.Search(42).Equals(2));
        }

        [TestMethod]
        public void DL_TestSearch_EmptyList()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();

            Assert.IsTrue(lists.Search(8).Equals(-1));
        }

        [TestMethod]
        public void DL_TestToString_HappyPath()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();
            lists.Append(23);
            lists.Append(88);
            lists.Append(74);
            lists.Append(245);
            lists.Append(1);

            Assert.IsTrue(lists.ToString().Equals("23, 88, 74, 245, 1"));
        }

        [TestMethod]
        public void DL_TestToString_EmptyList()
        {
            DoubleLinkedLists<int> lists = new DoubleLinkedLists<int>();

            Assert.IsTrue(lists.ToString().Equals(""));
        }

        #endregion
    }
}