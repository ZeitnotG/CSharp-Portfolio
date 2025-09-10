using System;
using System.Collections.Generic;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("War and Piece", "Tolstoy", 1869, Genre.Romance);
            Book book2 = new Book("Anna Karenina", "Tolstoy", 1877, Genre.Romance);
            Book book3 = new Book("Crime and Punishment", "Dostoevsky", 1866, Genre.Thriller);
            Book book4 = new Book("The Brothers Karamazov", "Dostoevsky", 1880, Genre.Romance);
            Book book5 = new Book("It", "King", 1986, Genre.Horror);
            Library library = new Library();
            library.AddBook(book1);
            library.AddBook(book2);
            library.AddBook(book3);
            library.AddBook(book4);
            library.AddBook(book5);
            library.ShowBooks();
            Reader reader = new Reader("Ivan");
            reader.BorrowBook(library, book1);
            reader.BorrowBook(library, book2 );
            reader.BorrowBook(library, book3);
            reader.BorrowBook(library, book4);
            library.ShowBooks();
            reader.ShowBorrowedBooks();
        }
    }

    public enum Genre
    {
        ScienceFiction,
        Fantasy,
        Mystery,
        Romance,
        Thriller,
        Horror,
        HistoricalFiction
    }
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public void GetInfo()
        {
            Console.WriteLine("Info:");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Year: {Year}");
            Console.WriteLine($"Genre: {Genre}");
        }
        public Book(string title, string author, int year, Genre genre)
        {
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
        }
    }

    public class Library
    {
        private List<Book> Books { get; set; }
        public Library()
        {
            Books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
            Console.WriteLine($"Book {book.Title} has been added to the library");
        }
        public Book LendBook(Book book) 
        {
            if (Books.Contains(book))
            {
                Books.Remove(book);
                Console.WriteLine($"Book {book.Title} has been lended");
                return book;
            }
            else
            {
                Console.WriteLine($"Book {book.Title} is not available in library");
                return null;
            }
        }
        public void ShowBooks()
        {
            if(Books.Count == 0)
            {
                Console.WriteLine("No books available in the library");
                return;
            }
            foreach (Book book in Books) 
            {
                int i = 1;
                book.GetInfo();
            }
        }

        public Book FindBookByTitle(string title)
        {
            foreach (Book book in Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    return book;
            }
            return null;
        }
    }

    public class Reader
    {
        public string Name { get; set; }
        private List<Book> BorrowedBooks { get; set; }
        public int BookLimit { get; private set; } = 3;
        public Reader(string name)
        {
            Name = name;
            BorrowedBooks = new List<Book>();
        }

        public void BorrowBook(Library library, Book book)
        {
            if (BorrowedBooks.Count < BookLimit)
            {
                Book borrowed = library.LendBook(book);
                if (borrowed != null)
                {
                    BorrowedBooks.Add(borrowed);
                }
            }
            else
            {
                Console.WriteLine("You have too many books." +
                    " Return one or more and try again.");
            }
        }
        public void ReturnBook(Library library, Book book)
        {
            BorrowedBooks.Remove(book);
            library.AddBook(book);
        }
        public void ShowBorrowedBooks()
        {
            Console.WriteLine($"The list of borrowed books by {Name}:");
            if (BorrowedBooks.Count == 0)
            {
                Console.WriteLine("No borrowed books");
                return;
            }
            foreach (Book book in BorrowedBooks)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}
