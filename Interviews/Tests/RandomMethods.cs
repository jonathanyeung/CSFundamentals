using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataStructures;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class RandomMethods
    {
        #region String Encoding
        /// <summary>
        /// Given a string of numbers, with the following mapping:
        /// 1 -> A, 2 -> B, ..., 26 -> Z, return the number of 
        /// different ways to decode the string.  I.E.
        /// 26 -> BF or 26 -> Z.
        /// </summary>
        [TestMethod]
        public void FindEncodingPossiblities()
        {
            Assert.AreEqual(1, encodingPossibilities("1"));

            Assert.AreEqual(2, encodingPossibilities("11"));

            Assert.AreEqual(3, encodingPossibilities("111"));

            Assert.AreEqual(1, encodingPossibilities("27"));

            Assert.AreEqual(2, encodingPossibilities("26"));
        }

        internal int encodingPossibilities(string input)
        {
            if (input.Length <= 1)
            {
                return 1;
            }
            if (input[0] == '1' || (input[0] == '2' && input[1] <= '6'))
            {
                return encodingPossibilities(input.Substring(1)) + encodingPossibilities(input.Substring(2));
            }

            return encodingPossibilities(input.Substring(1));
        }

        #endregion

        #region Sudoku
        [TestMethod]
        public void SudokuCheckerTest()
        {

        }

        internal bool SudokuChecker(int[,] board)
        {
            for (int i = 0; i < 10; i++)
            {
                if (!CheckSquare(board, i))
                {
                    return false;
                }
                if (!CheckRow(board, i))
                {
                    return false;
                }
                if (!CheckColumn(board, i))
                {
                    return false;
                }
            }
            return true;
        }

        internal bool CheckSquare(int[,] board, int squareIndex)
        {
            var row = squareIndex / 3;
            var col = squareIndex % 3;

            var list = new List<int>();

            for (int i = row * 3; i < row * 3 + 3; i++)
            {
                for (int j = col * 3; col < col * 3 + 3; j++)
                {
                    if (board[i, j] != -1)
                    {
                        list.Add(board[i, j]);
                    }
                }
            }

            return CheckListForDuplicates(list);
        }

        internal bool CheckRow(int[,] board, int row)
        {
            var list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                if (board[row, i] != -1)
                {
                    list.Add(board[row, i]);
                }
            }

            return CheckListForDuplicates(list);
        }

        internal bool CheckColumn(int[,] board, int column)
        {
            var list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                if (board[i, column] != -1)
                {
                    list.Add(board[column, i]);
                }
            }

            return CheckListForDuplicates(list);
        }

        internal bool CheckListForDuplicates(List<int> input)
        {
            var check = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (var i in input)
            {
                if (check[i] != 0)
                {
                    return false;
                }
                check[i]++;
            }

            return true;
        }
        #endregion

        #region Deck Shuffler
        //Shuffle a deck of cards.
        [TestMethod]
        public void DeckShufflerTest()
        {
            var deck = new int[52];
            for (int i = 0; i < 52; i++)
            {
                deck[i] = i;
            }

            DeckShuffler(deck);
        }


        internal void DeckShuffler(int[] deck)
        {
            var length = deck.Length;

            var r = new Random();

            for (int i = deck.Length - 1; i > 0; i--)
            {
                var index = r.Next(0, i - 1);
                var tmp = deck[i];
                deck[i] = deck[index];
                deck[index] = tmp;
            }
        }
        #endregion

        #region stringRotation

        [TestMethod]
        public void RotateStringSingular()
        {
            string test = "helloworld";
            var c = test.ToCharArray();
            var l = c.Length;

            char temp = c[0];

            for (int i = 0; i < test.Length - 1; i++)
            {
                c[i] = c[i + 1];
            }

            c[test.Length - 1] = temp;
        }

        [TestMethod]
        public void RotateStringTest()
        {
            var res = RotateString("helloworld", 1);
            res = RotateString("helloworld", 2);
            res = RotateString("hellow", 3);
            res = RotateString("helloworld", 3);
        }

        [TestMethod]
        public void IsIntegerPresentTest()
        {
            var test = new int[] { 16, 21, 22, 4, 5, 10, 11, 14 };

            Assert.IsTrue(IsIntInArray(test, 4, 0, test.Length - 1));

            Assert.IsTrue(IsIntInArray(test, 16, 0, test.Length - 1));

            Assert.IsTrue(IsIntInArray(test, 14, 0, test.Length - 1));
        }

        private bool IsIntInArray(int[] input, int k, int left, int right)
        {
            if (left > right)
            {
                return false;
            }

            var mid = (left + right) / 2;

            if (input[mid] == k)
            {
                return true;
            }

            else if (input[mid] < k)
            {
                if (input[right] >= k)
                {
                    return IsIntInArray(input, k, mid + 1, right);
                }
                else
                {
                    return IsIntInArray(input, k, left, mid - 1);
                }
            }

            // input[mid] > k
            else
            {
                if (input[left] <= k)
                {
                    return IsIntInArray(input, k, left, mid - 1);
                }
                else
                {
                    return IsIntInArray(input, k, mid + 1, right);
                }
            }
        }



        /// <summary>
        /// Rotate a string by k positions, using only 1 additional byte of storage.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        internal string RotateString(string s, int k)
        {
            var chars = s.ToCharArray();

            var gcd = GCD(s.Length, k);

            for (int i = 0; i < gcd; i++)
            {
                int start = i;
                var temp = chars[i];
                for (int j = 0; j < (s.Length / gcd) - 1; j++)
                {
                    chars[(i + k * j) % s.Length] = chars[(i + k * (j + 1)) % s.Length];
                }

                chars[(((s.Length / gcd) - 1) * k + i) % s.Length] = temp;
            }

            return new string(chars);
        }

        static int GCD(int a, int b)
        {
            return (b == 0) ? a : GCD(b, a % b);
        }
        #endregion

        #region DictionaryStuffs

        [TestMethod]
        public void DictionaryTesting()
        {
            var d = new Dictionary<TwoInt, int>(new EqualityComparer());

            d.Add(new TwoInt(1, 2), 1);

            var t = new TwoInt(1, 2);

            var effed = !d.ContainsKey(t);
        }
        #endregion

        #region nMostCommonChars
        [TestMethod]
        public void nMostCommonCharsTest()
        {
            var res = nMostCommonChars("aaaaaabbbbcccdde", 1);
            res = nMostCommonChars("aaaaaabbbbcccdde", 2);
            res = nMostCommonChars("aaaaaabbbbcccdde", 3);
            res = nMostCommonChars("aaaaaabbbbcccdde", 4);
            res = nMostCommonChars("aaaaaabbbbcccdde", 5);
            res = nMostCommonChars("aaaaaabbbbcccdde", 6);
        }

        public string nMostCommonChars(string s, int n)
        {
            var d = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (!d.ContainsKey(c))
                {
                    d.Add(c, 1);
                }
                else
                {
                    d[c]++;
                }
            }

            var topN = new MinHeap<charIntPair>();

            int count = 0;

            foreach (var key in d.Keys)
            {
                if (count < n)
                {
                    topN.Insert(new charIntPair() { c = key, count = d[key] });
                }
                else
                {
                    if (d[key] > topN.Peek().count)
                    {
                        topN.ExtractMin();
                        topN.Insert(new charIntPair() { c = key, count = d[key] });
                    }
                }

                count++;
            }

            string result = "";

            while (topN.Count != 0)
            {
                result += topN.ExtractMin().c;
                result += ", ";
            }
            result = result.Remove(result.Length - 2);

            return result;
        }
        #endregion

        #region ParseMathExpression

        [TestMethod]
        public void ParseMathExpressionTest()
        {
            //var ret = ConvertExpressionToInfixNotation("4 + 5");
            //ret = ConvertExpressionToInfixNotation("4 + 5 * 3");

            var ret = ConvertExpressionToRPNNotation("3 + 4 * 2 / ( 1 - 5)");
            var answer = ComputeRPN(ret);
        }

        internal int ComputeRPN(string input)
        {
            var chars = input.Split(' ');

            var operandStack = new Stack<string>();

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == "/" ||
                    chars[i] == "*" ||
                    chars[i] == "+" ||
                    chars[i] == "-")
                {
                    var arg2 = Convert.ToInt32(operandStack.Pop());
                    var arg1 = Convert.ToInt32(operandStack.Pop());

                    switch (chars[i])
                    {
                        case "/":
                            operandStack.Push((arg1 / arg2).ToString());
                            break;
                        case "*":
                            operandStack.Push((arg1 * arg2).ToString());
                            break;
                        case "+":
                            operandStack.Push((arg1 + arg2).ToString());
                            break;
                        case "-":
                            operandStack.Push((arg1 - arg2).ToString());
                            break;
                    }
                }
                else if (chars[i] != "")
                {
                    operandStack.Push(chars[i]);
                }
            }

            return Convert.ToInt32(operandStack.Pop());
        }

        /// <summary>
        /// Converts a math expression to its Reverse Polish notation, for simpler
        /// computation.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal string ConvertExpressionToRPNNotation(string input)
        {
            var operatorStack = new Stack<char>();
            var outputQueue = new Queue<string>();

            bool parsingNumber = false;
            int numberStartIndex = 0;

            int temp;

            input = input.Replace(" ", "");
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '/' || 
                    input[i] == '*' || 
                    input[i] == '+' ||
                    input[i] == '-' || 
                    input[i] == '(')
                {
                    if (parsingNumber)
                    {
                        parsingNumber = false;
                        outputQueue.Enqueue(int.Parse(input.Substring(numberStartIndex, i - numberStartIndex)).ToString());
                    }

                    if (operatorStack.Count > 0)
                    {
                        var curTop = operatorStack.Peek();

                        if ((curTop == '/' || curTop == '*') && (input[i] == '/' || input[i] == '*'))
                        {
                            outputQueue.Enqueue(operatorStack.Pop().ToString());
                        }
                        else if ((curTop == '+' || curTop == '-') && (input[i] == '+' || input[i] == '-'))
                        {
                            outputQueue.Enqueue(operatorStack.Pop().ToString());
                        }
                    }
                    operatorStack.Push(input[i]);
                }
                else if (input[i] == ')')
                {
                    if (parsingNumber)
                    {
                        parsingNumber = false;
                        outputQueue.Enqueue(int.Parse(input.Substring(numberStartIndex, i - numberStartIndex)).ToString());
                    }

                    var op = operatorStack.Pop();

                    while(op != '(')
                    {
                        outputQueue.Enqueue(op.ToString());
                        
                        if (operatorStack.Count == 0)
                        {
                            throw new ArgumentException("Mismatched Parenthesis in Expression!");
                        }

                        op = operatorStack.Pop();
                    }
                }
                else if (!parsingNumber && Int32.TryParse(input[i].ToString(), out temp))
                {
                    parsingNumber = true;
                    numberStartIndex = i;
                }
                else
                {
                    throw new ArgumentException("Invalid Argument");
                }
            }
            if (parsingNumber)
            {
                outputQueue.Enqueue(int.Parse(input.Substring(numberStartIndex, input.Length - numberStartIndex)).ToString());
            }

            while(operatorStack.Count != 0)
            {
                outputQueue.Enqueue(operatorStack.Pop().ToString());
            }

            string result = "";

            while (outputQueue.Count > 0)
            {
                result += outputQueue.Dequeue();
                result += " ";
            }

            result.Trim(' ');

            return result;
        }
        internal enum mathToken
        {
            number,
            op,
            leftParen,
            rightParen            
        }



        #endregion

        #region Crossword Verifier

        [TestMethod]
        public void CrosswordVerifierTest()
        {
            var test = new bool[3, 3] { { false, false, false }, { false, false, false }, { false, false, false } };

            Assert.IsTrue(CrosswordVerifier(test));

            test = new bool[3, 3] { { true, true, false }, { false, false, false }, { false, false, false } };

            Assert.IsTrue(CrosswordVerifier(test));

            test = new bool[3, 3] { { true, false, false }, { false, true, false }, { false, false, true } };

            Assert.IsFalse(CrosswordVerifier(test));

            test = new bool[3, 3] { { false, false, true }, { false, false, true }, { false, false, false } };

            Assert.IsTrue(CrosswordVerifier(test));
        }

        /// <summary>
        /// Verify that a given crossword puzzle is valid.  A crossword puzzle is 
        /// valid if every white space is accessible to each other through a
        /// non-diagonal.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool CrosswordVerifier(bool[,] input)
        {
            var check = new bool[input.GetLength(0), input.GetLength(1)];

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    check[i, j] = false;
                }
            }

            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j])
                    {
                        Mark(input, check, i, j);
                        goto cont;
                    }
                }
            }

        cont:
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    if (input[i, j] && !check[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void Mark(bool[,] input, bool[,] check, int i, int j)
        {
            if (input[i, j] == false || check[i, j] == true)
            {
                return;
            }

            check[i, j] = true;

            if (i - 1 > 0)
            {
                Mark(input, check, i - 1, j);
            }
            if (i + 1 < input.GetLength(0))
            {
                Mark(input, check, i + 1, j);
            }
            if (j - 1 > 0)
            {
                Mark(input, check, i, j - 1);
            }
            if (j + 1 < input.GetLength(1))
            {
                Mark(input, check, i, j + 1);
            }
        }

        #endregion

        #region String Compression

        [TestMethod]
        public void StringCompressionTest()
        {
            Assert.AreEqual("a4b3c2d1", StringCompression("aaaabbbccd"));

            Assert.AreEqual("abcdd", StringCompression("abcdd"));
        }

        /// <summary>
        /// This method will try to compress a string with the following algo:
        /// aaaabb -> a4b2.  If the compressed version is longer than the
        /// original, return the original.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string StringCompression(string input)
        {
            char curChar = ' ';
            int curCount = 0;
            string resultString = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == curChar)
                {
                    curCount++;
                }
                else
                {
                    if (curChar != ' ')
                    {
                        resultString = resultString + curChar + curCount;
                    }
                    curChar = input[i];
                    curCount = 1;
                }
            }

            resultString = resultString + curChar + curCount;

            return (resultString.Length >= input.Length ? input : resultString);
        }

        #endregion

        #region Image Rotation
        [TestMethod]
        public void RotateImageTest()
        {
            var input = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
            RotateImage(input);
            RotateImage(input);
        }

        private void RotateImage(int[,] input)
        {
            if (input.GetLength(0) != input.GetLength(1))
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < input.GetLength(0) / 2; i++)
            {
                for (int j = i; j < input.GetLength(0) - 1 - i; j++)
                {
                    RotateFour(input, i, j);
                }
            }
        }

        private void RotateFour(int[,] input, int i, int j)
        {
            int tmp = input[i, j];

            input[i, j] = input[input.GetLength(0) - 1 - j, i];
            input[input.GetLength(0) - 1 - j, i] = input[input.GetLength(0) - 1 - i, input.GetLength(0) - 1 - j];
            input[input.GetLength(0) - 1 - i, input.GetLength(0) - 1 - j] = input[j, input.GetLength(0) - 1 - i];
            input[j, input.GetLength(0) - 1 - i] = tmp;
        }

        #endregion

        #region TowersOfHanoi


        [TestMethod]
        public void TowersOfHanoiTest()
        {
            var left = new Stack<int>();

            for (int i = 4; i > 0; i--)
            {
                left.Push(i);
            }

            var mid = new Stack<int>();
            var right = new Stack<int>();

            TowersOfHanoi(left, mid, right);
        }

        private void TowersOfHanoi(Stack<int> left, Stack<int> mid, Stack<int> right)
        {
            SwapTowers(left, mid, right, left.Count);
        }

        private void SwapTowers(Stack<int> source, Stack<int> temp, Stack<int> dest, int n)
        {
            if (n == 0)
            {
                return;
            }
            else if (n == 1)
            {
                var i = source.Pop();
                dest.Push(i);
            }
            else
            {
                SwapTowers(source, dest, temp, n - 1);
                var i = source.Pop();
                dest.Push(i);
                SwapTowers(temp, source, dest, n - 1);
            }
        }

        #endregion
    }

    public class charIntPair : IComparable
    {
        public char c;
        public int count;

        public int CompareTo(object obj)
        {
            if (count < ((charIntPair)obj).count)
            {
                return -1;
            }
            else if (count == ((charIntPair)obj).count)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
    public class EqualityComparer : IEqualityComparer<TwoInt>
    {
        public bool Equals(TwoInt x, TwoInt y)
        {
            return (x.one == y.one && x.two == y.two);
        }

        public int GetHashCode(TwoInt obj)
        {
            return obj.two ^ obj.one;
        }
    }

    public class TwoInt
    {
        public int one;
        public int two;

        public TwoInt(int a, int b)
        {
            one = a; two = b;
        }
    }

    public class DataStructurePlayground
    {
        public void Test()
        {
            var q = new Queue<int>();

            q.Enqueue(1);
            var res = q.Dequeue();
        }

        public void TestStack()
        {
            var s = new Stack<int>();

            s.Push(25);
        }
    }
}
