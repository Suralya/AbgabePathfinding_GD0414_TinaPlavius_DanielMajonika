using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tastenhacker.Pathfinding.Core;

namespace Graphtesting
{
    [TestClass]
    public class DirectedGraphTests
    {

        private static DirectedGraph<string, string> _graph;
        private static Vertex<string> _vertex1, _vertex2, _vertex3, _vertex4, _vertex5, _vertex6, _vertex7, _vertex8, _vertex9;
        private static Edge<string, string> _edge1, _edge2, _edge3, _edge4, _edge5, _edge6, _edge7, _edge8, _edge9, _edge10, _edge11, _edge12, _edge13, _edge14, _edge15, _edge16;

        public void Initialize()
        {
            _graph = new DirectedGraph<string, string>();
            _vertex1 = _graph.CreateVertex("A");
            _vertex2 = _graph.CreateVertex("B");
            _vertex3 = _graph.CreateVertex("C");
            _vertex4 = _graph.CreateVertex("D");
            _vertex5 = _graph.CreateVertex("E");

            _edge1 = _graph.AddEdge(_vertex1, _vertex2);
            _edge2 = _graph.AddEdge(_vertex2, _vertex3);
            _edge3 = _graph.AddEdge(_vertex3, _vertex4);
            _edge4 = _graph.AddEdge(_vertex1, _vertex4);
        }

        public void InitializeTwoArmedGraph()
        {
            _graph = new DirectedGraph<string, string>();
            _vertex1 = _graph.CreateVertex("A");
            _vertex2 = _graph.CreateVertex("B");
            _vertex3 = _graph.CreateVertex("C");
            _vertex4 = _graph.CreateVertex("D");
            _vertex5 = _graph.CreateVertex("E");
            _vertex5 = _graph.CreateVertex("F");

            _edge1 = _graph.AddEdge(_vertex1, _vertex2);
            _edge2 = _graph.AddEdge(_vertex1, _vertex3);
            _edge3 = _graph.AddEdge(_vertex2, _vertex3);
            _edge4 = _graph.AddEdge(_vertex3, _vertex4);
            _edge5 = _graph.AddEdge(_vertex4, _vertex5);
            _edge6 = _graph.AddEdge(_vertex4, _vertex6);
            _edge7 = _graph.AddEdge(_vertex5, _vertex6);
        }

        public void InitializeLoneVertexGraph()
        {
            _graph = new DirectedGraph<string, string>();
            _vertex1 = _graph.CreateVertex("A");

        }

        public void InitializeStarGraph()
        {
            _graph = new DirectedGraph<string, string>();
            _vertex1 = _graph.CreateVertex("A");
            _vertex2 = _graph.CreateVertex("B");
            _vertex3 = _graph.CreateVertex("C");
            _vertex4 = _graph.CreateVertex("D");
            _vertex5 = _graph.CreateVertex("E");

            _edge1 = _graph.AddEdge(_vertex1, _vertex2);
            _edge2 = _graph.AddEdge(_vertex1, _vertex3);
            _edge3 = _graph.AddEdge(_vertex1, _vertex4);
            _edge4 = _graph.AddEdge(_vertex1, _vertex5);

        }

        public void InitializePathfindingGraph()
        {
            _graph = new DirectedGraph<string, string>();

            _vertex1 = _graph.CreateVertex("A");
            _vertex2 = _graph.CreateVertex("B");
            _vertex3 = _graph.CreateVertex("C");
            _vertex4 = _graph.CreateVertex("D");
            _vertex5 = _graph.CreateVertex("E");
            _vertex6 = _graph.CreateVertex("F");
            _vertex7 = _graph.CreateVertex("G");
            _vertex8 = _graph.CreateVertex("H");
            _vertex9 = _graph.CreateVertex("K");

            _edge1 = _graph.AddEdge(_vertex1, _vertex2, 10, "AB");
            _edge2 = _graph.AddEdge(_vertex2, _vertex3, 10, "BC");
            _edge3 = _graph.AddEdge(_vertex1, _vertex4, 10, "AD");
            _edge4 = _graph.AddEdge(_vertex1, _vertex5, 15, "AE");
            _edge5 = _graph.AddEdge(_vertex4, _vertex5, 10, "DE");
            _edge6 = _graph.AddEdge(_vertex4, _vertex8, 15, "DH");
            _edge7 = _graph.AddEdge(_vertex2, _vertex5, 10, "BE");
            _edge8 = _graph.AddEdge(_vertex5, _vertex6, 10, "EF");
            _edge9 = _graph.AddEdge(_vertex5, _vertex8, 10, "EH");
            _edge10 = _graph.AddEdge(_vertex5, _vertex3, 15, "EC");
            _edge11 = _graph.AddEdge(_vertex5, _vertex9, 15, "EK");
            _edge12 = _graph.AddEdge(_vertex3, _vertex6, 10, "CF");
            _edge13 = _graph.AddEdge(_vertex3, _vertex7, 50, "CG");
            _edge14 = _graph.AddEdge(_vertex8, _vertex9, 10, "HK");
            _edge15 = _graph.AddEdge(_vertex9, _vertex6, 10, "KF");
            _edge16 = _graph.AddEdge(_vertex9, _vertex7, 20, "KG");
        }

        [TestMethod]
        public void FindingInGraph()
        {
            Initialize();
            DirectedEdge<string, string> expectededge;
            Assert.IsTrue(_graph.EdgeExists(_vertex1, _vertex2, out expectededge));
            Assert.AreEqual(_vertex1, expectededge.BaseVertex);
            Assert.AreEqual(_vertex2, expectededge.TargetVertex);
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
        public void IsCyclic()
        {
            Initialize();
            Assert.IsTrue(_graph.IsAcyclic());
        }

        [TestMethod]
        public void IsAdjacency()
        {
            Initialize();
            bool[][] expectedBools = new bool[5][];
            bool[][] givenBools = _graph.Adjacency_Dir();

            expectedBools[0] = new bool[5]; expectedBools[1] = new bool[5]; expectedBools[2] = new bool[5]; expectedBools[3] = new bool[5]; expectedBools[4] = new bool[5];

            expectedBools[0][0] = false; expectedBools[0][1] = true; expectedBools[0][2] = false; expectedBools[0][3] = true; expectedBools[0][4] = false;
            expectedBools[1][0] = false; expectedBools[1][1] = false; expectedBools[1][2] = true; expectedBools[1][3] = false; expectedBools[1][4] = false;
            expectedBools[2][0] = false; expectedBools[2][1] = false; expectedBools[2][2] = false; expectedBools[2][3] = true; expectedBools[2][4] = false;
            expectedBools[3][0] = false; expectedBools[3][1] = false; expectedBools[3][2] = false; expectedBools[3][3] = false; expectedBools[3][4] = false;
            expectedBools[4][0] = false; expectedBools[4][1] = false; expectedBools[4][2] = false; expectedBools[4][3] = false; expectedBools[4][4] = false;

            for (int i = 0; i < expectedBools.Length - 1; i++)
            {
                for (int j = 0; j < expectedBools[i].Length - 1; j++)
                {
                    Assert.AreEqual(expectedBools[i][j], givenBools[i][j]);
                }
            }
        }

        [TestMethod]
        public void Breadthfirstsearch()
        {
            Initialize();
            List<Vertex<string>> bread;
            Assert.IsTrue(_graph.BreadthFirstSearch(_vertex1, _vertex3, out bread));
        }

        [TestMethod]
        public void Depthfirstsearch()
        {
            Initialize();
            bool tmp = _graph.DepthFirstSearch(_vertex1, _vertex3);
            bool tmp2 = _graph.DepthFirstSearch(_vertex1, _vertex5);
            Assert.IsTrue(tmp);

            Assert.IsFalse(tmp2);
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
            Assert.AreEqual(_graph, new DirectedGraph<string, string>());
        }
    }
}
