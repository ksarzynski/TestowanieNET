using MyBookCollection;
using System;
using System.Collections.Generic;

namespace MyBookCollection
{
    public class Manager
    {
        public List<IApplicationReader> Readers { get; private set; }
        public List<IApplicationBook> Books { get; private set; }

        public Manager()
        {
            Readers = new List<IApplicationReader>();
            Books = new List<IApplicationBook>();
        }

        public bool ReaderExists(string login)
        {
            if (String.IsNullOrEmpty(login) || GetReader(login) == null)
                return false;
            return true;
        }

        public bool ReaderExists(IApplicationReader reader)
        {
            if (reader == null || !Readers.Contains(reader))
                return false;
            return true;
        }

        public IApplicationReader GetReader(string login)
        {
            return Readers.Find(u => u.Login == login);
        }

        public bool AddReader(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                throw new ArgumentNullException();
            if (!ReaderExists(login))
            {
                Readers.Add(new Reader(login, password));
                return true;
            }
            else
                return false;
        }

        public bool AddReader(IApplicationReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException();
            if (!ReaderExists(reader.Login))
            {
                Readers.Add(reader);
                return true;
            }
            else
                return false;
        }

        public bool DeleteReader(string login)
        {
            if (String.IsNullOrEmpty(login))
                throw new ArgumentNullException();
            if (ReaderExists(login))
            {
                Readers.Remove(GetReader(login));
                return true;
            }
            else
                return false;
        }

        public bool DeleteReader(IApplicationReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException();
            if (ReaderExists(reader))
            {
                Readers.Remove(reader);
                return true;
            }
            else
                return false;
        }

        public void AddBook(string user, string title, string author)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(author))
                throw new ArgumentNullException();
            if (String.IsNullOrEmpty(user))
                throw new ArgumentNullException();
             var username = GetReader(user);
             var newBook = new Book(username, title, author);
            username.BookCollection.Add(newBook);
            Books.Add(newBook);
        }

        public void AddBook(IApplicationReader user, string title, string author)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(author))
                throw new ArgumentNullException();
            if (title == null || author == null)
                throw new ArgumentNullException();
            if (!ReaderExists(user))
                throw new Exception();
            var newBook = new Book(user, title, author);
            user.BookCollection.Add(newBook);
            Books.Add(newBook);
        }
    }
}
