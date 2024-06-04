using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphLibrary;

namespace TestProject
{
    //Only needed five unit tests for the assignment

    [TestClass]
    public class MazeSolverTests
    {
        [TestMethod]
        public void TestMazeSolver_CreateGraph_HappyPath()
        {
            var solver = new Solver("..\\..\\..\\..\\GraphLibrary\\mazeStructure1.txt");

            Assert.IsNotNull(solver.maze.graph);
        }

        [TestMethod]
        public void TestMazeSolver_CreateMaze_HappyPath()
        {
            var solver = new Solver("..\\..\\..\\..\\GraphLibrary\\mazeStructure1.txt");

            Assert.IsNotNull(solver.maze);
        }

        [TestMethod]
        public void TestMazeSolver_CreateMaze_NonExsistentFile()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Solver("C:t"));
        }

        [TestMethod]
        public void TestMazeSolver_GetShortestPath_HappyPath()
        {
            var solver = new Solver("..\\..\\..\\..\\GraphLibrary\\mazeStructure1.txt");
            
            var result = solver.maze.getShortestPath();

            var actual = result.Item1.LastOrDefault();

            Assert.IsTrue('E' == actual);
            Assert.IsTrue(3 == result.Item2);
        }

        [TestMethod]
        public void TestMazeSolver_SolveWeightedEdges_HappyPath()
        {
            //edges arent all set to 1 and this show multiple mazes can be run
            var solver = new Solver("..\\..\\..\\..\\GraphLibrary\\maze.txt");

            var result = solver.maze.getShortestPath();

            Assert.IsTrue(5 == result.Item1.Count);
            //A B D F E
        }


        [TestMethod]
        public void TestMazeSolver_DeadEnd_HappyPath()
        {
            var solver = new Solver("..\\..\\..\\..\\GraphLibrary\\mazeDeadEnd.txt");

            var result = solver.maze.getShortestPath();

            Assert.IsTrue(4 == result.Item1.Count);
        }



        [TestMethod]
        public void TestMazeSolver_NoSolution_HappyPath()
        {
            var solver = new Solver("..\\..\\..\\..\\GraphLibrary\\mazeNoSolution.txt");

            var result = solver.maze.getShortestPath();

            Assert.IsTrue(result.Item1.Count == 1);
            Assert.IsTrue(result.Item2 == int.MaxValue);
        }
    }
}
