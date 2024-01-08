using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Inventory
    {
        private List<Book> inventory;

        public Inventory()
        {
            inventory = new List<Book>();
        }

        public void DeleteBook(string bookId)
        {
            int indexOfBookToDelete = -1;

            for (int i = 0; i < inventory.Count(); i++)
            {
                if (inventory[i].GetBookID() == bookId)
                {
                    indexOfBookToDelete = i;
                }
            }

            if (indexOfBookToDelete != -1)
            {
                inventory.Remove(inventory[indexOfBookToDelete]);

                string filePath = "book.txt";
                //string textToDelete = ;

                //// Reading file content line by line
                //List<string> lines = File.ReadAllLines(filePath).ToList();

                //// Finding and removing lines to be deleted
                //lines.RemoveAll(line => line.Contains(textToDelete));

                //// Writing modified content back to the file
                //File.WriteAllLines(filePath, lines);

                Console.WriteLine("Book deletion successful!");
            }
            else
            {
                Console.WriteLine("Book deletion failed! The book was not found.");
            }
        }

        public void CreateBook(Book book)
        {
            inventory.Add(book);
        }

        public void Add(Book book)
        {
            inventory.Add(book);
        }

        public void InputBookDetails()
        {
            bool isNewlyBorrowed = false, isNewlyReserved = false;
            bool emptyParameter = false;
            bool invalidParameter = false;

            Console.Write("Book ID: ");
            string newBookId = Console.ReadLine();
            Console.Write("Book Title: ");
            string newTitle = Console.ReadLine();
            Console.Write("Author: ");
            string newAuthor = Console.ReadLine();
            Console.Write("Publisher: ");
            string newPublisher = Console.ReadLine();
            Console.Write("Publication Year: ");
            string input = Console.ReadLine();

            int newPublicationYear;

            if (!(Int32.TryParse(input, out newPublicationYear)) || newPublicationYear <= 0)
            {
                invalidParameter = true;
            }

            Console.Write("Language: ");
            string newLanguage = Console.ReadLine();
            Console.Write("Category: ");
            string newCategory = Console.ReadLine();

            if (newBookId == "" || newTitle == "" || newAuthor == "" || newPublisher == "" || newPublicationYear == 0 || newLanguage == "" || newCategory == "")
            {
                emptyParameter = true;
            }

            Console.Write("Is the book borrowed? (yes/no): ");
            string isBorrowed = Console.ReadLine();
            if (isBorrowed == "yes" || isBorrowed == "Yes" || isBorrowed == "y" || isBorrowed == "Y")
            {
                isNewlyBorrowed = true;
            }
            else if (isBorrowed == "no" || isBorrowed == "No" || isBorrowed == "n" || isBorrowed == "N")
            {
                isNewlyBorrowed = false;
            }
            else
            {
                invalidParameter = true;
            }

            Console.Write("Is the book reserved? (yes/no): ");
            string isReserved = Console.ReadLine();

            if (isReserved == "yes" || isReserved == "Yes" || isReserved == "y" || isReserved == "Y")
            {
                isNewlyReserved = true;
            }
            else if (isReserved == "no" || isReserved == "No" || isReserved == "n" || isReserved == "N")
            {
                isNewlyReserved = false;
            }
            else
            {
                invalidParameter = true;
            }

            if (!emptyParameter || !invalidParameter)
            {
                Book newBook = new Book(newBookId, newTitle, newAuthor, newPublisher, newPublicationYear, newLanguage, newCategory, isNewlyBorrowed, isNewlyReserved);
                CreateBook(newBook);
                Console.WriteLine("Book creation successful.");
            }
            else if (invalidParameter)
            {
                Console.WriteLine("Invalid parameter!");
            }
            else if (emptyParameter)
            {
                Console.WriteLine("Empty parameter!");
            }
        }

        public int InstanceCounter(string title)
        {
            int count = 0;
            foreach (Book book in inventory)
            {
                if (book.GetTitle() == title)
                {
                    count++;
                }
            }
            return count;
        }

        public void Print()
        {
            foreach (Book book in inventory)
            {
                Console.WriteLine($"{book.GetAuthor()}: {book.GetTitle()}, Instances: {InstanceCounter(book.GetTitle())}");
            }
        }

        public void SearchFilter(string idFilter, string titleFilter, string authorFilter, string publisherFilter, string languageFilter, string categoryFilter, string publicationYearFilter, string borrowedFilter, string reservedFilter)
        {
            List<Book> newList = new List<Book>();
            bool toDelete = false;
            bool toPrint = false;
            foreach (Book book in inventory)
            {
                if (idFilter != book.GetBookID() && idFilter != "0")
                    toDelete = true;
                if (titleFilter != book.GetTitle() && titleFilter != "0")
                    toDelete = true;
                if (authorFilter != book.GetAuthor() && authorFilter != "0")
                    toDelete = true;
                if (publisherFilter != book.GetPublisher() && publisherFilter != "0")
                    toDelete = true;
                if (languageFilter != book.GetLanguage() && languageFilter != "0")
                    toDelete = true;
                if (categoryFilter != book.GetCategory() && categoryFilter != "0")
                    toDelete = true;

                if (publicationYearFilter != "0")
                {
                    if (Convert.ToInt32(publicationYearFilter) != book.GetPublicationYear())
                        toDelete = true;
                }
                if (borrowedFilter != book.GetBorrowed_STRING() && borrowedFilter != "0")
                    toDelete = true;
                if (reservedFilter != book.GetReserved_STRING() && reservedFilter != "0")
                    toDelete = true;
                if (!toDelete) newList.Add(book);
            }
            foreach (Book book in newList)
            {
                book.PrintBook();
                toPrint = true;
                Console.WriteLine();
            }
            if (!toPrint)
            {
                Console.WriteLine("No book found with the provided parameters!");
            }
        }

        public void AvailableBooks()
        {
            List<Book> books = new List<Book>(inventory);
            foreach (Book book in books)
            {
                if (!book.GetBorrowed())
                {
                    book.PrintBook();
                }
            }
        }

        public List<Book> GetList() { return inventory; }

        public bool BookAvailable_forLoan(string book_id)
        {
            bool canBorrow = false;
            if (inventory.Count() != 0)
            {
                foreach (Book book in inventory)
                {
                    if (!book.GetBorrowed() && book.GetBookID() == book_id)
                    {
                        canBorrow = true;
                    }
                }
            }
            return canBorrow;
        }
        public bool BookAvailable_forReservation(string book_id)
        {
            bool canReserve = false;
            if (inventory.Count() != 0)
            {
                foreach (Book book in inventory)
                {
                    if (!book.GetReserved() && book.GetBookID() == book_id)
                    {
                        canReserve = true;
                    }
                }
            }
            return canReserve;
        }

        public Book GetObjectByID(string ID)
        {
            foreach (Book book in inventory)
            {
                if (book.GetBookID() == ID)
                    return book;
            }
            return null;
        }
        public int GetNonBorrowedCount()
        {
            int count = 0;
            foreach (Book book in inventory)
            {
                if (!book.GetBorrowed())
                    count++;
            }
            return count;
        }
    }
}