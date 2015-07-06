//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Tastenhacker.Pathfinding.Core
{

    /// <summary>
    /// Represenst the heuristic the user of the graph class has to prevent, heuristic helps the AStar to be faster than the Dijkstra
    /// </summary>
    public delegate int HeuristicDelegate<E, V>(Vertex<V> start, Vertex<V> finish, Graph<E, V> graph);

    public delegate bool FilterEdges<E, V>(Edge<E, V> edge);

    public delegate bool FilterVertex<V>(Vertex<V> edge);

    public class UndirectedGraph<E, V> : Graph<E, V>
    {
        /// <summary>
        /// Creates a new undirected graph
        /// </summary>
        /// <param name="name">Name associated with the graph (generated automatically if null)</param>
        public UndirectedGraph(string name = null) : base(name)
        {
        }

        /// <summary>
        /// Returns if this Edge exits (Edge is described by two vertices), complexity: O (|E|)
        /// </summary>
        /// <param name="a">first vertex of the edge to test</param>
        /// <param name="b">second vertex of the edge to test</param>
        public override bool EdgeExists(Vertex<V> a, Vertex<V> b)
        {
            return EdgeExists(new UndirectedEdge<E, V>(a, b));
        }

        /// <summary>
        /// Add new edge to undirected graph
        /// </summary>
        /// <param name="v1">First vertex</param>
        /// <param name="v2">Second vertex</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="name">Edge name</param>
        /// <returns>ID of added edge</returns>
        public override Edge<E, V> AddEdge(Vertex<V> v1, Vertex<V> v2, int weight = 0, string name = null)
        {
            if (EdgeExists(v1, v2))
            {
                throw new DuplicateEdgeException();
            }

            Edge<E, V> edge = new UndirectedEdge<E, V>(v1, v2, weight, name);

            return AddEdge(edge);
        }

        /// <summary>
        /// Creates the shortest path from start-vertex to finish vertex, uses a heuristic to be faster than the Dijkstra 
        /// </summary>
        /// <param name="start">Represents the start-vertex</param>
        /// <param name="finish">Represents the finish-vertex</param>
        /// <param name="maxWeight">Represents the max-edge-weight that can be passed by the algorithm</param>
        /// <param name="heuristicDelegate">Represents a heuristic to helpt the AStar to be faster</param>
        /// <returns>Returns the shortest path if available, returns NULL if no path exists</returns>
        public Path<Vertex<V>> AStar(Vertex<V> start, Vertex<V> finish, int maxWeight, FilterEdges<E, V> edgeFilter, FilterVertex<V> vertexFilter, HeuristicDelegate<E,V> heuristicDelegate)
        {
            AStarVertexList<V> openList = new AStarVertexList<V>();
            AStarVertexList<V> closedList = new AStarVertexList<V>();

            //add start point to openList
            openList.Add(AStarVertex<V>.Create(start));

            //runs till open list is empty (we found finish-vertex)
            while (!closedList.ContainsVertex(finish) || openList.Count != 0)
            {
                if (openList.Count == 0)
                {
                    throw new PathNotFoundException();
                }

                //1. cheapest element of openList
                AStarVertex<V> self = openList.FindCheapest();

                //2. delete self from open list, add to closed list
                openList.Remove(self);
                closedList.Add(self);

                //3. find sourrounding vertices
                List<Edge<E, V>> sourroundingEdges = GetConnectedEdges(self.vertex);

                foreach (Edge<E, V> edge in sourroundingEdges.Where(e => !edgeFilter(e)))
                {
                    //find the other vertex (NOT ME!)
                    Vertex<V> notMe = (edge.BaseVertex.Equals(self.vertex)) ? edge.TargetVertex : edge.BaseVertex;

                    //check if this edge is cheap enough
                    if (edge.Weight <= maxWeight && !openList.ContainsVertex(notMe) && !closedList.ContainsVertex(notMe) && !vertexFilter(notMe))
                    {
                        AStarVertex<V> neigbour = AStarVertex<V>.Create(notMe);
                        neigbour.Origin = self; //self.vertex
                        neigbour.G = self.G + edge.Weight;
                        neigbour.H = heuristicDelegate(self.vertex, finish, this);
                        neigbour.vertex.PathCost = edge.Weight;
                        openList.Add(neigbour);
                    }
                    else
                    {
                        if (openList.ContainsVertex(notMe))
                        {
                            AStarVertex<V> a = openList.FindElement(notMe);

                            if (a != null)
                            {
                                if (a.F > self.F + edge.Weight) // a.G > self.G + edge.Weight
                                {
                                    a.vertex.PathCost = edge.Weight;
                                    a.Origin = self; //self.vertex
                                }
                            }
                        }
                    }
                }
            }

            //build path
            Path<Vertex<V>> path = new Path<Vertex<V>>();
            AStarVertex<V> pathFinish = closedList.FindElement(finish);

            while (pathFinish != null)
            {
                //add to path
                path.Add(pathFinish.vertex);
                pathFinish = pathFinish.Origin;
            }

            //TODO path!
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Calculates the shortest path from start-vertex to finish vertex
        /// </summary>
        /// <param name="start">Represents the start-vertex</param>
        /// <param name="finish">Represents the finish-vertex</param>
        /// <param name="maxWeight">Represents the max-edge-weight that can be passed by the algorithm</param>
        /// <returns>Returns the shortest path if available, returns NULL if no path exists</returns>
        public Path<Vertex<V>> Djkstra(Vertex<V> start, Vertex<V> finish, int maxWeight)
        {
            AStarVertexList<V> openList = new AStarVertexList<V>();
            AStarVertexList<V> closedList = new AStarVertexList<V>();

            //add start point to openList
            openList.Add(AStarVertex<V>.Create(start));

            //runs till open list is empty (we found finish-vertex)
            while(!closedList.ContainsVertex(finish) || openList.Count != 0)
            {
                if(openList.Count == 0)
                {
                    throw new PathNotFoundException();
                }

                //1. cheapest element of openList
                AStarVertex<V> self = openList.FindCheapest(); 
                
                //2. delete self from open list, add to closed list
                openList.Remove(self);
                closedList.Add(self);

                //3. find sourrounding vertices
                List<Edge<E,V>> sourroundingEdges = GetConnectedEdges(self.vertex);

                foreach (Edge<E,V> edge in sourroundingEdges)
                {
                    //find the other vertex (NOT ME!)
                    Vertex<V> notMe = (edge.BaseVertex.Equals(self.vertex)) ? edge.TargetVertex : edge.BaseVertex;

                    //check if this edge is cheap enough
                    if (edge.Weight <= maxWeight && !openList.ContainsVertex(notMe) && !closedList.ContainsVertex(notMe))
                    {
                        AStarVertex<V> neigbour = AStarVertex<V>.Create(notMe);
                        neigbour.Origin = self; //self.vertex
                        neigbour.G = self.G + edge.Weight;
                        neigbour.H = 0;

                        openList.Add(neigbour);
                    }
                    else 
                    {
                        if(openList.ContainsVertex(notMe))
                        {
                            AStarVertex<V> a = openList.FindElement(notMe);

                            if (a != null)
                            {
                                if (a.F > self.F + edge.Weight)
                                {
                                    a.Origin = self; //self.vertex
                                }
                            }
                        }
                    }
                }
            }

            //build path
            Path<Vertex<V>> path = new Path<Vertex<V>>();
            AStarVertex<V> pathFinish = closedList.FindElement(finish);

            while(pathFinish != null)
            {
                //add to path
                path.Add(pathFinish.vertex);
                pathFinish = pathFinish.Origin;
            }

            //TODO path!
            path.Reverse();
            return path;
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

            UndirectedGraph<E, V> graph = obj as UndirectedGraph<E, V>;

            return graph != null && graph.Equals(this);
        }

        public UndirectedGraph<E, V> Kruskal()
        {
            return new UndirectedGraph<E, V>();
        }

        public UndirectedGraph<E, V> Prim()
        {

            return new UndirectedGraph<E, V>();
        }

        /// <summary>
        /// Creates the default string for this Graph.
        /// </summary>
        /// <returns>Default string for this Graph</returns>
        public override string GetInitialString()
        {
            string mInitString = "graph G {" + Environment.NewLine;
            return mInitString;
        }

        /// <summary>
        /// Return a new UndirectedGraph
        /// </summary>
        /// <returns>New UndirectedGraph</returns>
        protected override Graph<E, V> NewGraph()
        {
            return new UndirectedGraph<E, V>();
        }

        /// <summary>
        /// Gets Adjacency of an Graph by checking connections of Vertices
        /// </summary>
        public bool[][] Adjacency_UnDir()
        {
            List<Vertex<V>> vertexList = GetVertices();

            int cv = vertexList.Count;

            bool[][] arr = new bool[cv][];

            for (int i = 0; i < cv; i++)
            {
               arr[i] = new bool[i + 1];
                for (int j = 0; j <= i; j++)
                {
                    arr[i][j] = false;
                }
            }
            foreach (Vertex<V> v in vertexList)
            {
                List<Vertex<V>> neighbours = GetNeighbourVertices(v);
                foreach (Vertex<V> nv in neighbours)
                {
                    int In = vertexList.IndexOf(v);
                    int Inv = vertexList.IndexOf(nv);
                    System.Diagnostics.Debug.WriteLine("Index of nv " + Inv + "\n" + "Index of n " + Inv);
                    if (In < Inv)
                    {
                        int exchange = In;
                        In = Inv;
                        Inv = exchange;
                    }
                    arr[In][Inv] = true;
                }
            }
            return arr;
        }

        public Edge<E,V> FindEdge(Vertex<V> v1, Vertex<V> neighbour)
        {
            List<Edge<E,V>> edges = GetConnectedEdges(v1);

            return edges.Single(e => e.BaseVertex.Equals(neighbour) || e.TargetVertex.Equals(neighbour));
        }
    }
}
