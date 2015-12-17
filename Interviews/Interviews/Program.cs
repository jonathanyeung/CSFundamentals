using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t = new Threading();

            //t.StartTransactions();

            //BitManipulation.ComputeAndProductIO();

            //while(true)
            //{
            //    var input = Console.ReadLine();
            //    var bitShift = Convert.ToUInt32(Console.ReadLine());
            //    Console.WriteLine(BitManipulation.ComputeXorCipher(input, bitShift));

            //}

            while (true)
            {
                Console.WriteLine("---------------------------");
                Console.ReadLine();

                //var trees = new Trees();
                //var res = trees.ConstructBST(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
                //Trees.PrintSubtree(res);

                //trees.PreOrderTraversal(res);
                //Console.WriteLine("---------------------------");
                //trees.InOrderTraversal(res);
                //Console.WriteLine("---------------------------");
                //trees.PostOrderTraversal(res);

                //Console.WriteLine("---------------------------");

                //Console.WriteLine(Trees.isBST(res));

                //var ar = new int[] { 5, 2, 3, 9, 8, 1, 7, 6, 4, 7, 3 };
                var ar = new int[] { 3, 3, 5, 1, 3, 3};
                Sorting.QuickSort(ar);

                Sorting.PrintArray(ar);

                ar = new int[] {1, 2, 3, 5, 8, 13, 21};

                Console.WriteLine(Searching.BinarySearch(ar, 1));

                Console.WriteLine(Searching.BinarySearch(ar, 21));

                Console.WriteLine(Searching.BinarySearch(ar, 13));
            }

        }
    }
}
