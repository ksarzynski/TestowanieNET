using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBookCollection;
using MyBookCollection.Fakes;

namespace MyBookCollectionTests
{
    [TestClass]
    public class ReaderTests
    {
        Reader reader;
        string testLogin;
        string testPassword;
        string testTitle;
      
        [TestInitialize]
        public void TestInitialize()
        {
            testLogin = "Kamil";
            testPassword = "Haslo";
            testTitle = "Tytul";
            reader = new Reader(testLogin, testPassword);
        }

        [TestMethod]
        public void ShowBooksEmpty()
        {
            StringAssert.Equals(reader.ShowBooks(), string.Empty);
        }

        [TestMethod]
        public void ShowBooksNotEmpty()
        {
            reader.BookCollection.Add(new StubIApplicationBook() { TitleGet = () => { return testTitle; } });
            StringAssert.Contains(reader.ShowBooks(), testTitle);
        }
               
        [TestMethod]
        public void DeleteBookFalse()
        {
            Assert.IsFalse(reader.DeleteBook(0));
        }

        [TestMethod]
        public void DeleteBookTrue()
        {
            var testBook = new StubIApplicationBook();
            reader.BookCollection.Add(testBook);
            reader.DeleteBook(0);
            CollectionAssert.DoesNotContain(reader.BookCollection, testBook);
        }

        [TestMethod]
        public void DeleteAllBooks()
        {
            int number = 5;
            for (int i = 0; i < number; i++)
            {
                reader.BookCollection.Add(new StubIApplicationBook());
            }
            reader.DeleteAllBooks();
            Assert.AreEqual(reader.BookCollection.Count, 0);
        }
        
    }
    }
