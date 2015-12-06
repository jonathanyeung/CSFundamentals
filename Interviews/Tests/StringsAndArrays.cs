using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Tests
{
    [TestClass]
    public class StringsAndArrays
    {
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
