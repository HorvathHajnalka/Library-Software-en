using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Book
    {
        private string bookId;
        private string title;
        private string author;
        private string publisher;
        private int publicationYear;
        private string language;
        private string category;
        private bool borrowed;
        private bool reserved;

        public string BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public bool Borrowed { get; set; }
        public bool Reserved { get; set; }

        public Book(string _bookId, string _title, string _author, string _publisher, int _publicationYear,
            string _language, string _category, bool _borrowed, bool _reserved)
        {
            bookId = _bookId;
            title = _title;
            author = _author;
            publisher = _publisher;
            publicationYear = _publicationYear;
            language = _language;
            category = _category;
            borrowed = _borrowed;
            reserved = _reserved;
        }

        public void PrintBookDetails()
        {
            Console.WriteLine($"ID: {bookId}, Title: {title}, Author: {author}, Publisher: {publisher}, Publication Year: {publicationYear}, Language: {language}, Category: {category}, Borrowed: {borrowed}, Reserved: {reserved}");
        }

        public void SetBorrowed(bool isBorrowed) { borrowed = isBorrowed; }

        public void SetReserved(bool isReserved) { reserved = isReserved; }

        public string GetBookId() { return bookId; }
        public string GetAuthor() { return author; }
        public string GetTitle() { return title; }
        public string GetPublisher() { return publisher; }
        public int GetPublicationYear() { return publicationYear; }
        public string GetLanguage() { return language; }
        public string GetCategory() { return category; }
        public bool GetBorrowed() { return borrowed; }
        public bool GetReserved() { return reserved; }

        public string GetBorrowedAsString()
        {
            if (borrowed)
                return "true";
            else
                return "false";
        }
        public string GetReservedAsString()
        {
            if (reserved)
                return "true";
            else
                return "false";
        }

        public string GetObjectAsString()
        {
            return $"{bookId};{title};{author};{publisher};{publicationYear};{language};{category};{borrowed};{reserved}";
        }
    }
}

