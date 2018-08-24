using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBookCollection;
using MyBookCollection.Fakes;


namespace MyBookCollectionTests
{
    [TestClass]
    public class ManagerTests
    {
        public TestContext TestContext { get; set; }

        Manager manager;
        string testLogin;
        string testPassword;
        string testTitle;
        string testAuthor;

        [TestInitialize]
        public void TestsInitialize()
        {
            manager = new Manager();
            testLogin = "Kamil";
            testPassword = "Haslo";
            testTitle = "Tytul";
            testAuthor = "Autor";
        }

        [TestMethod]
        public void ReaderDoesNotExists()
        {
            var testReader = new StubIApplicationReader()
            {
                LoginGet = () => { return testLogin; },
                PasswordGet = () => { return testPassword; }
            };
            Assert.IsFalse(manager.ReaderExists(testReader));
        }

        [TestMethod]
        public void ReaderExists()
        {
            var testReader = new StubIApplicationReader()
            {
                LoginGet = () => { return testLogin; },
                PasswordGet = () => { return testPassword; }
            };
            manager.AddReader(testReader);
            Assert.IsTrue(manager.ReaderExists(testReader));
        }

        [TestMethod]
        public void GetReaderDoesNotExists()
        {
            var testReader = new StubIApplicationReader()
            {
                LoginGet = () => { return testLogin; }
            };
            Assert.AreEqual(manager.GetReader(testReader.LoginGet()), null);
        }

        [TestMethod]
        public void GetReaderExists()
        {
            var testReader = new StubIApplicationReader()
            {
                LoginGet = () => { return testLogin; }
            };
            manager.AddReader(testReader);
            Assert.AreEqual(manager.GetReader(testReader.LoginGet()), testReader);
        }
        
        [TestMethod]
        public void AddReaderTrue()
        {
            Assert.IsTrue(manager.AddReader(testLogin, testPassword));
        }

        [TestMethod, DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\Test.csv", "Test#csv", DataAccessMethod.Sequential), DeploymentItem("Test.csv")]
        public void AddReaderFromCSV()
        {
            string login = TestContext.DataRow["login"].ToString();
            string password = TestContext.DataRow["password"].ToString();
            Assert.IsTrue(manager.AddReader(login, password));
        }

        [TestMethod]
        public void DeleteReaderFalse()
        {
            Assert.IsFalse(manager.DeleteReader(testLogin));
        }

        [TestMethod]
        public void DeleteReaderTrue()
        {
            manager.AddReader(testLogin, testPassword);
            Assert.IsTrue(manager.DeleteReader(testLogin));
        }
             
        [TestMethod]
        public void AddBookTest()
        {
            var testReader = new StubIApplicationReader()
            {
                LoginGet = () => { return testLogin; },
                PasswordGet = () => { return testPassword; },
                BookCollectionGet = () => { return new List<IApplicationBook>(); }
            };
           
            manager.AddReader(testReader);
            manager.AddBook(testReader, testTitle, testAuthor);
            StringAssert.Equals(manager.Books[0].Title, testTitle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddBookTestWithNullTitle()
        {
            var testReader = new StubIApplicationReader()
            {
                LoginGet = () => { return testLogin; },
                PasswordGet = () => { return testPassword; },
                BookCollectionGet = () => { return new List<IApplicationBook>(); }
            };
            string nullTitle = null;
            manager.AddReader(testReader);
            manager.AddBook(testReader, nullTitle, testAuthor);
            StringAssert.Equals(manager.Books[0].Title, nullTitle);
        }

        [TestMethod]
        public void AddReaderSameFiveTimes()
        {
            int number = 5;
            for (int i = 0; i < number; i++)
                manager.AddReader(testLogin, testPassword);
             Assert.AreEqual(manager.Readers.Count, 1);
        }

    }
    }
