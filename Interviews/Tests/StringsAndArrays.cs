using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class StringsAndArrays
    {
        #region Unique Letters
        [TestMethod]
        public void UniqueLettersTest()
        {
            Assert.IsTrue(UniqueLetters(""));
            Assert.IsTrue(UniqueLetters("a"));
            Assert.IsTrue(UniqueLetters("abcd"));
            Assert.IsFalse(UniqueLetters("abcda"));
            Assert.IsTrue(UniqueLetters("12345"));
            Assert.IsFalse(UniqueLetters("123451"));
            Assert.IsFalse(UniqueLetters("aaabbbccc"));
            Assert.IsFalse(UniqueLetters("abcdb"));

            Assert.IsTrue(UniqueLettersTwo(""));
            Assert.IsTrue(UniqueLettersTwo("a"));
            Assert.IsTrue(UniqueLettersTwo("abcd"));
            Assert.IsFalse(UniqueLettersTwo("abcda"));
            Assert.IsTrue(UniqueLettersTwo("12345"));
            Assert.IsFalse(UniqueLettersTwo("123451"));
            Assert.IsFalse(UniqueLettersTwo("aaabbbccc"));
            Assert.IsFalse(UniqueLettersTwo("abcdb"));
        }

        /// <summary>
        /// Determine if a string has all unique letters.
        /// CTCI: q 1.1; pg. 73 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool UniqueLetters(string s)
        {
            var dict = new Dictionary<char, int>();

            foreach(char c in s)
            {
                if (dict.ContainsKey(c))
                {
                    return false;
                }
                else
                {
                    dict.Add(c, 1);
                }
            }

            return true;
        }

        /// <summary>
        /// No additional data structures
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool UniqueLettersTwo(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (s[i] == s[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region IsPermutation

        [TestMethod]
        public void IsPermutationTest()
        {
            Assert.IsTrue(IsPermutation("", ""));
            Assert.IsTrue(IsPermutation("a", "a"));
            Assert.IsTrue(IsPermutation("ab", "ab"));
            Assert.IsTrue(IsPermutation("ab", "ba"));
            Assert.IsTrue(IsPermutation("aa", "aa"));
            Assert.IsTrue(IsPermutation("abcdefa", "abcadef"));
            Assert.IsTrue(IsPermutation("aaabbcd", "dcababa"));

            Assert.IsFalse(IsPermutation("abc", "abb"));
            Assert.IsFalse(IsPermutation("a", "b"));
            Assert.IsFalse(IsPermutation("abb", "ab"));
            Assert.IsFalse(IsPermutation("ab", "abb"));
        }

        private bool IsPermutation(string a, string b)
        {
            var dict = new Dictionary<char, int>();

            foreach (char s in a)
            {
                if (dict.ContainsKey(s))
                {
                    dict[s]++;
                }
                else
                {
                    dict.Add(s, 1);
                }
            }

            foreach (char s in b)
            {
                if (dict.ContainsKey(s))
                {
                    dict[s]--;
                    if (dict[s] < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            foreach (var key in dict.Keys)
            {
                if (dict[key] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region PercentTwenty

        [TestMethod]
        public void PercentTwentyTest()
        {
            Assert.AreEqual("HelloWorld", PercentTwenty("HelloWorld"));
            Assert.AreEqual("Hello%20World", PercentTwenty("Hello World  "));
            Assert.AreEqual("Hello%20Worldy%20World", PercentTwenty("Hello Worldy World    "));
        }

        public string PercentTwenty(string s)
        {
            var newStr = new char[s.Length];

            if (s.Length <= 1)
            {
                return s;
            }

            int endPtr;
            int indexPtr;

            endPtr = s.Length - 1;
            indexPtr = endPtr;

            while (indexPtr >= 0)
            {
                if (s[indexPtr] == ' ')
                {
                    indexPtr--;
                }
                else
                {
                    break;
                }
            }

            while (indexPtr >= 0)
            {
                if (s[indexPtr] == ' ')
                {
                    newStr[endPtr] = '0';
                    endPtr--;
                    newStr[endPtr] = '2';
                    endPtr--;
                    newStr[endPtr] = '%';
                    endPtr--;
                }
                else
                {
                    newStr[endPtr] = s[indexPtr];
                    endPtr--;
                }

                indexPtr--;
            }

            return new string(newStr);
        }

        #endregion

        /// <summary>
        /// Capitalize all words in a sentence.
        /// </summary>
        [TestMethod]
        public void CapitalizeWordsTest()
        {
            var ret = CapitalizeWords("hello my name is bob  and you'renot   cool");
        }

        public string CapitalizeWords(string input)
        {
            var sb = new StringBuilder(input);

            bool capitalize = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (sb[i] == ' ')
                {
                    capitalize = true;
                }
                else if (capitalize)
                {
                    char c = sb[i];
                    var s = c.ToString().ToUpper();
                    sb[i] = s[0];
                    capitalize = false;
                }
            }

            return sb.ToString();
        }
    }
}
