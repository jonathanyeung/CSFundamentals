using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interviews;
using System.Threading;

namespace Tests
{
    /// <summary>
    /// Summary description for ThreadingTests
    /// </summary>
    [TestClass]
    public class ThreadingTests
    {
        [TestMethod]
        public void BankTransactionsTest()
        {
            var threading = new Threading();
            threading.StartTransactions();
        }

        [TestMethod]
        public void ReturnValuesTest()
        {
            var threading = new Threading();
            threading.ReturnValuesFromThread();
        }

        [TestMethod]
        public void BackgroundThreadTest()
        {
            var threading = new Threading();
            threading.BackgroundThreads();

            Thread.Sleep(3000);
        }
    }
}
