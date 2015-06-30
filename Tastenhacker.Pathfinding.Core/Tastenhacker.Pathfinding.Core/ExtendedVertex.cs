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
            get { return DegreeIn + DegreeOut; }
            set { DegreeIn = 0; DegreeOut = value; }
        }

        /// <summary>
        /// Number of incoming edges
        /// </summary>
        new public int InDegree
        {
            get { return DegreeIn; }
            set { DegreeIn = value; }
        }

        /// <summary>
        /// Number of outgoing edges
        /// </summary>
        new public int OutDegree
        {
            get { return DegreeOut; }
            set { DegreeOut = value; }
        }

        /// <summary>
        /// Create new extended vertex (to be used internally)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="name">Name associated with the vertex (generated automatically if null)</param>
        public ExtendedVertex(T data, string name = null) : base(data, name)
        {
        }
    }
}
