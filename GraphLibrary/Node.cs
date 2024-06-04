using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
    internal class Node<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public List<Edge<T>> Edges { get; set; }
        public Node<T>? previous { get; set; }
        public int? Distance { get; set; }

        /// <summary>
        /// The constructor that sets the node value to the parameter and sets the distance to infinte.<br/>
        /// Initializes the node.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="value">Takes in the unique node value represending a node</param>
        public Node(T value)
        {
            Value = value;
            previous = null;
            Distance = int.MaxValue;
            Edges = new List<Edge<T>>();
        }

        /// <summary>
        /// Creates an edge using the parameters and and adds it to the edges list on the node.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="node">Takes in the connecting node</param>
        /// <param name="weight">Takes in the distance to the connecting node</param>
        public void AddEdge(Node<T> node, int weight)
        {
            //nodes and edges are unique
            //var node = new Node<T>(nodeVal);
            var edge = new Edge<T>(node, weight);
            Edges.Add(edge);
        }

        /// <summary>
        /// Sets the pervious of the node to the parameter and finds the edge weight.<br/>
        /// Using that add it to the previous node's distance and set that to the new distance from start node.<br/><br/>
        /// Big O Notation: <br/> O(n)
        /// </summary>
        /// <param name="node">Takes in the previous node</param>
        public void SetPrevious(Node<T> node)
        {
            previous = node;
            //distance to a node from start is found by 
            //distance between this node and previous node (Edge Weight)
            // + distance from start to previous node
            //consider nodes to be unique by value
            int? EdgeWeight = 0;
            foreach(var edge in Edges)
            {
                if(edge.Node.Value.CompareTo(node.Value) == 0)
                {
                    EdgeWeight = edge.Weight;
                    break;
                }
            }

            int? PreviousDistance = node.Distance;
            Distance = EdgeWeight + PreviousDistance;
        }

        /// <summary>
        /// Uses a concat to turn the node value into a string.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return string.Concat(Value);
        }
    }
}
