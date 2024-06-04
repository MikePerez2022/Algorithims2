using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
    public class Solver
    {
        public Maze<char> maze;

        /// <summary>
        /// Solver constructor builds the maze when called using the file path retrieve the maze data.<br/>
        /// It got the high worst case from all the methods that are calles.<br/><br/>
        /// Big O Notation: <br/> O(n^3)
        /// </summary>
        /// <param name="filePath">Takes in the path to the maze data</param>
        public Solver(string? filePath)
        {
            CreateMaze(filePath);
        }

        /// <summary>
        /// Checks if the file exsits if so reads the file and gets maze data as a set of strings.<br/>
        /// Loop through it and spereate the data into the correct sections and passes it into the maze constructor.<br/><br/>
        /// Big O Notation: <br/> O(n^3)
        /// </summary>
        /// <param name="file">Takes in the file location of the file that holds the maze data</param>
        /// <exception cref="ArgumentNullException">Throws exception if the file path doesn't exsist</exception>
        public void CreateMaze(string? file)
        {
            if (!File.Exists(file)) throw new ArgumentNullException();

            List<string>? mazeData = ReadFile(file);
            List<string> MazeNodes = new List<string>();
            List<string> edgesData = new List<string>();
            string[] StartEndNodes = new string [2];

            foreach (var dataSection in mazeData)
            {
                if (dataSection == mazeData[0]) MazeNodes = dataSection.Split(',').ToList();
                else if (dataSection == mazeData[1]) StartEndNodes = dataSection.Split(',');
                else edgesData.Add(dataSection);
            }

            maze = new Maze<char>(edgesData, MazeNodes, char.Parse(StartEndNodes[0]), char.Parse(StartEndNodes[1]));
        }

        /// <summary>
        /// reads each line of the text file and passes each as a string which can be added to a list.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="filePath">Takes in the file path to be read</param>
        /// <returns></returns>
        private List<string>? ReadFile(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            List<string> mazeStructData = File.ReadAllLines(filePath).ToList();

            return mazeStructData;
        }
    }
}
