using System;

namespace MyBookCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            Reader firstReader = new Reader("Jan", "Haslo1");
            Reader secondReader = new Reader("Piotr", "Haslo2");
           
            manager.AddReader(firstReader);
            manager.AddReader(secondReader);
          
            manager.AddBook("Jan", "Pierwsza ksiazka Jana", "Autor pierwszej ksiazki");
            manager.AddBook("Jan", "Druga ksiazka Jana", "Autor drugiej ksiazki");
            manager.AddBook("Jan", "Trzecia ksiazka Jana", "Autor trzeciej ksiazki");
            manager.AddBook("Piotr", "Pierwsza ksiazka Piotra", "Autor tej ksiazki");
            Console.WriteLine("Książki Jana:");
            firstReader.ShowBooks();
            Console.WriteLine("Książki Piotra:");
            secondReader.ShowBooks();

            Console.WriteLine("Usuń drugą książkę Jana:");
            firstReader.DeleteBook(1);
            firstReader.ShowBooks();

            Console.WriteLine("Usuń wszystkie książki Jana:");
            firstReader.DeleteAllBooks();
            firstReader.ShowBooks();

            Console.ReadKey();
        }
    }
}