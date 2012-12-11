using NUnit.Framework;

namespace DataStructures.Tests
{
    public class Given_empty_ternary_trie
    {
        private TernaryTrie<int> _trie;

        //It was the best of times, it was the worst of times,

        [SetUp]
        public void SetUp()
        {
            _trie = new TernaryTrie<int>();
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