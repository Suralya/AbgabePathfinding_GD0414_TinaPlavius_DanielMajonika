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
        private readonly List<Edge<E,V>> _edges;
        private int _position = -1;

        public EdgeEnumerator(List<Edge<E,V>> edges)
        {
            _edges = edges;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _edges[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            _position++;
            return (_position < _edges.Count);
        }

        public void Reset()
        {
            _position = -1;
        }

    }
}
