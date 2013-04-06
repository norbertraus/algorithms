using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFind
{
    /// <summary>
    /// Dynamic connectivity problem.
    /// </summary>
    public interface IDynamicConnectivityFinder
    {
        /// <summary>
        /// Merges two components if the two given sites are in different components
        /// </summary>
        void Union(int p, int q);
        
        /// <summary>
        /// Provides component identifier for a given site
        /// </summary>
        int Find(int p);
        
        /// <summary>
        /// Determines whether two given sites are in the same component
        /// </summary>
        bool AreConnected(int p, int q);
        
        /// <summary>
        /// Provides number of components
        /// </summary>
        int Count { get; }
    }

    public abstract class DynamicConnectivityFinder : IDynamicConnectivityFinder
    {
        protected int _count;
        protected int[] _components; //provides component id that are indexed by the site id

        protected DynamicConnectivityFinder(int size)
        {
            _count = size;
            _components = new int[size];
            for (int i = 0; i < size; i++) //initally each site is disconnected and has different component
            {
                _components[i] = i;
            }
        }

        public abstract void Union(int p, int q);
        public abstract int Find(int p);
        
        public bool AreConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }
        
        public int Count { get { return _count; } }
    }

    public class QuickFind : DynamicConnectivityFinder
    {
        public QuickFind(int size)
            : base(size)
        {
        }

        public override void Union(int p, int q)
        {
            int componentP = Find(p);
            int componentQ = Find(q);

            if (componentP != componentQ)
            {
                for (int i = 0; i < _components.Length; i++)
                {
                    if (_components[i] == componentP)
                        _components[i] = componentQ; //change the component id for "p" to value of "q"
                }

                _count--;
            }
        }

        public override int Find(int p)
        {
            return _components[p];
        }

        
    }

    /// <summary>
    /// Instead of storing index of component for the site, it stores the "link" to the next site in the component. At the beginning after initialization
    /// the link point to itself indicating single one-site components  
    /// </summary>
    public class QuickUnion : DynamicConnectivityFinder
    {
        public QuickUnion(int size)
            : base(size)
        {
        }

        public override void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);

            if (rootP != rootQ)
            {
                _components[rootP] = rootQ;
                _count--;
            }
        }

        /// <summary>
        /// we start at the given site, follow its link to another site, follow that site’s link to yet
        /// another site, and so forth, following links until reaching a root, a site that
        /// has a link to itself
        /// </summary>
        /// <remarks>
        /// Two sites are in the same component if and only if this process leads them to the same root.
        /// </remarks>
        public override int Find(int p)
        {
            while (p != _components[p])
            {
                p = _components[p];
            }
            return p;
        }
    }

    /// <summary>
    /// Uses size of the component trees with mering two components and links smaller tree to larger to balance the size of the newly created component tree
    /// </summary>
    /// <remarks>
    /// The weighted quick-union algorithm uses at most cMlgN array accesses to process M connections among N sites for a small constant c.
    /// </remarks>
    public class WeightedQuickUnion : QuickUnion
    {
        private int[] _sizes; //size of component for roots indexed by site

        public WeightedQuickUnion(int size)
            : base(size)
        {
            _sizes = new int[size];
            for (int i = 0; i < size; i++)
            {
                _sizes[i] = 1;
            }
        }

        public override void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);

            if(rootP != rootQ)
            {
                //check with root is smaller
                if (_sizes[rootP] < _sizes[rootQ])
                {
                    _components[rootP] = rootQ;
                    _sizes[rootQ] += _sizes[rootP];
                }
                else
                {
                    _components[rootQ] = rootP;
                    _sizes[rootP] += _sizes[rootQ];
                }

                _count--;
            }
        }
       
    }
}
