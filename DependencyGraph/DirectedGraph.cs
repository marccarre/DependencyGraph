using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyGraph
{
    public class DirectedGraph<T>
    {
        private IDictionary<T, Edges> graph = new Dictionary<T, Edges>();

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
            public ISet<T> inBound = new HashSet<T>();
            public ISet<T> outBound = new HashSet<T>();
        }

        public int Count()
        {
            return graph.Count();
        }
    }
}
