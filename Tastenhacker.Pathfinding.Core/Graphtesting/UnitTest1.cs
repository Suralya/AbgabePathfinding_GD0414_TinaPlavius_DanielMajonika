using System;
using System.Collections.Generic;
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
            bool[][] helperBools = graph.Adjacency_UnDir();
        }
    }
}
