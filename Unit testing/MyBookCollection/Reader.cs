using MyBookCollection;
using System;
using System.Collections.Generic;

namespace MyBookCollection
{
    public interface IApplicationReader
    {
        string Login { get; }
        string Password { get; }
        List<IApplicationBook> BookCollection { get; }
        string ShowBooks();
        bool DeleteBook(int index);
        void DeleteAllBooks();
    }


    public class Reader : IApplicationReader
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public List<IApplicationBook> BookCollection { get; private set; }

        public Reader(string login, string password)
        {
            Login = login;
            Password = password;
            BookCollection = new List<IApplicationBook>();

        }

        public string ShowBooks()
        {
            if (BookCollection.Count == 0)
                return string.Empty;
            else
            {
                string AllBooks = "";
                for (int i = 0; i < BookCollection.Count; i++)
                    AllBooks += "[" + i + "]" + " " + BookCollection[i].Title + " | " + BookCollection[i].Author + "\n";
                Console.WriteLine(AllBooks);
                return AllBooks;
            }
        }

        public bool DeleteBook(int index)
        {
            if (index < BookCollection.Count)
            {
                BookCollection.RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        public void DeleteAllBooks()
        {
            BookCollection = new List<IApplicationBook>();
        }

    }
}
