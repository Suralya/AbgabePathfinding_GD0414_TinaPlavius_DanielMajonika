//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

namespace Tastenhacker.Pathfinding.Core
{
    /// <summary>
    /// Extension of Vertex class which is used internally to be able to modify degree counts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class ExtendedVertex<T> : Vertex<T>
    {
        /// <summary>
        /// Number of incoming and outgoing edges
        /// </summary>
        new public int Degree
        {
            get { return degreeIn + degreeOut; }
            set { degreeIn = 0; degreeOut = value; }
        }

        /// <summary>
        /// Number of incoming edges
        /// </summary>
        new public int InDegree
        {
            get { return degreeIn; }
            set { degreeIn = value; }
        }

        /// <summary>
        /// Number of outgoing edges
        /// </summary>
        new public int OutDegree
        {
            get { return degreeOut; }
            set { degreeOut = value; }
        }

        /// <summary>
        /// Create new extended vertex (to be used internally)
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="name">Name associated with the vertex (generated automatically if null)</param>
        public ExtendedVertex(T _data, string name = null) : base(_data, name)
        {
        }
    }
}
