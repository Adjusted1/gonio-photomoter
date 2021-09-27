using Xitron_Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Xitron_Controller_Tests
{
    
    
    /// <summary>
    ///This is a test class for Xitron280xControllerTest and is intended
    ///to contain all Xitron280xControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Xitron280xControllerTest
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Xitron280xController Constructor
        ///</summary>
        [TestMethod()]
        public void Xitron280xControllerConstructorTest()
        {
            Xitron280xController target = new Xitron280xController();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for getDeviceList
        ///</summary>
        [TestMethod()]
        public void getDeviceListTest()
        {
            Xitron280xController target = new Xitron280xController();
            IEnumerable<string> expected = new List<string>();
            IEnumerable<string> actual;
            actual = target.getDeviceList();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for refreshDeviceList
        ///</summary>
        [TestMethod()]
        public void refreshDeviceListTest()
        {
            Xitron280xController target = new Xitron280xController(); // TODO: Initialize to an appropriate value
            target.refreshDeviceList();
        }
    }
}
