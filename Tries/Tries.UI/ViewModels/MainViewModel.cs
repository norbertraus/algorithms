using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using QuickGraph;
using Tries.UI.Annotations;
using Tries.UI.Model;

namespace Tries.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            TrieText = "she sells the shells close to the sea";
        }

        private BidirectionalGraph<Vertex, IEdge<Vertex>> _graph;
        public BidirectionalGraph<Vertex, IEdge<Vertex>> Graph
        {
            get { return _graph; }
            set 
            { 
                _graph = value;
                OnPropertyChanged();
            }
        }

        private string _trieText;
        public string TrieText
        {
            get { return _trieText; }
            set 
            { 
                if (_trieText != value)
                {
                    _trieText = value;
                    OnPropertyChanged();
                }
            }
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