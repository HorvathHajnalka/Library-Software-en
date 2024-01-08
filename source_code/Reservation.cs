using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Reservation
    {
        private List<Book> books;
        private string date;
        private string readerId;

        public Reservation(string _date, string _readerId)
        {
            date = _date;
            readerId = _readerId;
            books = new List<Book>();
        }

        public void AddBook(string id, Inventory inventory)
        {
            foreach (Book book in inventory.GetList())
            {
                if (book.GetBookID() == id)
                {
                    book.SetReserved(true);
                    books.Add(book);
                }
            }
        }
    }
}
