using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
    public class Maze<T> where T : IComparable<T>
    {
        public Graph<T>? graph;
        internal Node<T>? start;
        internal Node<T>? end;
        Dictionary<T, Dictionary<T, int>> edgesDictionary = new Dictionary<T, Dictionary<T, int>>();

        /// <summary>
        /// Loops through the edge data strings and adds each to their corresponding node values into a nested dictionary.<br/>
        /// The dictionary inside is set to the outupt of the sort edges function which is another dictionary. Then creates<br/>
        /// the graph and loops through all the nodes to set the start and end nodes.<br/><br/>
        /// Big O Notation: <br/> O(n^3)
        /// </summary>
        /// <param name="nodesEdgeStrings">Takes in the list of strings containing edge information</param>
        /// <param name="nodesValues">Takes in the list of node values</param>
        /// <param name="startValue">Takes in the start node's values</param>
        /// <param name="endValue">Takes in the end node's value</param>
        public Maze(List<string> nodesEdgeStrings, List<string> nodesValues, T startValue, T endValue)
        {
            foreach(var edgesString in nodesEdgeStrings)
            {
                //Got this from stack overflow when searching how to convert something to a generic type
                T key = (T)Convert.ChangeType(edgesString[0], typeof(T));
                edgesDictionary.Add(key, sortEdges(edgesString));
            }

            graph = new Graph<T>(nodesValues, edgesDictionary);
            foreach(var node in graph.nodes)
            {
                if (node.Value.CompareTo(startValue) == 0) start = node;
                else if (node.Value.CompareTo(endValue) == 0) end = node;
            }
        }

        /// <summary>
        /// Splits up the edge data strings and removes the key value then splits the edge pairs and creates<br/>
        /// dictionary pairs and sets the data to those pairs.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="edgesString">Takes in a string holding all edges of a specific node</param>
        /// <returns></returns>
        public Dictionary<T, int> sortEdges(string edgesString)
        {
            Dictionary<T, int> edges = new Dictionary<T, int>();
            List<string>? pairs = edgesString.Split(',').ToList();
            pairs.RemoveAt(0);

            foreach(var pair in pairs)
            {
                var data = pair.Split(':');
                edges.Add((T)Convert.ChangeType(data[0], typeof(T)), int.Parse(data[1]));
            }
            return edges;
        }

        //djikstras
        //worst case : O(n^2)
        /// <summary>
        /// Runs the dijkstra algoritnim and sets the start node distance to zero then enques it it start the search.<br/>
        /// It loops through the priority queue is empty and the lowest cost path is found by adding distance to nodes as traversed<br/>
        /// and keeping track of the previous node with the shortest path and when done e is given it's shortest path distance.<br/>
        /// After that it loops in reverse from the end node and creates a path.<br/><br/>
        /// Big O Notation: <br/> O(n^2)
        /// </summary>
        /// <returns>The shortes path to the end node and the distance in a tuple</returns>
        public (Stack<T>?, int?) getShortestPath()//T startValue, T endValue
        {
            if (start == null || end == null) return (null, int.MaxValue);
            else start.Distance = 0;

            PriorityQueue<Node<T>, int> priorityQueue = new PriorityQueue<Node<T>, int>();
            priorityQueue.Enqueue(start, 0);

            Node<T>? currentNode;
            int currentDistance;

            while (priorityQueue.Count > 0)
            {
                priorityQueue.TryDequeue(out currentNode, out currentDistance);

                if (currentDistance > currentNode.Distance) continue;
                foreach (var edge in currentNode.Edges)
                {
                    var distanceToEdgeNode = edge.Weight + currentDistance;
                    if (distanceToEdgeNode < edge.Node.Distance)
                    {
                        edge.Node.SetPrevious(currentNode);
                        priorityQueue.Enqueue(edge.Node, (int)distanceToEdgeNode);
                    }
                }
            }

            Stack<T>? path = new Stack<T>();
            currentNode = end;
            while (currentNode != null)
            {
                path.Push(currentNode.Value);
                currentNode = currentNode.previous;
            }

            return (path, end.Distance);
        }
    }
}
