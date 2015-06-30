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
    public class EdgeEnumerator<E,V> : IEnumerator, IDisposable
    {
        private List<Edge<E,V>> edges;
        private int _position = -1;

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
                    return edges[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            return (++_position < edges.Count);
        }

        public void Reset()
        {
            _position = -1;
        }

        public void Dispose()
        {
        }
    }
}
