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
    public class VertexEnumerator<V> : IEnumerator
    {
        private readonly List<Vertex<V>> _vertices;
        private int _position = -1;

        public VertexEnumerator(List<Vertex<V>> vertices)
        {
            _vertices = vertices;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _vertices[_position];
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
            return (_position < _vertices.Count);
        }

        public void Reset()
        {
            _position = -1;
        }

    }
}
