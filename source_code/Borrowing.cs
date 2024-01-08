using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace LibrarySystem
{
    public class Borrowing
    {
        private List<Book> books;
        private string borrowingDate;
        private string dueDate;
        private string readerId;
        private string borrowingID;

        public List<Book> Books { get; set; }
        public string BorrowingDate { get; set; }
        public string DueDate { get; set; }
        public string ReaderId { get; set; }
        public string BorrowingID { get; set; }

        [JsonConstructor]
        public Borrowing(string borrowingDate, string dueDate, string readerId, string borrowingID)
        {
            BorrowingDate = borrowingDate;
            DueDate = dueDate;
            ReaderId = readerId;
            BorrowingID = borrowingID;
            books = new List<Book>();
        }

        public Borrowing()
        {
            books = new List<Book>();
        }

        public Borrowing(string borrowingDate, string dueDate, string readerId, string[] bookIds, Inventory inventory, string borrowingID)
        {
            BorrowingDate = borrowingDate;
            DueDate = dueDate;
            ReaderId = readerId;
            books = new List<Book>();
            BorrowingID = borrowingID;

            foreach (string id in bookIds)
            {
                books.Add(inventory.GetObjectByID(id));
            }
        }

        public Borrowing(string readerId, string borrowingID)
        {
            this.BorrowingID = borrowingID;
            this.ReaderId = readerId;
            books = new List<Book>();
            DateTime currentDate = DateTime.Now;
            BorrowingDate = currentDate.ToString("yyyy.MM.dd");

            DueDate = AddOneMonth(BorrowingDate);
        }

        public void AddBook(string id, Inventory inventory)
        {
            foreach (Book book in inventory.GetList())
            {
                if (book.GetBookID() == id)
                {
                    book.SetBorrowed(true);
                    books.Add(book);
                }
            }
        }

        public int CompleteBorrowing(string idToRemove)
        {
            int indexToRemove = -1;

            for (int i = 0; i < books.Count(); i++)
            {
                if (books[i].GetBookID() == idToRemove)
                {
                    indexToRemove = i;
                }
            }
            return indexToRemove;
        }

        public void Extend()
        {
            bool success = false;
            foreach (Book book in books)
            {
                if (!book.GetReserved())
                {
                    DueDate = AddOneMonth(DueDate);
                    Console.Write($"Extension successful! New due date: {DueDate}\n");
                    success = true;
                    break;
                }
            }

            if (!success)
            {
                Console.WriteLine("This book has been reserved by someone else. You cannot extend the borrowing!\n");
            }
        }
        public string AddOneMonth(string baseDate)
        {
            string[] date = baseDate.Split('.');

            if (date[1] == "01") date[1] = "02";
            else if (date[1] == "02") date[1] = "03";
            else if (date[1] == "03") date[1] = "04";
            else if (date[1] == "04") date[1] = "05";
            else if (date[1] == "05") date[1] = "06";
            else if (date[1] == "06") date[1] = "07";
            else if (date[1] == "07") date[1] = "08";
            else if (date[1] == "08") date[1] = "09";
            else if (date[1] == "09") date[1] = "10";
            else if (date[1] == "10") date[1] = "11";
            else if (date[1] == "11") date[1] = "12";
            else if (date[1] == "12")
            {
                int year = Convert.ToInt32(date[0]) + 1;
                date[0] = Convert.ToString(year);
                date[1] = "01";
            }
            string newDate = date[0] + "." + date[1] + "." + date[2];
            return newDate;
        }

        public string GetBorrowingDate() { return borrowingDate; }
        public string GetDueDate() { return dueDate; }
        public string GetReaderId() { return readerId; }
        public List<Book> GetBooks() { return books; }
        public string GetBorrowingID() { return borrowingID; }

        public string GetObjectAsString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Book book in books)
            {
                sb.Append(book.GetBookID());
                sb.Append("\t");
            }
            sb.Remove(sb.Length - 1, 1); // Remove the last tab

            sb.Append($";{borrowingDate};{dueDate};{readerId}");
            string result = sb.ToString();
            return result;
        }

        public void SetBorrowingDate(string bd)
        {
            borrowingDate = bd;
        }
        public void SetDueDate(string dd)
        {
            dueDate = dd;
        }
        public void SetReaderId(string rid)
        {
            readerId = rid;
        }
        public void SetBorrowingID(string bid)
        {
            borrowingID = bid;
        }
    }
}