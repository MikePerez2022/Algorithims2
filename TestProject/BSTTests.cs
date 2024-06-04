using SearchTreeLibrary;

namespace TestProject
{
    [TestClass]
    public class BSTTests
    {
        #region BST_TREE_PLANTING_TESTS
        //since node is internal I can't check that and can only check the cound,
        //but debugging will show it works if needed

        [TestMethod]
        public void TestAdd_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(1);

            Assert.AreEqual(1, bst.count);
        }

        [TestMethod]
        public void TestAdd_MultipleNodes()
        {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(2);
            bst.Add(3);

            Assert.AreEqual(3, bst.count);
        }

        [TestMethod]
        public void TestPrivate_add_DuplicateNodes()
        {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(2);
            bst.Add(2);
            bst.Add(2);

            Assert.AreEqual(4, bst.count);
        }

        [TestMethod]
        public void TestPrivate_add_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(4);
            bst.Add(2);
            bst.Add(1);
            bst.Add(5);
            bst.Add(6);

            Assert.AreEqual(5, bst.count);
        }

        [TestMethod]
        public void TestPrivate_Traverse_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(3);
            bst.Add(0);
            bst.Add(2);

            Assert.AreEqual(4, bst.count);
        }

        [TestMethod]
        public void TestPrivate_Traverse_DuplicateNodes()
        {
            BST<int> bst = new BST<int>();
            bst.Add(7);
            bst.Add(5);
            bst.Add(5);
            bst.Add(9);

            Assert.AreEqual(4, bst.count);
        }

        [TestMethod]
        public void TestClear_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(1);
            bst.Add(2);
            bst.Clear();

            Assert.AreEqual(0, bst.count);
        }

        [TestMethod]
        public void TestClear_EmptyTree()
        {
            BST<int> bst = new BST<int>();
            bst.Clear();
            Assert.AreEqual(0, bst.count);
        }

        #endregion

        #region BST_TESTS
        //The private methods are tested when using the public methods

        [TestMethod]
        public void TestContains_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(75);

            Assert.IsTrue(bst.Contains(75));
        }

        [TestMethod]
        public void TestContains_EmptyTree()
        {
            BST<int> bst = new BST<int>();

            Assert.IsFalse(bst.Contains(75));
        }

        [TestMethod]
        public void TestRemove_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(80);

            bst.Remove(80);

            Assert.AreEqual(2, bst.count);
        }

        [TestMethod]
        public void TestRemove_EmptyTree()
        {
            BST<int> bst = new BST<int>();

            bst.Remove(80);

            Assert.AreEqual(0, bst.count);
        }

        [TestMethod]
        public void TestHeight_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(15);
            bst.Add(5);
            bst.Add(20);
            bst.Add(40);
            bst.Add(75);
            bst.Add(60);
            bst.Add(100);

            Assert.AreEqual(4, bst.Height());
        }

        [TestMethod]
        public void TestHeight_EmptyTree()
        {
            BST<int> bst = new BST<int>();

            Assert.AreEqual(0, bst.Height());
        }

        [TestMethod]
        public void TestToArray_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(75);

            int[] array = bst.ToArray();

            Assert.IsTrue(array[0] == 25);
            Assert.IsTrue(array[1] == 50);
            Assert.IsTrue(array[2] == 75);
        }

        [TestMethod]
        public void TestToArray_DuplicateValues()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(25);
            bst.Add(75);

            int[] array = bst.ToArray();

            Assert.IsTrue(array[0] == 25);
            Assert.IsTrue(array[1] == 25);
            Assert.IsTrue(array[2] == 50);
            Assert.IsTrue(array[3] == 75);
        }

        [TestMethod]
        public void TestInOrder_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(75);

            Assert.IsTrue(bst.InOrder().Equals("25, 50, 75"));
        }

        [TestMethod]
        public void TestInOrder_Duplicates()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(25);
            bst.Add(75);

            Assert.IsTrue(bst.InOrder().Equals("25, 25, 50, 75"));
        }

        [TestMethod]
        public void TestPreOrder_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(75);

            Assert.IsTrue(bst.PreOrder().Equals("50, 25, 75"));
        }

        [TestMethod]
        public void TestPreOrder_Duplicates()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(25);
            bst.Add(75);

            Assert.IsTrue(bst.PreOrder().Equals("50, 25, 25, 75"));
        }

        [TestMethod]
        public void TestPostOrder_HappyPath()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(75);

            Assert.IsTrue(bst.PostOrder().Equals("25, 75, 50"));
        }

        [TestMethod]
        public void TestPostOrder_Duplicates()
        {
            BST<int> bst = new BST<int>();
            bst.Add(50);
            bst.Add(25);
            bst.Add(25);
            bst.Add(75);

            Assert.IsTrue(bst.PostOrder().Equals("25, 25, 75, 50"));
        }

        #endregion

        #region AVL_TESTS

        [TestMethod]
        public void TestAVL_NoDuplicates()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(11);
            avl.Add(11);
            avl.Add(56);

            Assert.IsTrue(avl.count == 2);
        }

        [TestMethod]
        public void TestAVL_ToArray_HappyPath()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(24);
            avl.Add(10);
            avl.Add(11);
            avl.Add(56);
            avl.Add(13);

            int[] arr = { 11, 10, 24, 13, 56 };

            Assert.IsTrue(arr.SequenceEqual(avl.ToArray()));
        }

        [TestMethod]
        public void TestAVL_ToArray_EmptyTree()
        {
            AVL<int> avl = new AVL<int>();
            Assert.ThrowsException<ArgumentNullException>(() => avl.ToArray());
        }

        [TestMethod]
        public void TestAVL_RotateRight_Add()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(87);
            avl.Add(34);
            avl.Add(2);

            string expected = "2, 34, 87";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAVL_RotateRight_Remove()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(87);
            avl.Add(100);
            avl.Add(55);
            avl.Add(5);

            avl.Remove(100);

            var expected = "5, 55, 87";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAVL_RotateLeft_Add()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(11);
            avl.Add(24);
            avl.Add(56);

            var expected = "11, 24, 56";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAVL_RotateLeft_Remove()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(11);
            avl.Add(5);
            avl.Add(24);
            avl.Add(56);

            avl.Remove(5);

            var expected = "11, 24, 56";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAVL_RotateRightLeft_Add()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(25);
            avl.Add(15);
            avl.Add(20);

            var expected = "15, 20, 25";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAVL_RotateRightLeft_Remove()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(25);
            avl.Add(50);
            avl.Add(15);
            avl.Add(20);

            avl.Remove(50);

            var expected = "15, 20, 25";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAVL_RotateLeftRight_Add()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(15);
            avl.Add(25);
            avl.Add(20);

            var expected = "15, 20, 25";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAVL_RotateLeftRight_Remove()
        {
            AVL<int> avl = new AVL<int>();
            avl.Add(15);
            avl.Add(5);
            avl.Add(25);
            avl.Add(20);

            avl.Remove(5);

            var expected = "15, 20, 25";
            string actual = avl.InOrder();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}