﻿//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System;
using System.Collections.Generic;

namespace Tastenhacker.Pathfinding.Core
{
    public class DirectedGraph<E, V> : Graph<E, V>
    {
        /// <summary>
        /// Creates a new directed graph
        /// </summary>
        /// <param name="name">Name associated with the graph (generated automatically if null)</param>
        public DirectedGraph(string name = null) : base(name)
        {
        }

        /// <summary>
        /// Returns if this Edge exits (Edge is described by two vertices), complexity: O (|E|)
        /// </summary>
        /// <param name="from">first vertex of the edge to test</param>
        /// <param name="to">second vertex of the edge to test</param>
        public override bool EdgeExists(Vertex<V> from, Vertex<V> to)
        {
            DirectedEdge<E, V> edge;

            return EdgeExists(from, to, out edge);
        }

        /// <summary>
        /// Returns if this Edge exits (Edge is described by two vertices), complexity: O (|E|)
        /// </summary>
        /// <param name="from">first vertex of the edge to test</param>
        /// <param name="to">second vertex of the edge to test</param>
        /// <param name="foundEdge">Edge (if an edge build by these vertices exits)</param>
        public bool EdgeExists(Vertex<V> from, Vertex<V> to, out DirectedEdge<E, V> foundEdge)
        {
            //both vertices have to exist
            if (VertexExists(from) && VertexExists(to))
            {
                foreach (DirectedEdge<E, V> edge in edges.Values)
                {
                    if (edge.BaseVertex == from && edge.TargetVertex == to)
                    {
                        foundEdge = edge;
                        return true;
                    }
                }
            }

            foundEdge = null;
            return false;
        }

        /// <summary>
        /// Returns a list of all incoming edges that are connected to this vertex
        /// </summary>
        /// <param name="vertex">Vertex to find incoming edge ids for</param>
        /// <returns>List of incoming connected edge ids</returns>
        public List<UInt64> GetIncomingEdgeIds(Vertex<V> vertex)
        {
            List<UInt64> list = new List<UInt64>();

            foreach (KeyValuePair<UInt64, Edge<E, V>> pair in edges)
            {
                if (pair.Value.TargetVertex == vertex)
                {
                    list.Add(pair.Value.ID);
                }
            }

            return list;
        }

        /// <summary>
        /// Returns a list of all incoming edges
        /// </summary>
        /// <param name="vertex">Vertex to find incoming edges for</param>
        /// <returns>List of incoming edges</returns>
        public List<Edge<E, V>> GetIncomingEdges(Vertex<V> vertex)
        {
            List<Edge<E, V>> fEdges = new List<Edge<E, V>>();

            foreach (KeyValuePair<UInt64, Edge<E, V>> pair in edges)
            {
                if (pair.Value.TargetVertex == vertex)
                {
                    fEdges.Add(pair.Value);
                }
            }

            return fEdges;
        }

        /// <summary>
        /// Returns a list of all outgoing edges
        /// </summary>
        /// <param name="vertex">Vertex to find connected outgoing edges for</param>
        /// <returns>List of all outgoing edges</returns>
        public List<Edge<E, V>> GetOutgoingEdges(Vertex<V> vertex)
        {
            List<Edge<E, V>> fEdges = new List<Edge<E, V>>();

            foreach (KeyValuePair<UInt64, Edge<E, V>> pair in edges)
            {
                if (pair.Value.BaseVertex == vertex)
                {
                    fEdges.Add(pair.Value);
                }
            }

            return fEdges;
        }

        /// <summary>
        /// Add new edge to directed graph
        /// </summary>
        /// <param name="v1">Base vertex</param>
        /// <param name="v2">Target vertex</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="name">Edge name</param>
        /// <returns>Added edge</returns>
        public override Edge<E, V> AddEdge(Vertex<V> v1, Vertex<V> v2, int weight = 0, string name = null)
        {
            if (EdgeExists(v1, v2))
            {
                throw new DuplicateEdgeException();
            }

            Edge<E, V> edge = new DirectedEdge<E, V>(v1, v2, weight, name);

            return AddEdge(edge);
        }

        /// <summary>
        /// Checks for value equality
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>true if obj is of same type and value equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            DirectedGraph<E, V> graph = obj as DirectedGraph<E, V>;

            if (graph == null)
            {
                return false;
            }

            return graph.Equals(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Creates the default string for this Graph.
        /// </summary>
        /// <returns>Default string for this Graph</returns>
        public override string GetInitialString()
        {
            string mInitString = "digraph G {" + Environment.NewLine;
            return mInitString;
        }

        /// <summary>
        /// Return a new DirectedGraph
        /// </summary>
        /// <returns>New DirectedGraph</returns>
        protected override Graph<E, V> NewGraph()
        {
            return new DirectedGraph<E, V>();
        }

        /// <summary>
        /// Gets Adjacency of an Graph by checking connections of Vertices
        /// </summary>
        public bool[][] Adjacency_Dir()
        {
            List<Vertex<V>> fVertices = GetVertices();

            int cv = fVertices.Count;

            bool[][] arr = new bool[cv][];

            for (int i = 0; i < cv; i++)
            {
                arr[i] = new bool[cv];
                for (int j = 0; j < cv; j++)
                {
                    arr[i][j] = false;
                }
            }
            foreach (Vertex<V> v in fVertices)
            {
                List<Edge<E, V>> Outgoing = GetOutgoingEdges(v);
                foreach(Edge<E, V> e in Outgoing)
                {
                    int Iv = fVertices.IndexOf(v);
                    int Ie = fVertices.IndexOf(e.TargetVertex);

                    arr[Iv][Ie] = true;
                }
            }
            return arr;
        }
    }
}
