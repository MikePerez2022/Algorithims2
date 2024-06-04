using GraphLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class MSTTests
    {
        [TestMethod]
        public void TestMST_CreateGraph_HappyPath()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST1.txt");
            Assert.IsNotNull(mst.graph);
        }

        [TestMethod]
        public void TestMST_CreateGraph_InvalidInput()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MST<string>(""));
        }

        [TestMethod]
        public void TestMST_CreateGraph_RemovesDuplicates()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST77.txt");

            Assert.IsTrue(mst.graph.ToString() == "AX1, AX2, AX3, AX4, AX5");
        }

        [TestMethod]
        public void TestMST_GraphAddNode_HappyPath()
        {
            var mst = new MST<string>();
            var test = new MST<string>();
            mst.graph.AddNode("Joker");

            Assert.IsFalse(mst.graph.ToString() == test.graph.ToString());
        }

        [TestMethod]
        public void TestMST_GraphAddNode_IntType()
        {
            var mst = new MST<int>();
            var test = new MST<int>();
            mst.graph.AddNode(55);

            Assert.IsFalse(mst.graph.ToString() == test.graph.ToString());
        }

        //related to graph creation
        [TestMethod]
        public void TestMST_ExtractFileData_HappyPath()
        {
            var mst = new MST<string>();
            Assert.IsNotNull(mst.ExtractDataFromFile("..\\..\\..\\..\\GraphLibrary\\MST1.txt"));
        }

        //related to graph creation
        [TestMethod]
        public void TestMST_ExtractFileData_InvalidFilePath()
        {
            var mst = new MST<string>();
            Assert.ThrowsException<NullReferenceException>(() => mst.ExtractDataFromFile(""));
        }

        //related to graph creation
        [TestMethod]
        public void TestMST_ExtractFileData_NullPath()
        {
            var mst = new MST<string>();
            Assert.ThrowsException<NullReferenceException>(() => mst.ExtractDataFromFile(null));
        }

        [TestMethod]
        public void TestMST_GetMST_HappyPath()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST1.txt");
            var actual = mst.GetMST();

            var expected1 = "Socket Set: AX1, AX4, AX2, AX3, AX5";
            var expected2 = "Cable Needed: 24ft";
            Assert.IsTrue(actual.Item1 == expected1);
            Assert.IsTrue(actual.Item2 == expected2);
        }

        [TestMethod]
        public void TestMST_GetMST_EmptyGraph()
        {
            var mst = new MST<string>();

            Assert.ThrowsException<NullReferenceException>(() => mst.GetMST());
        }

        [TestMethod]
        public void TestMST_GetMST_EmptyFile()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST67.txt");

            Assert.IsNotNull(mst.graph);
            Assert.IsTrue(mst.edgesDictionary.Count == 0);
        }

        [TestMethod]
        public void TestMST_GetMST_NoEdges()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST66.txt");
            var result = mst.GetMST();
            Assert.IsTrue(result.Item1 == "Socket Set: AX1");
            Assert.IsTrue(result.Item2 == "Cable Needed: 0ft");
        }

        //Related to getting MST
        [TestMethod]
        public void TestMST_CreateEdgesDictionary_HappyPath()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST1.txt");
            Assert.IsNotNull(mst.edgesDictionary);
        }

        //Related to getting MST
        [TestMethod]
        public void TestMST_CreateEdgesDictionary_NoEdgesProvided()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST66.txt");
            Assert.IsTrue(mst.edgesDictionary.Count == 0);
        }

        //Related to getting MST - algorithim finding it
        [TestMethod]
        public void TestMST_PrimAlgorithim_HappyPath()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST2.txt");
            var result = mst.GetMST();
            Assert.IsNotNull(result.Item1);
            Assert.IsNotNull(result.Item2);
        }

        [TestMethod]
        public void TestMST_PrimAlgorithim_EmptyGraph()
        {
            var mst = new MST<string>();

            Assert.ThrowsException<NullReferenceException>(() => mst.GetMST());
        }

        [TestMethod]
        public void TestMST_PrimAlgorithim_NullGraph()
        {
            var mst = new MST<string>();
            mst.graph = null;

            Assert.IsNull(mst.GetMST().Item1);
            Assert.IsNull(mst.GetMST().Item2);
        }

        [TestMethod]
        public void TestMST_GetMST_SingleNode()
        {
            var mst = new MST<string>("..\\..\\..\\..\\GraphLibrary\\MST68.txt");

            var result = mst.GetMST();

            Assert.IsTrue(result.Item1 == "Socket Set: AX1");
            Assert.IsTrue(result.Item2 == "Cable Needed: 0ft");
        }

        //Related to getting MST
        [TestMethod]
        public void TestMST_CreateDictionary_HappyPath()
        {
            var mst = new MST<string>();

            string edgeData = "AX87,AX4:3,AX2:3,AX3:6,AX55:32";

            var result = mst.CreateDictionary(edgeData);

            Assert.IsTrue(result.Item1.CompareTo("AX87") == 0);

            Assert.IsTrue(result.Item2["AX55"] == 32);
        }

        //Related to getting MST
        [TestMethod]
        public void TestMST_CreateDictionary_EmptyData()
        {
            var mst = new MST<string>();

            string edgeData = "";

            Assert.ThrowsException<ArgumentNullException>( () => mst.CreateDictionary(edgeData));
        }

        //Related to getting MST
        [TestMethod]
        public void TestMST_CreateDictionary_NullInput()
        {
            var mst = new MST<string>();

            Assert.ThrowsException<ArgumentNullException>(() => mst.CreateDictionary(null));
        }
    }
}
