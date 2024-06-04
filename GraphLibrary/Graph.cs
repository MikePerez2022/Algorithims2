using System.IO;

namespace GraphLibrary
{
    public class Graph<T> where T : IComparable<T>
    {
        internal List<Node<T>> nodes = new List<Node<T>>();
        //KEY = node value      VALUE = key = connected node value/edge node value     value = edge weight
        //Dictionary<T, Dictionary<T, int>> edgesDictionary = new Dictionary<T, Dictionary<T, int>>();

        /// <summary>
        /// loops through the string values and creates and add the node's to a list using them. <br/>
        /// Then loop through and add the edges the specific nodes they are contained by.<br/><br/>
        /// Big O Notation: <br/> O(n^3)
        /// </summary>
        /// <param name="nodeValues">Takes in a list of strings holding all node values</param>
        /// <param name="nodeEdges">Takes in a nested dictionary holding all edges contained in specific nodes</param>
        public Graph(List<string> nodeValues, Dictionary<T, Dictionary<T, int>> nodeEdges)
        {
            foreach(var value in nodeValues)
            {
                T val = (T)Convert.ChangeType(value, typeof(T));
                AddNode(val);
            }

            foreach(var node in nodes)
            {
                if (!nodeEdges.Keys.Contains(node.Value)) continue;
                AddNodeEdges(node, nodeEdges[node.Value]);
            }
        }

        public Graph()
        {

        }

        /// <summary>
        /// This loops through all the edges and all the nodes to find a match and add the edge to the node containing it.<br/><br/>
        /// Big O Notation: <br/> O(n^2)
        /// </summary>
        /// <param name="node">Takes in the node edges are added to</param>
        /// <param name="edges">Takes in the edges dictionary linked to the node</param>
        private void AddNodeEdges(Node<T> node, Dictionary<T, int> edges)//O(n^2)
        {
            foreach (var edge in edges)
            {
                foreach (var edgeNode in nodes)
                    if (edgeNode.Value.CompareTo(edge.Key) == 0) node.AddEdge(edgeNode, edge.Value);
            }
        }

        /// <summary>
        /// loops throug the nodes to find duplicates and if none are found a new node is made and added to the node list.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="value">Takes in the unique value of a node</param>
        public void AddNode(T value)//O(n)
        {
            foreach(var node in nodes) if (node.Value.CompareTo(value) == 0) return;
            var newNode = new Node<T>(value);
            nodes.Add(newNode);
        }

        /// <summary>
        /// Returns all nodes in the graph formatted in a string list.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            if (nodes.Count == 0) return "";

            string result = "";

            foreach(var node in nodes)
            {
                result += (node == nodes.Last()) ? node.ToString() : node.ToString() + ", ";
            }

            return result;
        }
    }
}
