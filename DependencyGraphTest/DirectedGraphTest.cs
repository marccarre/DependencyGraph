using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyGraphTest
{
    using DependencyGraph;
    using NUnit.Framework;

    [TestFixture]
    public class DirectedGraphTest
    {
        private DirectedGraph<string> graph = new DirectedGraph<string>();

        [Test]
        public void AddVerticesEdgesAndBrowseGraph()
        {
            graph.AddEdge("a", "b");
            graph.AddEdge("a", "B");
            graph.AddEdge("b", "c");
            DirectedGraph<string>.Edges edges = graph.EdgesFor("b");
            Assert.IsNotNull(edges);
            Assert.IsNotNull(edges.inBound);
            Assert.IsNotNull(edges.outBound);
            Assert.AreEqual(1, edges.inBound.Count());
            Assert.AreEqual("a", edges.inBound.First());
            Assert.AreEqual(1, edges.outBound.Count());
            Assert.AreEqual("c", edges.outBound.First());
        }
    }
}
