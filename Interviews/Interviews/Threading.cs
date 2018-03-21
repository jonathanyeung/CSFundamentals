using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace Interviews
{
    public class Threading
    {
        #region Bank Transactions

        private static int _bankTotal = 0;
        private static object _lock = new object();

        public void StartTransactions()
        {
            _bankTotal = 0;
            //  Notes: The argument to the thread constructor must be a delegate that takes an argument of type object.
            //  You cannot get type safety through this technique.            

            for (int i = 0; i < 10; i++)
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
                Debug.WriteLine("Updating Bank Account with ${0}!", _bankTotal);
            }
        }

        #endregion

        #region Return Values
        /// <summary>
        /// Demonstration on how to get return values from a thread. Wrap the 
        /// target method in a class that contains fields that can be set.
        /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/threading/parameters-and-return-values-for-multithreaded-procedures
        /// </summary>
        public void ReturnValuesFromThread()
        {
            var rect = new Rectangle() { Width = 5, Height = 2 };
            var thread = new Thread(rect.CalculateArea);
            thread.Start();

            thread.Join();

            Debug.WriteLine("Area is {0}", rect.Area);
        }

        private class Rectangle
        {
            public int Width;
            public int Height;
            public int Area;

            public void CalculateArea()
            {
                this.Area = this.Width * this.Height;
            }
        }

        #endregion

        #region Background Threads
        public void BackgroundThreads()
        {
            var backgroundThread = new BackgroundWorker();
            backgroundThread.RunWorkerCompleted += BackgroundThread_RunWorkerCompleted;
            backgroundThread.DoWork += BackgroundThread_DoWork;

            var rect = new Rectangle() { Height = 5, Width = 2 };

            backgroundThread.RunWorkerAsync(rect);
        }

        private void BackgroundThread_DoWork(object sender, DoWorkEventArgs e)
        {
            var rect = e.Argument as Rectangle;

            rect.CalculateArea();

            e.Result = rect;
        }

        private void BackgroundThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var rect = e.Result as Rectangle;

            if (rect != null)
            {
                Debug.WriteLine("Rectangle Area: {0}", rect.Area);
            }
        }
        #endregion
    }

    /// <summary>
    /// Thread Safe Version of the Singleton implementation. This prevents
    /// multiple threads from calling the Singleton constructor by putting
    /// a lock on it.
    /// </summary>
    public sealed class ThreadSafeSingleton
    {
        private static ThreadSafeSingleton _instance = null;

        private static readonly object _lock = new object();

        private ThreadSafeSingleton() { }

        public static ThreadSafeSingleton GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = new ThreadSafeSingleton();
                }
            }

            return _instance;
        }
    }


}
