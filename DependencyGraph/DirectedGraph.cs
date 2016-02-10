using System.Collections.Generic;
using System.Linq;
using System.IO;
using YamlDotNet.Serialization;

namespace DependencyGraph
{
    public class DirectedGraph<T>
    {
        private readonly IDictionary<T, Edges> graph = new Dictionary<T, Edges>();

        public Edges AddVertex(T vertex)
        {
            Edges adjacencySets;
            if (!graph.TryGetValue(vertex, out adjacencySets))
            {
                adjacencySets = new Edges();
                graph[vertex] = adjacencySets;
            }
            return adjacencySets;
        }

        public void AddEdge(T from, T to)
        {
            AddVertex(from).outBound.Add(to);
            AddVertex(to).inBound.Add(from);
        }

        public Edges EdgesFor(T vertex)
        {
            return graph[vertex];
        }

        public class Edges
        {
            [YamlMember(Alias = "isUsedBy")]
            public ISet<T> inBound { get; }

            [YamlMember(Alias = "dependsOn")]
            public ISet<T> outBound { get; }
            
            public Edges()
            {
                inBound = new HashSet<T>();
                outBound = new HashSet<T>();
            }
        }

        public int Count()
        {
            return graph.Count();
        }

        public void ExportAsYaml(TextWriter outputWriter)
        {
            var serializer = new Serializer();
            serializer.Serialize(outputWriter, graph);
        }
    }
}
