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
        private Trie<int> _trie;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        public MainViewModel ViewModel { get; private set; }

        //void LoadGraphFromTrie()
        //{
        //    _trie = new Trie<int>();

        //    var keys = "she sells sea shells by the sea shore".Split(' ');
        //    for(int i = 0; i < keys.Length; i++)
        //    {
        //        _trie.Put(keys[i], i + 1);
        //    }

        //    //walk the trie and initialize the graph
        //    //var graph = new Graph();
        //    //var root = new Vertex("Root");
        //    //graph.AddVertex(root);

        //    //_Add(graph, root, _trie.Root);

        //    //set the graph

        //    var graph = new Graph();

        //    for(int i = 0; i < 8; i++)
        //    {
        //        var v = new Vertex(i.ToString());
        //        graph.AddVertex(v);
        //    }

        //    graph.AddEdge(new Edge("0to1", graph.Vertices.ElementAt(0), graph.Vertices.ElementAt(1)));
        //    graph.AddEdge(new Edge("1to2", graph.Vertices.ElementAt(1), graph.Vertices.ElementAt(2)));
        //    graph.AddEdge(new Edge("2to3", graph.Vertices.ElementAt(2), graph.Vertices.ElementAt(3)));
        //    graph.AddEdge(new Edge("2to4", graph.Vertices.ElementAt(2), graph.Vertices.ElementAt(4)));
        //    graph.AddEdge(new Edge("0to5", graph.Vertices.ElementAt(0), graph.Vertices.ElementAt(5)));
        //    graph.AddEdge(new Edge("1to7", graph.Vertices.ElementAt(1), graph.Vertices.ElementAt(7)));
        //    graph.AddEdge(new Edge("4to6", graph.Vertices.ElementAt(4), graph.Vertices.ElementAt(6)));

        //    ViewModel.Graph = graph;
        //}

        //private void _Add(Graph graph, Vertex root, Trie<int>.Node node)
        //{
        //    foreach (var link in _trie.Root.Links.Where(x => x.Value != 0))
        //    {
        //        var vertex = new Vertex(link.Value.ToString());
        //        graph.AddVertex(vertex);

        //        var edge = new Edge("", root, vertex);
        //        graph.AddEdge(edge);

        //        _Add(graph, vertex, link);
        //    }
        //}
    }
}
