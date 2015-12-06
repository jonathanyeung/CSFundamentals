using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interviews;
using System.Diagnostics; 

namespace Tests
{
    [TestClass]
    public class BitManipulationTests
    {
        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void TestComputeAndProduct()
        {
            AndProductHelper(12, 15, 12);

            AndProductHelper(1, 10, 0);

            AndProductHelper(551124992, 551129087, 551124992);

            AndProductHelper(10, 11, 0);
            
            AndProductHelper(4, 5, 0);

            AndProductHelper(123, 10, 0);

        }

        private void AndProductHelper(uint a, uint b, uint answer)
        {
            Debug.WriteLine("Input A: {0}, Input B: {1}", a, b);

            var calculatedAnswer = BitManipulation.ComputeAndProduct(a, b);

            
            Debug.WriteLine("Result: {0}, Answer: {1}", calculatedAnswer, answer);

            Debug.WriteLine("--------------------------");
            //Debug.Assert(answer == calculatedAnswer);
        }
    }
}
