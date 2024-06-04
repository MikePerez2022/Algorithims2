using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
    internal class Edge<T> where T : IComparable<T>
    {
        public Node<T> Node { get; set; }

        public int Weight { get; set; }

        /// <summary>
        /// Sets the connected node and distance using parameters.<br/><br/>
        /// Big O Notation: <br/> O(1)
        /// </summary>
        /// <param name="node">Takes in a node that is on the other side as a connecting node</param>
        /// <param name="weight">Takes in an integer that represents distance from node to this one</param>
        public Edge(Node<T> node, int weight)
        {
            Node = node;
            Weight = weight;
        }
    }
}
