using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DataStructures.Tests
{
    public class Given_empty_trie
    {
        private Trie<int> _trie;

        //It was the best of times, it was the worst of times,

        [SetUp]
        public void SetUp()
        {
            _trie = new Trie<int>();
        }

        [Test]
        public void should_be_empty()
        {
            Assert.That(_trie.Size, Is.EqualTo(0));
            Assert.That(_trie.Keys.Count(), Is.EqualTo(0));
        }
                
        [Test]
        public void should_add_new_key()
        {
            _trie.Put("it", 1);
            
            Assert.That(_trie.Size, Is.EqualTo(1));
            Assert.That(_trie.Keys.First(), Is.EqualTo("it"));
        }

        [Test]
        public void should_add_two_keys()
        {
            _trie.Put("it", 1);
            _trie.Put("was", 2);

            Assert.That(_trie.Size, Is.EqualTo(2));
            Assert.That(_trie.Keys.First(), Is.EqualTo("it"));
            Assert.That(_trie.Keys.Skip(1).First(), Is.EqualTo("was"));
        }

        [Test]
        public void should_get_value_for_key()
        {
            _trie.Put("it", 3);

            Assert.That(_trie.GetValue("it"), Is.EqualTo(3));
        }

        [Test]
        public void should_override_key_value()
        {
            _trie.Put("it", 1);
            _trie.Put("it", 2);

            Assert.That(_trie.GetValue("it"), Is.EqualTo(2));
        }
    }

   
}
