using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tastenhacker.Pathfinding.Core;

namespace Graphtesting
{
    [TestClass]
    public class UnitTest1
    {

        private static UndirectedGraph<string, string> graph = new UndirectedGraph<string, string>();

        private static Vertex<string> Vertex1 = graph.CreateVertex("Penis");
        private static Vertex<string> Vertex2 = graph.CreateVertex("Tomato");
        private static Vertex<string> Vertex3 = graph.CreateVertex("Isa");
        private static Vertex<string> Vertex4 = graph.CreateVertex("Robin");

        static UndirectedEdge<string, string> Edge1 = (UndirectedEdge<string, string>) graph.AddEdge(Vertex1, Vertex2);
        static UndirectedEdge<string, string> Edge2 = (UndirectedEdge<string, string>) graph.AddEdge(Vertex2, Vertex3);
        static UndirectedEdge<string, string> Edge3 = (UndirectedEdge<string, string>) graph.AddEdge(Vertex3, Vertex4);
        static UndirectedEdge<string, string> Edge4 = (UndirectedEdge<string, string>) graph.AddEdge(Vertex1, Vertex4);




        [TestMethod]
        public void VertexCreation()
        {
            Assert.AreEqual(graph.VertexExists(Vertex1), true);
            Assert.AreEqual(graph.VertexExists(Vertex2), true);
            Assert.AreEqual(graph.VertexExists(Vertex3), true);
            Assert.AreEqual(graph.VertexExists(Vertex4), true);
        }

        [TestMethod]
        public void EdgeCreation()
        {
            Assert.IsTrue(graph.EdgeExists(Edge1));
            Assert.IsTrue(graph.EdgeExists(Edge2));
            Assert.IsTrue(graph.EdgeExists(Edge3));
            Assert.IsTrue(graph.EdgeExists(Edge4));
        }

        [TestMethod]
        public void IsCyclic()
        {
            Assert.IsTrue(graph.IsAcyclic());
        }

        [TestMethod]
        public void IsAdjacency()
        {
            bool[][] expectedBools = new bool[4][];
            bool[][] givenBools = graph.Adjacency_UnDir();

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
            //TODO Testing AStar

        }

        [TestMethod]
        public void Breadthfirstsearch()
        {
            List<Vertex<string> > bread = new List<Vertex<string>>();
            Assert.IsTrue(graph.BreadthFirstSearch(Vertex1, Vertex3,out bread));

        }

        [TestMethod]
        public void RemovingAndClearing()
        {
            graph.RemoveEdge(Edge1);
            Assert.IsFalse(graph.EdgeExists(Edge1));
            graph.RemoveVertex(Vertex1);
            Assert.IsFalse(graph.VertexExists(Vertex1));
            graph.Clear();
            Assert.AreEqual(graph,new UndirectedGraph<string, string>());
        }

        [TestMethod]
        public void FindingInGraph()
        {
            //Graph.FindEdge
            Assert.AreEqual(Edge1, graph.FindEdge(Vertex1, Vertex2));
            Assert.AreEqual(Edge2, graph.FindEdge(Vertex2, Vertex3));
            Assert.AreEqual(Edge3, graph.FindEdge(Vertex3, Vertex4));
            Assert.AreEqual(Edge4, graph.FindEdge(Vertex1, Vertex4));

            List<Vertex<string>> expectedVertices = new List<Vertex<string>> {Vertex1, Vertex3};
            List<Vertex<string>> givenVertices = graph.GetNeighbourVertices(Vertex2);

            foreach (Vertex<string> vertex in givenVertices)
            {
                expectedVertices.Remove(vertex);
            }

            //Graph.GetNeighbourVertices
            Assert.AreEqual(0, expectedVertices.Count);

            List<Edge<string, string>> expectedEdges1 = new List<Edge<string, string>> { Edge1, Edge4 };
            List<Edge<string, string>> expectedEdges2 = new List<Edge<string, string>> { Edge2, Edge3 };
            List<Edge<string, string>> givenEdges1 = graph.GetConnectedEdges(Vertex1);
            List<Edge<string, string>> givenEdges2 = graph.GetConnectedEdges(Vertex3);

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
    }
}
