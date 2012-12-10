using System.Diagnostics;
using QuickGraph;

namespace Tries.UI.Model
{
    /// <summary>
    /// A simple identifiable edge.
    /// </summary>
    [DebuggerDisplay("{Source.Id} -> {Target.Id}")]
    public class Edge : Edge<Vertex>
    {
        public Edge(string id, Vertex source, Vertex target)
            : base(source, target)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}