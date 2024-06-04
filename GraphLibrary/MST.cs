using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GraphLibrary
{
    public class MST<T> where T : IComparable<T>
    {
        public Graph<T>? graph;
        public Dictionary<T, Dictionary<T, int>> edgesDictionary = new Dictionary<T, Dictionary<T, int>>();

        /// <summary>
        /// Creates the graph<br/><br/>
        /// Big O Notation: <br/> O(n^3)
        /// </summary>
        /// <param name="FilePath">The path to the file holding the graph structure</param>
        public MST(string? FilePath)
        {
            CreateGraph(FilePath);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MST()
        {
            graph = new Graph<T>();
        }

        /// <summary>
        /// checks if the graph is null and if not calls the prim algorithim and passes in all the nodes in the graph.<br/>
        /// using the result add up all the nodes to the socketSet string in the correct format and cormat the second string in the return.<br/><br/>
        /// Big O Notation: <br/> O(n^2)
        /// </summary>
        /// <returns>a tuple containing the sockets in the mst and the cable needed to connect them</returns>
        public (string?, string?) GetMST()
        {
            if (graph == null) return(null,null);

            var result = PrimAlgorithim(graph.nodes.FirstOrDefault());
            string SocketSet = "Socket Set: ";
            //int Cable = 0;

            foreach ( var node in result.Item1 )
            {
                SocketSet += (node == result.Item1.Last()) ? node.ToString() : node.ToString() + ", ";//O(1)
            }

            return (SocketSet, $"Cable Needed: {result.Item2}ft");
        }

        /// <summary>
        /// Finds the values for an MST by adding the root node to the visited nodes and adding all the edge nodes to the priotity queue.<br/>
        /// Loops through every edge in the graph using the queue and checking if it is contained in the visited nodes list.<br/>
        /// If it is skip over it otherwise add the node to visited nodes and add up the distance. Then enqueue that node's edge nodes.<br/><br/>
        /// Big O Notation: <br/> O(n^2)
        /// 
        /// </summary>
        /// <param name="RootNode">Takes in node where search starts</param>
        /// <returns>a tuple holding the list of nodes and the amount of cable to connect for a MST</returns>
        private (List<Node<T>>?, int?) PrimAlgorithim(Node<T>? RootNode)//n^2
        {
            if (RootNode == null) throw new NullReferenceException();

            PriorityQueue<Node<T>?, int> priorityQueue = new PriorityQueue<Node<T>?, int>();

            List<Node<T>>? visitedNodes = new List<Node<T>>();
            int? cableLength = 0;
            visitedNodes.Add(RootNode);

            foreach(var item in RootNode.Edges) priorityQueue.Enqueue(item.Node, item.Weight);

            while (priorityQueue.Count > 0)
            {
                priorityQueue.TryDequeue(out var vistedNode, out var distance);
                if (vistedNode == null || visitedNodes.Contains(vistedNode)) continue;
                visitedNodes.Add(vistedNode);
                cableLength += distance;
                foreach (var item in vistedNode.Edges) priorityQueue.Enqueue(item.Node, item.Weight);

            }

            return(visitedNodes, cableLength);
        }

        /// <summary>
        /// Checks if the file exsits if so extracts the data from the file into a tuple containing a list of nodes and edges.<br/>
        /// Loops through the edges and gets passes into the create dictionary function which returns a tuple of the key and dictionary.<br/>
        /// using the CreateDictionary first items the key is coverted to T and both the key and the second item are used to make a dictionary<br/>
        /// then graph is constructed with the dictionary and the list of nodes.<br/><br/>
        /// Big O Notation: <br/> O(n^3)
        /// </summary>
        /// <param name="file">Takes in the file location of the file that holds the maze data</param>
        /// <exception cref="ArgumentNullException">Throws exception if the file path doesn't exsist</exception>
        public void CreateGraph(string? file)
        {
            if (!File.Exists(file)) throw new ArgumentNullException();

            var NetworkData = ExtractDataFromFile(file);

            if(NetworkData.Item1 == null || NetworkData.Item2 == null) throw new NullReferenceException();

            foreach (var edgesString in NetworkData.Item2)
            {
                var dictionary = CreateDictionary(edgesString);
                T key = (T)Convert.ChangeType(dictionary.Item1, typeof(T));
                edgesDictionary.Add(key, dictionary.Item2);
            }

            graph = new Graph<T>(NetworkData.Item1, edgesDictionary);
        }

        /// <summary>
        /// Splits up the edge data strings and pulls out key value then splits the edge pairs and creates<br/>
        /// dictionary pairs and sets the data to those pairs.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="edgesString">Takes in a string holding all edges of a specific node</param>
        /// <returns>a tuple holding the key and the edge dictionary</returns>
        public (string, Dictionary<T, int>) CreateDictionary(string? edgesString)
        {
            if(edgesString == null || edgesString == "") throw new ArgumentNullException();

            Dictionary<T, int> edges = new Dictionary<T, int>();
            List<string>? pairs = edgesString.Split(',').ToList();
            string key = pairs[0];
            pairs.RemoveAt(0);

            foreach (var pair in pairs)
            {
                var data = pair.Split(':');
                edges.Add((T)Convert.ChangeType(data[0], typeof(T)), int.Parse(data[1]));
            }
            return (key, edges);
        }

        /// <summary>
        /// First checks if the file exists and if so it will turn all lines of the file to strings and return that list.<br/>
        /// Then it loops through the array and sets the first to the list of nodes and the rest are added to the edgesData.<br/><br/><rb/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>A tuple holding the nodes and edge data</returns>
        /// <exception cref="NullReferenceException">throws exception if file path doesn't exsist</exception>
        public (List<string>?, List<string>?) ExtractDataFromFile(string? filePath)//O(n)
        {
            if (!File.Exists(filePath)) throw new NullReferenceException();

            List<string> NetworkFileData = File.ReadAllLines(filePath).ToList();

            List<string> NetworkNodes = new List<string>();
            List<string> edgesData = new List<string>();

            foreach (var dataSection in NetworkFileData)
            {
                if (dataSection == NetworkFileData[0]) NetworkNodes = dataSection.Split(',').ToList();
                else edgesData.Add(dataSection);
            }

            return (NetworkNodes, edgesData);
        }
    }
}
