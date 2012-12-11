using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using DataStructures;
using GraphSharp.Controls;
using QuickGraph;
using QuickGraph.Serialization;
using Tries.UI.Model;
using Tries.UI.ViewModels;

namespace Tries.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        public MainViewModel ViewModel { get; private set; }

        private void Generate_Trie(object sender, RoutedEventArgs e)
        {
            var graph = new BidirectionalGraph<Vertex, IEdge<Vertex>>();

            var trie = new Trie<int>();
            var keys = ViewModel.TrieText.Split(' ');
            for(int i = 0; i < keys.Length; i++)
            {
                trie.Put(keys[i], i + 1);
            }

            //walk the trie and initialize the graph
            var vertex = new Vertex("root");
            graph.AddVertex(vertex);
            _Add(graph, vertex, trie.Root);

            //update view model
            ViewModel.Graph = graph;
        }

        private void _Add(BidirectionalGraph<Vertex, IEdge<Vertex>> graph, Vertex root, Trie<int>.Node node)
        {
            for(int next = 0; next < 256; next++)
            {
                if(node.Links[next] != null)
                {
                    //add vertex
                    var vertex = new Vertex(((char)next).ToString());
                    graph.AddVertex(vertex);

                    var edge = new Edge<Vertex>(root, vertex);
                    graph.AddEdge(edge);

                    _Add(graph, vertex, node.Links[next]);
                }
            }
        }
    }

    public class MyGraphLayout : GraphLayout<Vertex, IEdge<Vertex>, IBidirectionalGraph<Vertex, IEdge<Vertex>>> { }
}
