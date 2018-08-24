using MyBookCollection;
using System;

namespace MyBookCollection
{
    public interface IApplicationBook
    {
        IApplicationReader User { get; }
        string Title { get; }
        string Author { get; }
    }
    public class Book : IApplicationBook
    {
        public IApplicationReader User { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }

        public Book(IApplicationReader user, string title, string author)
        {
            Title = title;
            Author = author;
        }
    }
}
