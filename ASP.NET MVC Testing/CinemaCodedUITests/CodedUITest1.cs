using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CinemaCodedUITests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Playback.Initialize();
            var bw = BrowserWindow.Launch(new Uri("https://localhost:44378"));
            bw.CloseOnPlaybackCleanup = false;
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            this.UIMap.LogInAdmin();
            this.UIMap.AssertLogInAdmin();

            this.UIMap.CreateInvalidFilm();
            this.UIMap.AssertCreateInvalidFilm();

            this.UIMap.CreateValidFilm();
            this.UIMap.AssertCreateValidFilm();

            this.UIMap.DetailsFilm();

            this.UIMap.EditFilm();
            this.UIMap.AssertEditFilm();

            this.UIMap.CreateInvalidCustomer();
            this.UIMap.AssertCreateInvalidCustomer();

            this.UIMap.CreateValidCustomer();
            this.UIMap.AssertCreateValidCustomer();

            this.UIMap.DetailsCustomer();

            this.UIMap.EditCustomer();
            this.UIMap.AssertEditCustomer();

            this.UIMap.CreateTicket();
            this.UIMap.AssertCreateTicket();

            this.UIMap.DetailsTicket();

            this.UIMap.EditTicket();
            this.UIMap.AssertEditTicket();

            this.UIMap.DeleteTicket();

            this.UIMap.DeleteCustomer();
            
            this.UIMap.DeleteFilm();

            this.UIMap.AdminLogOut();
            this.UIMap.AssertAdminLogOut();

        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

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
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
