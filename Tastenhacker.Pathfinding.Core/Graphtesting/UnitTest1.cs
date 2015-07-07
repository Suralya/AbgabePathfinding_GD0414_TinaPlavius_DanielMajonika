using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tastenhacker.Pathfinding.Core;

namespace Graphtesting
{
    [TestClass]
    public class UnitTest1
    {

        private static UndirectedGraph<string, string> _graph;
        private static Vertex<string> _vertex1, _vertex2, _vertex3, _vertex4;
        private static UndirectedEdge<string, string> _edge1, _edge2, _edge3, _edge4;

        public void Initialize()
        {
            _graph =  new UndirectedGraph<string, string>();
            _vertex1 = _graph.CreateVertex("Penis");
            _vertex2 = _graph.CreateVertex("Tomato");
            _vertex3 = _graph.CreateVertex("Isa");
            _vertex4 = _graph.CreateVertex("Robin");

            _edge1 = (UndirectedEdge<string, string>)_graph.AddEdge(_vertex1, _vertex2);
            _edge2 = (UndirectedEdge<string, string>)_graph.AddEdge(_vertex2, _vertex3);
            _edge3 = (UndirectedEdge<string, string>)_graph.AddEdge(_vertex3, _vertex4);
            _edge4 = (UndirectedEdge<string, string>)_graph.AddEdge(_vertex1, _vertex4);
        }


        [TestMethod]
        public void VertexCreation()
        {
            Initialize();
            Assert.IsTrue(_graph.VertexExists(_vertex1));
            Assert.IsTrue(_graph.VertexExists(_vertex2));
            Assert.IsTrue(_graph.VertexExists(_vertex3));
            Assert.IsTrue(_graph.VertexExists(_vertex4));
        }

        [TestMethod]
        public void EdgeCreation()
        {
            Initialize();
            Assert.IsTrue(_graph.EdgeExists(_edge1));
            Assert.IsTrue(_graph.EdgeExists(_edge2));
            Assert.IsTrue(_graph.EdgeExists(_edge3));
            Assert.IsTrue(_graph.EdgeExists(_edge4));
        }


        [TestMethod]
        public void FindingInGraph()
        {
            Initialize();
            //Graph.FindEdge
            Assert.AreEqual(_edge1, _graph.FindEdge(_vertex1, _vertex2));
            Assert.AreEqual(_edge2, _graph.FindEdge(_vertex2, _vertex3));
            Assert.AreEqual(_edge3, _graph.FindEdge(_vertex3, _vertex4));
            Assert.AreEqual(_edge4, _graph.FindEdge(_vertex1, _vertex4));

            List<Vertex<string>> expectedVertices = new List<Vertex<string>> { _vertex1, _vertex3 };
            List<Vertex<string>> givenVertices = _graph.GetNeighbourVertices(_vertex2);

            foreach (Vertex<string> vertex in givenVertices)
            {
                expectedVertices.Remove(vertex);
            }

            //Graph.GetNeighbourVertices
            Assert.AreEqual(0, expectedVertices.Count);

            List<Edge<string, string>> expectedEdges1 = new List<Edge<string, string>> { _edge1, _edge4 };
            List<Edge<string, string>> expectedEdges2 = new List<Edge<string, string>> { _edge2, _edge3 };
            List<Edge<string, string>> givenEdges1 = _graph.GetConnectedEdges(_vertex1);
            List<Edge<string, string>> givenEdges2 = _graph.GetConnectedEdges(_vertex3);

            foreach (Edge<string, string> edge in givenEdges1)
            {
                expectedEdges1.Remove(edge);
            }

            foreach (Edge<string, string> edge in givenEdges2)
            {
                expectedEdges2.Remove(edge);
            }

            //Graph.GetConnectedEdges
            Assert.AreEqual(0, expectedEdges1.Count);
            Assert.AreEqual(0, expectedEdges2.Count);
        }

        [TestMethod]
        public void IsCyclic()
        {
            Initialize();
            Assert.IsTrue(_graph.IsAcyclic());
        }

        [TestMethod]
        public void IsAdjacency()
        {
            Initialize();
            bool[][] expectedBools = new bool[4][];
            bool[][] givenBools = _graph.Adjacency_UnDir();

            expectedBools[0] = new bool[1]; expectedBools[1] = new bool[2]; expectedBools[2] = new bool[3]; expectedBools[3] = new bool[4];

            expectedBools[0][0] = false;
            expectedBools[1][0] = true;     expectedBools[1][1] = false;
            expectedBools[2][0] = false;    expectedBools[2][1] = true;     expectedBools[2][2] = false;
            expectedBools[3][0] = true;     expectedBools[3][1] = false;    expectedBools[3][2] = true;     expectedBools[3][3] = false;

            for (int i = 0; i < expectedBools.Length - 1; i++)
            {
                for (int j = 0; j < expectedBools[i].Length - 1; j++)
                {
                    Assert.AreEqual(expectedBools[i][j], givenBools[i][j]);
                }
            }
        }

        [TestMethod]
        public void AStarTest()
        {
            Initialize();
            //TODO Testing AStar

        }



        [TestMethod]
        public void Breadthfirstsearch()
        {
            Initialize();
            List<Vertex<string> > bread;
            Assert.IsTrue(_graph.BreadthFirstSearch(_vertex1, _vertex3,out bread));
        }

        [TestMethod]
        public void Depthfirstsearch()
        {
            Initialize();
            List<Vertex<string>> bread;
            Assert.IsTrue(_graph.DepthFirstSearch(_vertex1, _vertex3, out bread));
        }


        [TestMethod]
        public void RemovingAndClearing()
        {
            Initialize();
            _graph.RemoveEdge(_edge1);
            Assert.IsFalse(_graph.EdgeExists(_edge1));
            _graph.RemoveVertex(_vertex1);
            Assert.IsFalse(_graph.VertexExists(_vertex1));
            _graph.Clear();
            Assert.AreEqual(_graph, new UndirectedGraph<string, string>());
        }
    }
}
