//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

namespace Tastenhacker.Pathfinding.Core
{
    /// <summary>
    /// Represents directed connection between two vertices
    /// </summary>
    /// <typeparam name="E">Type for user data associated with edges</typeparam>
    /// <typeparam name="V">Type for user data associated with vertices</typeparam>
    public class DirectedEdge<E, V> : Edge<E, V>
    {
        /// <summary>
        /// Create new directed edge
        /// </summary>
        /// <param name="baseVertex">Base vertex</param>
        /// <param name="targetVertex">Target vertex</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="name">Edge name</param>
        public DirectedEdge(Vertex<V> baseVertex, Vertex<V> targetVertex, int weight = 0, string name = null) : base(baseVertex, targetVertex, weight, name)
        {
            //decorator = new DirectedEdgeDecorator<V>(baseVertex, targetVertex);
        }

        /// <summary>
        /// Swaps direction of edge
        /// </summary>
        public override void ReverseDirection()
        {
            ExtendedVertex<V> bVertex = (ExtendedVertex<V>)BaseVertex;
            ExtendedVertex<V> tVertex = (ExtendedVertex<V>)TargetVertex;

            bVertex.OutDegree--;
            bVertex.InDegree++;
            tVertex.InDegree--;
            tVertex.OutDegree++;

            BaseVertex = tVertex;
            TargetVertex = bVertex;
        }


        /// <summary>
        /// Creates the default string for this Edge.
        /// </summary>
        /// <returns>Default string for this Edge</returns>
        public override string GetInitialString()
        {
            string mGraphvizString = baseVertex.GetInitialString() + "->" + targetVertex.GetInitialString();
            return mGraphvizString;
        }
    }
}
