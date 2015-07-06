﻿//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tastenhacker.Pathfinding.Core
{
    public abstract class GraphException: Exception{
        public GraphException() { }
        public GraphException(string message) : base(message) { }
    }
    public class DuplicateEdgeException : GraphException {}
    public class EdgeNotFoundException : GraphException {}
    public class VertexNotFoundException : GraphException {}
    public class PathNotFoundException : GraphException {}
    public abstract class Graph<E,V> : IEnumerable
    {
        protected Dictionary<UInt64, Edge<E, V>> edges;
        protected Dictionary<UInt64, Vertex<V>> vertices;
        private readonly UInt64 id;
        private static UInt64 idGenerator = 0;
        private readonly string name;


        /// <summary>
        /// Creates a new graph
        /// </summary>
        /// <param name="name">Name associated with the graph (generated automatically if null)</param>
        public Graph(string name = null)
        {
            edges = new Dictionary<UInt64, Edge<E, V>>();
            vertices = new Dictionary<UInt64, Vertex<V>>();
            id = ++idGenerator;

            this.name = name ?? "graph-" + ID;
        }

        /// <summary>
        /// </summary>
        /// <param name="v"></param>
        /// <todo>handle exceptions</todo>
        protected void AddVertex(Vertex<V> v)
        {
            vertices.Add(v.ID, v);
        }

        /// <summary>
        /// Create new vertex and add it to the graph
        /// </summary>
        /// <returns>Newly created vertex</returns>
        public Vertex<V> CreateVertex(V data, string fName = null)
        {
            Vertex<V> v = new ExtendedVertex<V>(data, fName);
            AddVertex(v);
            return v;
        }

        /// <summary>
        /// Returns the total number of vertices in this list
        /// </summary>
        public int VertexCount
        {
            get { return vertices.Count; }
        }

        /// <summary>
        /// Returns the name given to the graph
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// ID of graph
        /// </summary>
        public UInt64 ID
        {
            get { return id; }
        }

        /// <summary>
        /// Returns if this Vertex exists
        /// </summary>
        /// <param name="vertex">Vertex to check</param>
        public bool VertexExists(Vertex<V> vertex)
        {
            return vertices.ContainsKey(vertex.ID);
        }

        /// <summary>
        /// Returns if this Edge exits
        /// </summary>
        /// <param name="edge">Edge to check</param>
        public bool EdgeExists(Edge<E, V> edge)
        {
            return edges.Values.Any(element => element.Equals(edge));
        }

        /// <summary>
        /// Returns a list of all connected edges 
        /// </summary>
        /// <param name="vertex">Vertex to find connected edges for</param>
        /// <returns>List of connected edges</returns>
        public List<Edge<E,V>> GetConnectedEdges(Vertex<V> vertex)
        {
            return (from pair in edges where pair.Value.BaseVertex == vertex || pair.Value.TargetVertex == vertex select pair.Value).ToList();
        }

        /// <summary>
        /// Returns a List of all neighbour vertices of this vertex
        /// </summary>
        /// <param name="vertex">Vertex to search neighbours for</param>
        /// <returns></returns>
        public List<Vertex<V>> GetNeighbourVertices(Vertex<V> vertex, FilterEdges<E, V> edgeFilter = null)
        {
            List<Edge<E, V>> connectedEdges;
            if (edgeFilter == null)
                connectedEdges = GetConnectedEdges(vertex);
            else
                connectedEdges = GetConnectedEdges(vertex).Where(e => !edgeFilter(e)).ToList();

            return connectedEdges.Select(edge => edge.BaseVertex == vertex ? edge.TargetVertex : edge.BaseVertex).ToList();
        }


        /// <summary>
        /// Returns if this Edge exits (Edge is described by two vertices)
        /// </summary>
        /// <param name="a">first vertex of the edge to test</param>
        /// <param name="b">second vertex of the edge to test</param>
        public abstract bool EdgeExists(Vertex<V> a, Vertex<V> b);

        /// <summary>
        /// Add new edge to graph
        /// </summary>
        /// <param name="v1">First vertex</param>
        /// <param name="v2">Second vertex</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="name">Edge fname</param>
        /// <returns>Added edge</returns>
        public abstract Edge<E, V> AddEdge(Vertex<V> v1, Vertex<V> v2, int weight = 0, string name = null);



        /// <summary>
        /// Looks for cycles in graph (directed and undirected)
        /// </summary>
        /// <returns>true if IsAcyclic</returns>
        public bool IsAcyclic()
        {
            UnmarkAllObjects();

            List<Vertex<V>> vertexList = GetVertices();

            bool lookAtVertices;
            int markedVertexCount = 0;

            do
            {
                lookAtVertices = false;

                foreach (Vertex<V> vertex in vertexList)
                {
                    if (vertex.Marked == false)
                    {
                        if (vertex.InDegree <= 1 || vertex.OutDegree <= 1)
                        {
                            lookAtVertices = true;
                            markedVertexCount++;

                            vertex.Marked = true;
                            List<Edge<E, V>> edgeList = GetConnectedEdges(vertex);

                            foreach (Edge<E, V> edge in edgeList)
                            {
                                edge.Marked = true;

                                if (vertex == edge.BaseVertex)
                                {
                                    ExtendedVertex<V> v = (ExtendedVertex<V>)edge.TargetVertex;
                                    v.InDegree--;
                                }
                                else
                                {
                                    ExtendedVertex<V> v = (ExtendedVertex<V>)edge.BaseVertex;
                                    v.OutDegree--;
                                }
                            }
                        }
                    }
                }
            }
            while (lookAtVertices);

            return markedVertexCount == vertexList.Count;
        }

        /// <summary>
        /// Add new edge to graph
        /// </summary>
        /// <param name="edge">Edge to add</param>
        /// <returns>True if edge was added to graph</returns>
        protected Edge<E, V> AddEdge(Edge<E, V> edge)
        {
            if (!EdgeExists(edge))
            {
                if (!VertexExists(edge.BaseVertex) || !VertexExists(edge.TargetVertex))
                {
                    throw new VertexNotFoundException();
                }

                edges.Add(edge.ID, edge);

                ExtendedVertex<V> baseVertex = (ExtendedVertex<V>)edge.BaseVertex;
                ExtendedVertex<V> targetVertex = (ExtendedVertex<V>)edge.TargetVertex;

                baseVertex.OutDegree++;
                targetVertex.InDegree++;

                return edge;
            }
            throw new DuplicateEdgeException();
        }

        /// <summary>
        /// Removes edge from graph
        /// </summary>
        /// <param name="edge">Edge to remove</param>
        /// <returns></returns>
        public bool RemoveEdge(Edge<E, V> edge)
        {
            if (EdgeExists(edge))
            {
                edges.Remove(edge.ID);

                ExtendedVertex<V> baseVertex = (ExtendedVertex<V>)edge.BaseVertex;
                ExtendedVertex<V> targetVertex = (ExtendedVertex<V>)edge.TargetVertex;

                baseVertex.OutDegree--;
                targetVertex.InDegree--;

                return true;
            }
            throw new EdgeNotFoundException();
        }

        /// <summary>
        /// Remove vertex and all edges attached to it from graph
        /// </summary>
        /// <param name="vertex">Vertex to remove</param>
        public void RemoveVertex(Vertex<V> vertex)
        {
            if (!VertexExists(vertex))
            {
                throw new VertexNotFoundException();
            }

            List<Edge<E, V>> connectedEdges = GetConnectedEdges(vertex);

            foreach (Edge<E, V> edge in connectedEdges)
            {
                RemoveEdge(edge);
            }

            vertices.Remove(vertex.ID);
        }

        /// <summary>
        /// Get enumerator for vertices in the graph
        /// </summary>
        /// <returns>Vertex enumerator</returns>
        public IEnumerator GetEnumerator()
        {
            return new VertexEnumerator<V>(GetVertices());
        }

        /// <summary>
        /// Get list of vertices in the graph
        /// </summary>
        /// <returns>List of vertices in the graph</returns>
        public List<Vertex<V>> GetVertices()
        {
            return vertices.Values.ToList();
        }

        /// <summary>
        /// Sets the unmark flag for all objects in the graph to false
        /// </summary>
        public void UnmarkAllObjects()
        {
            List<Vertex<V>> vertexList = GetVertices();

            foreach (Vertex<V> vertex in vertexList)
            {
                vertex.Marked = false;
            }

            List<Edge<E, V>> edgeList = GetEdges();

            foreach (Edge<E, V> edge in edgeList)
            {
                edge.Marked = false;
            }
        }

        /// <summary>
        /// Get enumerator for edges in the graph
        /// </summary>
        /// <returns>Vertex edges</returns>
        public IEnumerator GetEdgeEnumerator()
        {
            return new EdgeEnumerator<E, V>(GetEdges());
        }

        /// <summary>
        /// Get list of edges in the graph
        /// </summary>
        /// <returns>List of edges in the graph</returns>
        public List<Edge<E,V>> GetEdges()
        {
            return edges.Values.ToList();
        }

        public List<Vertex<V>> BreadthFirstSearch(Vertex<V> root, int depth, FilterEdges<E, V> edgeFilter, FilterVertex<V> vertexFilter)
        {
            List<Vertex<V>> ClosedList = new List<Vertex<V>>();
            List<Vertex<V>> OpenList = new List<Vertex<V>>();
            List<Vertex<V>> NewNodesPerIteration = new List<Vertex<V>>();
            List<Vertex<V>> OpenListMarkedAsRemoved = new List<Vertex<V>>();


            OpenList.Add(root);
            for (int iteration = 0; iteration <= depth; iteration++)
            {
                NewNodesPerIteration.Clear();
                for (int index =  OpenList.Count - 1; index >= 0; index--)
                {
                    Vertex<V> v = OpenList[index];
                    OpenListMarkedAsRemoved.Add(v);
                    ClosedList.Add(v);
                    foreach (Vertex<V> neighbour in GetNeighbourVertices(v, edgeFilter))
                    {
                        if (!ClosedList.Contains(neighbour) && !OpenList.Contains(neighbour) && !vertexFilter(neighbour))
                        {
                            NewNodesPerIteration.Add(neighbour);
                        }
                    }
                }
                OpenList.AddRange(NewNodesPerIteration);
                OpenList.RemoveAll(OpenListMarkedAsRemoved.Contains);
                OpenListMarkedAsRemoved.Clear();
            }
            return ClosedList.Distinct().ToList();
        }

        /// <summary>
        /// Search for vertex in graph breadth first
        /// </summary>
        /// <param name="root">Vertex to start search from</param>
        /// <param name="goal">Vertex to find, null to return all reachable vertices</param>
        /// <returns>Orderd list of vertices examined while searching for goal breadth first</returns>
        public List<Vertex<V>> BreadthFirstSearch(Vertex<V> root, Vertex<V> goal)
        {
            List<Vertex<V>> vertexList = new List<Vertex<V>>();
            Dictionary<Vertex<V>, bool> mark = vertices.Values.ToDictionary(vertex => vertex, vertex => false);

            Queue<Vertex<V>> queue = new Queue<Vertex<V>>();

            queue.Enqueue(root);

            vertexList.Add(root);

            mark[root] = true;

            while (queue.Count != 0)
            {
                Vertex<V> vertex = queue.Dequeue();

                if (vertex == goal)
                {
                    return vertexList;
                }

                List<Edge<E, V>> connectedEdges = GetConnectedEdges(vertex);

                foreach (Vertex<V> target in connectedEdges.Select(edge => edge.TargetVertex).Where(target => mark[target] == false))
                {
                    mark[target] = true;
                    queue.Enqueue(target);
                    vertexList.Add(target);
                }
            }
            return vertexList;
        }

        /// <summary>
        /// Search for vertex in graph depth first
        /// </summary>
        /// <param name="root">Vertex to start search from</param>
        /// <param name="goal">Vertex to find, null to return all reachable vertices</param>
        /// <returns>Ordered list of vertice examined while searching for goal depth first</returns>
        public List<Vertex<V>> DepthFirstSearch(Vertex<V> root, Vertex<V> goal)
        {
            Dictionary<Vertex<V>, bool> mark = vertices.Values.ToDictionary(vertex => vertex, vertex => false);

            List<Vertex<V>> vertexList = new List<Vertex<V>> {root};

            List<Vertex<V>> neighbours = GetNeighbourVertices(root);

            foreach (Vertex<V> vertex in neighbours.Where(vertex => DepthFirstSearch(vertex, goal, root, ref mark, ref vertexList)))
            {
            }

            return vertexList;
        }


        /// <summary>
        /// Search for vertex in graph depth first
        /// </summary>
        /// <param name="root">Vertex to start search from</param>
        /// <param name="goal">Vertex to find, null to return all reachable vertices</param>
        /// <param name="parent">Last examined parent</param>
        /// <param name="mark">Dictionary to mark examined vertices</param>
        /// <param name="VertexList">The list to sinert examined vertices into</param>
        /// <returns>Ordered list of vertice examined while searching for goal depth first</returns>
        private bool DepthFirstSearch(Vertex<V> root, Vertex<V> goal, Vertex<V> parent, ref Dictionary<Vertex<V>, bool> mark, ref List<Vertex<V>> VertexList)
        {
            VertexList.Add(root);

            if (root == goal)
            {
                return true;
            }
            List<Vertex<V>> neighbours = GetNeighbourVertices(root);

            neighbours.Remove(parent);

            foreach (Vertex<V> vertex in neighbours)
            {
                if (mark[vertex] == false)
                {
                    mark[vertex] = true;
                    if (DepthFirstSearch(vertex, goal, root, ref mark, ref VertexList))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get connected components of graph
        /// </summary>
        /// <returns>List of connected components, each component is represented as a list of its vertices</returns>
        public List<Graph<E, V>> GetConnectedComponents()
        {
            List<Graph<E, V>> components = new List<Graph<E, V>>();

            List<Vertex<V>> mark = vertices.Values.ToList();

            while (mark.Count != 0)
            {
                List<Vertex<V>> result = BreadthFirstSearch(mark[0], null);

                Graph<E, V> graph = NewGraph();
                List<Vertex<V>> list = new List<Vertex<V>>();

                foreach (Vertex<V> vertex in result)
                {
                    mark.Remove(vertex);

                    list.Add(graph.CreateVertex(vertex.Data, vertex.Name));
                }

                foreach (Vertex<V> vertex in result)
                {
                    List<Edge<E, V>> edges = GetConnectedEdges(vertex);

                    foreach (Edge<E, V> edge in edges)
                    {
                        try
                        {
                            graph.AddEdge(list[result.FindIndex(v => v == edge.BaseVertex)], list[result.FindIndex(delegate(Vertex<V> v) { return v == edge.TargetVertex; })]);
                        }
                        catch (DuplicateEdgeException)
                        {
                        }
                    }
                }

                components.Add(graph);
            }

            return components;
        }

        /// <summary>
        /// Return a new Graph
        /// </summary>
        /// <returns>New Graph</returns>
        protected abstract Graph<E, V> NewGraph();

        /// <summary>
        /// Find first vertex named fname in graph
        /// </summary>
        /// <param name="vName">Name of vertex to find</param>
        /// <returns>First vertex named fname, null if no vertex is named fname</returns>
        public Vertex<V> FindFirst(string vName)
        {
            return vertices.Values.FirstOrDefault(vertex => vertex.Name == vName);
        }

        /// <summary>
        /// Checks for value equality
        /// </summary>
        /// <param name="obj">Object to check against</param>
        /// <returns>true if obj is of same type and value equal</returns>
        public abstract override bool Equals(object obj);

        /// <summary>
        /// Checks for value equality
        /// </summary>
        public bool Equals(Graph<E, V> graph)
        {
            List<Vertex<V>> myVertices = GetVertices();
            List<Vertex<V>> otherVertices = graph.GetVertices();

            if (myVertices.Count != otherVertices.Count)
            {
                return false;
            }

            for (int i = 0; i < myVertices.Count; ++i)
            {
                List<Edge<E, V>> myEdges = GetConnectedEdges(myVertices[i]);
                List<Edge<E, V>> otherEdges = graph.GetConnectedEdges(otherVertices[i]);

                if (myEdges.Count != otherEdges.Count)
                {
                    return false;
                }

                if (myEdges.Where((t, j) => t.Weight != otherEdges[j].Weight
                                            || myVertices.FindIndex(vertex => vertex == t.BaseVertex) != otherVertices.FindIndex(vertex => vertex == otherEdges[j].BaseVertex)
                                            || myVertices.FindIndex(vertex => vertex == t.TargetVertex) != otherVertices.FindIndex(vertex => vertex == otherEdges[j].TargetVertex)).Any())
                {
                    return false;
                }
            }
            return true;
        }


        public override string ToString()
        {
            return Name;
        }


        /// <summary>
        /// Gets undecorated part of the Graphvizstring.
        /// </summary>
        public abstract string GetInitialString();

        
        public void Clear()
        {
            edges.Clear();
            vertices.Clear();
        }
    }
}
 
