//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tastenhacker.Pathfinding.Core
{
    public class EdgeEnumerator<E,V> : IEnumerator
    {
        private List<Edge<E,V>> edges;
        private int position = -1;

        public EdgeEnumerator(List<Edge<E,V>> edges)
        {
            this.edges = edges;
        }

        public object Current
        {
            get
            {
                try
                {
                    return edges[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            return (++position < edges.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
        }
    }
}
