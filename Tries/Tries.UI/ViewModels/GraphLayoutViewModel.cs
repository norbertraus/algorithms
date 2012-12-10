using System.ComponentModel;
using GraphSharp.Controls;
using Tries.UI.Model;

namespace Tries.UI.ViewModels
{
    public class TrieGraphLayout : GraphLayout<Vertex, Edge, Graph> { }

    public class GraphLayoutViewModel : INotifyPropertyChanged
    {
        private string layoutAlgorithmType;
        private Graph graph;

        public string LayoutAlgorithmType
        {
            get { return layoutAlgorithmType; }
            set
            {
                if (value != layoutAlgorithmType)
                {
                    layoutAlgorithmType = value;
                    NotifyChanged("LayoutAlgorithmType");
                }
            }
        }

        public Graph Graph
        {
            get { return graph; }
            set
            {
                if (value != graph)
                {
                    graph = value;
                    NotifyChanged("Graph");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}