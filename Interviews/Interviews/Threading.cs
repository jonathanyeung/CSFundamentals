using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Interviews
{
    public class Threading
    {

        private static int _bankTotal = 0;

        private static object _lock = new object();

        public void StartTransactions()
        {
            _bankTotal = 0;
            //  Notes: The argument to the thread constructor must be a delegate that takes an argument of type object.
            //  You cannot get type safety through this technique.            

            for (int i = 0; i < 10; i++ )
            {
                var newThread = new Thread(Threading.UpdateBankAccount);

                newThread.Start(10);
            }
        }

        //Notes: The argument to the Thread
        public static void UpdateBankAccount(object data)
        {
            lock (_lock)
            {
                _bankTotal += (int)data;
                Console.WriteLine("Updating Bank Account with ${0}!", _bankTotal);
            }
        }

    }
}
