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
        private readonly List<Vertex<V>> vertices;
        private int position = -1;

        public VertexEnumerator(List<Vertex<V>> vertices)
        {
            this.vertices = vertices;
        }

        public object Current
        {
            get
            {
                try
                {
                    return vertices[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            return (++position < vertices.Count);
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
