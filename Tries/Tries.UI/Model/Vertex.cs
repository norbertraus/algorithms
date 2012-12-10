using System.Diagnostics;

namespace Tries.UI.Model
{
    [DebuggerDisplay("{Id}")]
    public class Vertex
    {
        public Vertex(string id)
        {
            Id = id;
        }
        public string Id { get; private set; }

        public override string ToString()
        {
            return Id;
        }
    }

    public class GraphModel
    {
        public string Name { get; private set; }
        public Graph Graph { get; private set; }

        public GraphModel(string name, Graph graph)
        {
            Name = name;
            Graph = graph;
        }
    }
}