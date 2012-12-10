using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Tries.UI.Annotations;
using Tries.UI.Model;

namespace Tries.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GraphModel selectedGraphModel;

        public ObservableCollection<GraphModel> GraphModels { get; private set; }
        public GraphModel SelectedGraphModel
        {
            get { return selectedGraphModel; }
            set
            {
                if(selectedGraphModel != value)
                {
                    selectedGraphModel = value;
                    SelectedGraphChanged();
                    OnPropertyChanged("SelectedGraphModel");
                }
            }
        }

        private void SelectedGraphChanged()
        {
            if(AnalyzedLayouts != null)
            {
                AnalyzedLayouts.Graph = selectedGraphModel.Graph;
            }
        }

        public GraphLayoutViewModel AnalyzedLayouts { get; private set; }

        public MainViewModel()
        {
            AnalyzedLayouts = new GraphLayoutViewModel
            {
                LayoutAlgorithmType = "FR"
            };
            GraphModels = new ObservableCollection<GraphModel>();

            CreateSampleGraphs();
        }

        void CreateSampleGraphs()
        {
            #region SimpleTree

            var graph = new Graph();

            for(int i = 0; i < 8; i++)
            {
                var v = new Vertex(i.ToString());
                graph.AddVertex(v);
            }

            graph.AddEdge(new Edge("0to1", graph.Vertices.ElementAt(0), graph.Vertices.ElementAt(1)));
            graph.AddEdge(new Edge("1to2", graph.Vertices.ElementAt(1), graph.Vertices.ElementAt(2)));
            graph.AddEdge(new Edge("2to3", graph.Vertices.ElementAt(2), graph.Vertices.ElementAt(3)));
            graph.AddEdge(new Edge("2to4", graph.Vertices.ElementAt(2), graph.Vertices.ElementAt(4)));
            graph.AddEdge(new Edge("0to5", graph.Vertices.ElementAt(0), graph.Vertices.ElementAt(5)));
            graph.AddEdge(new Edge("1to7", graph.Vertices.ElementAt(1), graph.Vertices.ElementAt(7)));
            graph.AddEdge(new Edge("4to6", graph.Vertices.ElementAt(4), graph.Vertices.ElementAt(6)));

            GraphModels.Add(new GraphModel("Fa", graph));

            #endregion
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}