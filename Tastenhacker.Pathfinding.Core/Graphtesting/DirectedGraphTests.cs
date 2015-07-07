﻿using System.Collections.Generic;
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
        public void VertexCreation()
        {
            Initialize();
            Assert.IsTrue(_graph.VertexExists(_vertex1));
            Assert.IsTrue(_graph.VertexExists(_vertex2));
            Assert.IsTrue(_graph.VertexExists(_vertex3));
            Assert.IsTrue(_graph.VertexExists(_vertex4));
        }
    }
}
