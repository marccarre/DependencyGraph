using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace DependencyGraphTest
{
    using DependencyGraph;
    using NUnit.Framework;

    [TestFixture]
    public class DirectedGraphTest
    {
        private DirectedGraph<string> graph = new DirectedGraph<string>();

        [SetUp]
        public void SetUp()
        {
            graph.AddEdge("B1", "A");
            graph.AddEdge("B2", "A");
            graph.AddEdge("C", "B1");
            graph.AddEdge("C", "B2");
            graph.AddEdge("D", "C");
        }

        [Test]
        public void AddVerticesEdgesAndBrowseGraph()
        {
            DirectedGraph<string>.Edges edges = graph.EdgesFor("B1");
            Assert.IsNotNull(edges);
            Assert.IsNotNull(edges.inBound);
            Assert.IsNotNull(edges.outBound);
            Assert.AreEqual(1, edges.inBound.Count());
            Assert.AreEqual("C", edges.inBound.First());
            Assert.AreEqual(1, edges.outBound.Count());
            Assert.AreEqual("A", edges.outBound.First());
        }

        [Test]
        public void DependeesFor()
        {
            var expectedDependees = new List<string> { "B1", "B2", "C", "D" };
            Assert.AreEqual(expectedDependees, graph.DependeesFor("A"));
        }

        [Test]
        public void DependeesForWithMaxDistance()
        {
            var expectedDependees = new List<string> { "B1", "B2", "C" };
            Assert.AreEqual(expectedDependees, graph.DependeesFor("A", 2));
        }

        [Test]
        public void ExportAsYaml()
        {
            using (var stringWriter = new StringWriter())
            {
                graph.ExportAsYaml(stringWriter);
                string actualYaml = stringWriter.ToString();
                System.Console.Out.WriteLine(actualYaml);
                Assert.AreEqual(@"B1:
  isUsedBy:
  - C
  dependsOn:
  - A
A:
  isUsedBy:
  - B1
  - B2
  dependsOn: []
B2:
  isUsedBy:
  - C
  dependsOn:
  - A
C:
  isUsedBy:
  - D
  dependsOn:
  - B1
  - B2
D:
  isUsedBy: []
  dependsOn:
  - C
", actualYaml);
            }
        }
    }
}
