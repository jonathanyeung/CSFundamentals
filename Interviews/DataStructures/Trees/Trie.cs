using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trees
{
    public class Trie
    {
        private TrieNode _root;
        const char delimiter = ' ';

        public Trie()
        {
            _root = new TrieNode(delimiter);
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            var curNode = _root;

            for (int i = 0; i < word.Length; i++)
            {
                var c = word[i];

                if (!curNode.Children.ContainsKey(c))
                {
                    curNode.AddChild(c);
                }

                curNode = curNode.Children[c];

                if (i == word.Length - 1)
                {
                    curNode.AddChild(delimiter);
                }
            }
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            var curNode = _root;
            word += delimiter;
            for (int i = 0; i < word.Length; i++)
            {
                if (curNode.Children.ContainsKey(word[i]))
                {
                    curNode = curNode.Children[word[i]];
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            var curNode = _root;
            for (int i = 0; i < prefix.Length; i++)
            {
                if (curNode.Children.ContainsKey(prefix[i]))
                {
                    curNode = curNode.Children[prefix[i]];
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }

    class TrieNode
    {
        public char Letter;
        public Dictionary<char, TrieNode> Children;

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }

        public TrieNode(char c)
        {
            Letter = c;
            Children = new Dictionary<char, TrieNode>();
        }

        public void AddChild(char c)
        {
            if (!Children.ContainsKey(c))
            {
                Children.Add(c, new TrieNode(c));
            }
        }
    }

    /**
     * Your Trie object will be instantiated and called as such:
     * Trie obj = new Trie();
     * obj.Insert(word);
     * bool param_2 = obj.Search(word);
     * bool param_3 = obj.StartsWith(prefix);
     */
}
