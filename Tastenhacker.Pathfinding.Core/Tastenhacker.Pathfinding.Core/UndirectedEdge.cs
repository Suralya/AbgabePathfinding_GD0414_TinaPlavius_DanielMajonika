//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

namespace Tastenhacker.Pathfinding.Core
{
    /// <summary>
    /// Represents undirected connection between two vertices
    /// </summary>
    /// <typeparam name="E">Type for user data associated with edges</typeparam>
    /// <typeparam name="V">Type for user data associated with vertices</typeparam>
    public class UndirectedEdge<E, V> : Edge<E, V>
    {
        /// <summary>
        /// Create new undirected edge
        /// </summary>
        /// <param name="firstVertex">First Vertex</param>
        /// <param name="secondVertex">Second Vertex</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="name">Edge name</param>
        public UndirectedEdge(Vertex<V> firstVertex, Vertex<V> secondVertex, int weight = 0, string name = null) : base(firstVertex, secondVertex, weight, name)
        {
        }

        /// <summary>
        /// Swaps direction of edge
        /// </summary>
        public override void ReverseDirection()
        {
        }

        /// <summary>
        /// Checks for value equality
        /// </summary>
        /// <param name="edge">Edge to check against</param>
        /// <returns>true if base vertices and target vertices are the same</returns>
        public override bool Equals(Edge<E, V> edge)
        {
            return Weight.Equals(edge.Weight) && ((BaseVertex.Equals(edge.BaseVertex) && TargetVertex.Equals(edge.TargetVertex)) || (BaseVertex.Equals(edge.TargetVertex) && TargetVertex.Equals(edge.BaseVertex)));
        }

        /// <summary>
        /// </summary>
        /// <returns>Default string for this Edge</returns>
        public override string GetInitialString()
        {
            string mGraphvizString = baseVertex.GetInitialString() + "--" + targetVertex.GetInitialString();
            return mGraphvizString;
        }
    }
}
