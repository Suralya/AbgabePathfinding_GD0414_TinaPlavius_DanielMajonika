﻿//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Tastenhacker.Pathfinding.Core
{
    public class Path<T>: List<T>
    {
    }

    public class AStarVertex<V>
    { 
        public int G, H;
        public Vertex<V> Vertex;
        public AStarVertex<V> Origin;
        public int F
        {
            get { return G + H; }
        } 

        public static AStarVertex<V> Create(Vertex<V> v)
        {
            AStarVertex<V> vertex = new AStarVertex<V> {Vertex = v, Origin = null, G = 0, H = 0};

            return vertex;
        }
    }

    public class AStarVertexList<V> : List<AStarVertex<V>>
    {
        public bool ContainsVertex(Vertex<V> v)
        {
            return this.Any(vertex => Equals(vertex.Vertex, v));
        }

        public AStarVertex<V> FindCheapest()
        {
            AStarVertex<V> cheapest = this[0];

            foreach (AStarVertex<V> vertex in this.Where(vertex => vertex.F < cheapest.F))
            {
                cheapest = vertex;
            }

            return cheapest;
        }

        public AStarVertex<V> FindElement(Vertex<V> vertex)
        {
            return this.FirstOrDefault(entry => Equals(entry.Vertex, vertex));
        }
    }
}
