using System;
using System.Linq;
using NUnit.Framework;

namespace DataStructures.Tests
{
    public class Given_populated_trie
    {
        private Trie<int> _trie;

        [SetUp]
        public void SetUp()
        {
            _trie = new Trie<int>();

            var keys = "she sells sea shells by the sea shore".Split(' ');
            for (int i = 0; i < keys.Length; i++)
            {
                _trie.Put(keys[i], i + 1);
            }
        }

        [Test]
        public void should_return_keys_with_prefix()
        {
            var keys = _trie.KeysWithPrefix("sh");

            Assert.That(keys.Count(), Is.EqualTo(3));
            Assert.That(keys.SingleOrDefault(x => x == "she"), Is.Not.Null);
            Assert.That(keys.SingleOrDefault(x => x == "shells"), Is.Not.Null);
            Assert.That(keys.SingleOrDefault(x => x == "shore"), Is.Not.Null);
        }

        [Test]
        public void should_handle_wildcard_prefix()
        {
            var keys = _trie.KeysWithMatch("*he");

            Assert.That(keys.Count(), Is.EqualTo(2));
            Assert.That(keys.SingleOrDefault(x => x == "she"), Is.Not.Null);
            Assert.That(keys.SingleOrDefault(x => x == "the"), Is.Not.Null);
        }
    }
}