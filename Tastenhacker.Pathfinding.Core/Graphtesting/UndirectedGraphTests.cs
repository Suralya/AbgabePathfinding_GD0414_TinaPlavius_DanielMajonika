using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tastenhacker.Pathfinding.Core;

namespace Graphtesting
{
    [TestClass]
    public class UndirectedGraphTests
    {

        private static UndirectedGraph<string, string> _graph;
        private static Vertex<string> _vertex1, _vertex2, _vertex3, _vertex4, _vertex5, _vertex6, _vertex7, _vertex8, _vertex9;
        private static Edge<string, string> _edge1, _edge2, _edge3, _edge4, _edge5, _edge6, _edge7, _edge8, _edge9, _edge10, _edge11, _edge12, _edge13, _edge14, _edge15, _edge16;

        public void Initialize()
        {
            _graph =  new UndirectedGraph<string, string>();
            _vertex1 = _graph.CreateVertex("Penis");
            _vertex2 = _graph.CreateVertex("Tomato");
            _vertex3 = _graph.CreateVertex("Isa");
            _vertex4 = _graph.CreateVertex("Robin");
            _vertex5 = _graph.CreateVertex("Außenseiter");

            _edge1 = _graph.AddEdge(_vertex1, _vertex2);
            _edge2 = _graph.AddEdge(_vertex2, _vertex3);
            _edge3 = _graph.AddEdge(_vertex3, _vertex4);
            _edge4 = _graph.AddEdge(_vertex1, _vertex4);
        }

        public void InitializeNullGraph()
        {
            _graph = new UndirectedGraph<string, string>();
        }

        public void InitializeTwoArmedGraph()
        {
            _graph = new UndirectedGraph<string, string>();
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
            _graph = new UndirectedGraph<string, string>();
            _vertex1 = _graph.CreateVertex("MrLonely");

        }

        public void InitializeStarGraph()
        {
            _graph = new UndirectedGraph<string, string>();
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
            _graph = new UndirectedGraph<string, string>();

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
        public void AStarTestPathExists()
        {
            InitializePathfindingGraph();
            Path<Vertex<string>> expectedPath = new Path<Vertex<string>> { _vertex1, _vertex5, _vertex9, _vertex7 };
            Path<Vertex<string>> givenPath = _graph.AStar(_vertex1, _vertex7, 40);

            foreach (Vertex<string> vertex in givenPath)
            {
                expectedPath.Remove(vertex);
            }
            Assert.AreEqual(0, expectedPath.Count);
        }
        [TestMethod]
        public void AStarTestPathisCorrect()
        {
            InitializePathfindingGraph();
            Path<Vertex<string>> expectedPath = new Path<Vertex<string>> { _vertex1, _vertex5, _vertex9, _vertex7 };
            Path<Vertex<string>> givenPath = _graph.AStar(_vertex1, _vertex7, 40);

            for (int i = 0; i < expectedPath.Count - 1; i++)
            {
                Assert.AreEqual(expectedPath[i], givenPath[i]);
            }
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
            Assert.AreEqual(_graph, new UndirectedGraph<string, string>());
        }
        [TestMethod]
        public void FindNotFound()
        {
            InitializeNullGraph();
            List<Edge<string, string>> givenEdges = _graph.GetEdges();

            Assert.AreEqual(0, givenEdges.Count);

            List<Vertex<string>> givenVertices = _graph.GetVertices();

            Assert.AreEqual(0, givenVertices.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(EdgeNotFoundException))]
        public void EdgeRemovingException()
        {
            InitializeNullGraph();
            _graph.RemoveEdge(_edge1);
        }

        [TestMethod]
        [ExpectedException(typeof(VertexNotFoundException))]
        public void VertexRemovingException()
        {
            InitializeNullGraph();
            _graph.RemoveVertex(_vertex1);
        }
        [TestMethod]
        [ExpectedException(typeof(VertexNotFoundException))]
        public void EdgeCreatingException()
        {
            Initialize();
            _graph.AddEdge(_vertex4, _vertex8);
        }
        [TestMethod]
        [ExpectedException(typeof(DuplicateEdgeException))]
        public void AddingDuplicateEdgeException()
        {
            Initialize();
            _edge1 = _graph.AddEdge(_vertex1, _vertex2);
            _edge2 = _graph.AddEdge(_vertex1, _vertex2);
        }
    }
}
