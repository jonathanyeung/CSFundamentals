using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    public class BitManipulation
    {

        public static void ComputeAndProductIO()
        {
            int t = Convert.ToInt32(Console.ReadLine());

            var Results = new uint[t];
            for (int i = 0; i < t; i++)
            {
                String elements = Console.ReadLine();
                String[] split_elements = elements.Split(' ');
                uint a = Convert.ToUInt32(split_elements[0]);
                uint b = Convert.ToUInt32(split_elements[1]);
                Results[i] = BitManipulation.ComputeAndProduct(a, b);


                Console.WriteLine(BitManipulation.ComputeAndProduct(a, b));
            }

            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!");
            for (int i = 0; i < t; i++)
            {
                Console.WriteLine(Results[i]);
            }

            Console.ReadLine();
        }
        public static uint ComputeAndProduct(uint a, uint b)
        {
            // Mark bits that are common to both a and b.
            uint commonBits = ~(a ^ b);

            Debug.WriteLine("Common Bits = {0}", commonBits);
            if (commonBits == 0)
            {
                return 0;
            }

            int highestSignificantBit = 0;
            int highestSignificantZero = -1;

            uint temp = commonBits;

            for (int i = 0; i < 32; i++)
            {
                if ((temp & 1) == 1)
                {
                    highestSignificantBit = i;
                }
                // Wizardry...
                else if ((1 << i) < commonBits)
                {
                    highestSignificantZero = i;
                }

                temp = temp >> 1;
            }

            // Generate the mask
            uint mask = 0;

            if (highestSignificantZero == -1)
            {
                for (int i = 0; i <= highestSignificantBit; i++)
                {
                    mask = mask << 1;
                    mask += 1;
                }
            }
            else
            {
                for (int i = 0; i <= highestSignificantBit; i++)
                {
                    mask = mask << 1;

                    if (i < (highestSignificantBit - highestSignificantZero))
                    {
                        mask += 1;
                    }
                }
            }

            Debug.WriteLine("Mask = {0}", mask);

            return (a & mask);
        }

        public static string ComputeXorCipher(string input, uint bitShift)
        {
            uint inputInt = Convert.ToUInt32(input);

            string output = "";

            uint outputInt = 0;

            if (bitShift == 1)
            {
                return input;
            }

            //Represents the count of 1's (base 2) in the current N sized sliding window
            int countOfOnes = 0;

            Queue<int> slidingWindow = new Queue<int>((int)bitShift - 1);

            for (int i = 0; i < bitShift - 1; i++)
            {
                slidingWindow.Enqueue(0);
            }

            // Iterate backwards in the input string.
            for (int i = 0; i <= input.Length - bitShift; i++)
            {
                countOfOnes = slidingWindow.Count<int>(t => t == 1);

                uint lowestInt = inputInt % 10;

                if (countOfOnes % 2 == 0)
                {
                    lowestInt ^= 0;
                }
                else
                {
                    lowestInt ^= 1;
                }

                output += lowestInt.ToString();

                inputInt /= 10;

                slidingWindow.Dequeue();
                slidingWindow.Enqueue((int)lowestInt);
            }

            char[] charArray = output.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }


}
