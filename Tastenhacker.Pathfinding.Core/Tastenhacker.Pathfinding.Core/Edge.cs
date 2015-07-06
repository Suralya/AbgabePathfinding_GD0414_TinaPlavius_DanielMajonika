//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System;

namespace Tastenhacker.Pathfinding.Core
{
    /// <summary>
    /// Represents connection between two vertices
    /// </summary>
    /// <typeparam _name="E">Type for user data associated with edges</typeparam>
    /// <typeparam _name="V">Type for user data associated with vertices</typeparam>
    public abstract class Edge<E, V> : IComparable
    {
        private readonly UInt64 _id;
        private static UInt64 idGenerator = 0;
        private string _name;

        public bool Marked;

        /// <summary>
        /// Weight of a specific edge, e.g. costs to travel this edge
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Every ending of an edge is defined by 
        /// </summary>
        protected Vertex<V> baseVertex, targetVertex;

        /// <summary>
        /// First Vertex
        /// </summary>
        public Vertex<V> BaseVertex
        {
            get { return baseVertex; }
            set { baseVertex = value; }
        }

        /// <summary>
        /// Second Vertex
        /// </summary>
        public Vertex<V> TargetVertex
        {
            get { return targetVertex; }
            set { targetVertex = value; }
        }

        /// <summary>
        /// User data that can be connected to this Edge
        /// </summary>
        public E Data { get; set; }

        /// <summary>
        /// ID of edge
        /// </summary>
        public UInt64 ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Returns the _name given to the edge
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Create new edge
        /// </summary>
        /// <param _name="baseVertex">First Vertex</param>
        /// <param _name="targetVertex">Second Vertex</param>
        /// <param _name="weight">Edge weight</param>
        /// <param _name="name">Edge _name</param>
        protected Edge(Vertex<V> baseVertex, Vertex<V> targetVertex, int weight = 0, string name = null)
        {
            BaseVertex = baseVertex;
            TargetVertex = targetVertex;
            Weight = weight;
            _id = ++idGenerator;
            Marked = false;

            this._name = name ?? "edge-" + ID;
        }

        /// <summary>
        /// Check if edge has the given ID
        /// </summary>
        /// <param _name="_ID">ID to check</param>
        /// <returns></returns>
        public bool HasID(UInt64 _ID)
        {
            return ID == _ID;
        }

        /// <summary>
        /// Swaps direction of edge
        /// </summary>
        public abstract void ReverseDirection();

        /// <summary>
        /// Checks for value equality
        /// </summary>
        /// <param _name="obj">Object to check against</param>
        /// <returns>true if obj is of same type and value equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Edge<E, V> edge = obj as Edge<E, V>;

            return edge != null && edge.Equals(this);
        }

        /// <summary>
        /// Checks for value equality
        /// </summary>
        /// <param _name="edge">Edge to check against</param>
        /// <returns>true if base vertices and target vertices are the same</returns>
        public virtual bool Equals(Edge<E, V> edge)
        {
            return  Weight.Equals(edge.Weight) && BaseVertex == edge.BaseVertex && TargetVertex == edge.TargetVertex;
        }

        public override int GetHashCode()
        {
            return (int)BaseVertex.ID ^ (int)TargetVertex.ID;
        }

        public int CompareTo(object obj)
        {
            if (obj is Edge<E, V>)
            {
                Edge<E, V> edge = (Edge<E, V>)obj;

                return GetHashCode() - edge.GetHashCode();
            }
            if (obj == null)
            {
                return 1;
            }

            System.Diagnostics.Debug.WriteLine("Not an edge: " + obj.GetType());

            throw new ArgumentException("object is not an Edge<E, V>");
        }

       
        public override string ToString()
        {
            return Name;
        } 
    }
}
