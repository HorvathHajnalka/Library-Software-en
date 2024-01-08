using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Users users = new Users();
            List<Reader> readers = new List<Reader>();
            Inventory inventory = new Inventory();
            List<Borrowing> borrowings = new List<Borrowing>();

            // Reading - books
            using (StreamReader reader = new StreamReader("books.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string[] pieces = line.Split(";");

                    string id = pieces[0];
                    string title = pieces[1];
                    string author = pieces[2];
                    string publisher = pieces[3];
                    int publicationYear = Convert.ToInt32(pieces[4]);
                    string language = pieces[5];
                    string category = pieces[6];
                    bool isBorrowed = Convert.ToBoolean(pieces[7]);
                    bool isReserved = Convert.ToBoolean(pieces[8]);

                    Book book = new Book(id, title, author, publisher, publicationYear, language, category, isBorrowed, isReserved);
                    inventory.CreateBook(book);
                }
            }

            // Reading - users
            using (StreamReader reader = new StreamReader("users.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string[] pieces = line.Split(";");

                    string name = pieces[0];
                    string userId = pieces[1];
                    string address = pieces[2];
                    string phoneNumber = pieces[3];
                    string email = pieces[4];
                    string password = pieces[5];
                    string role = pieces[6];

                    if (role == "librarian")
                    {
                        string position = pieces[7];
                        Librarian librarian = new Librarian(position, name, userId, address, phoneNumber, email, password, role);
                        users.CreateUser(librarian);
                    }
                    else if (role == "admin")
                    {
                        string position = pieces[7];
                        Administrator admin = new Administrator(position, name, userId, address, phoneNumber, email, password, role);
                        users.CreateUser(admin);
                    }
                }
            }

            // Reading - readers
            string json = File.ReadAllText("data.json");
            List<Reader> readersData = JsonConvert.DeserializeObject<List<Reader>>(json);

            foreach (var readerData in readersData)
            {
                Reader reader = new Reader(readerData.MembershipFee, readerData.OverdueFee,
                    readerData.Name, readerData.Id, readerData.Address, readerData.Phone, readerData.Email,
                    readerData.Password, readerData.Role);

                foreach (var borrowing in readerData.Borrowings)
                {
                    Borrowing borrowingData = new Borrowing();
                    borrowingData.SetBorrowDate(borrowing.BorrowDate);
                    borrowingData.SetDueDate(borrowing.DueDate);
                    borrowingData.SetReaderId(borrowing.ReaderId);
                    borrowingData.SetBorrowId(borrowing.BorrowId);

                    foreach (var book in borrowing.Books)
                    {
                        borrowingData.AddBook(book.BookId, inventory);
                    }

                    reader.AddBorrowing(borrowingData);
                }

                readers.Add(reader);
                users.CreateUser(reader);
            }


            while (true)
            {
            Login:
                Console.Clear();
                User user = Menu.Login_Logout(users);
                string userType = user.GetPermission();
            ControlPanel:
                Console.Clear();
                string operationNumber = Menu.Operations(userType);
                Menu.Control(userType, operationNumber, users, inventory, user);

                Console.WriteLine("Continue session [ENTER]");
                Console.WriteLine("Logout [ESC]");
                ConsoleKey input;
                do
                {
                    input = Console.ReadKey(true).Key;
                    Console.Clear();
                    switch (input)
                    {
                        case ConsoleKey.Enter:
                            goto ControlPanel;

                        case ConsoleKey.Escape:
                            // Delete JSON
                            try
                            {
                                // Check if the file exists
                                string filePath = "data.json";
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath); // Delete the file
                                                           //Console.WriteLine("JSON file deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("The file does not exist.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error occurred while deleting the file: {ex.Message}");
                            }

                            // New JSON
                            readers.Clear();
                            foreach (User user01 in users.GetList())
                            {
                                if (user01 is Reader)
                                {
                                    readers.Add(user01 as Reader);
                                }
                            }

                            List<object> dataList = new List<object>();

                            foreach (Reader r in readers)
                            {
                                var borrowingsList = new List<object>();
                                foreach (Borrowing bor in r.GetBorrowings())
                                {
                                    var booksList = new List<object>();
                                    foreach (Book book in bor.GetBooks())
                                    {
                                        var singleBook = new
                                        {
                                            Book_id = book.GetBookID(),
                                            Title = book.GetTitle(),
                                            Author = book.GetAuthor(),
                                            Publisher = book.GetPublisher(),
                                            Publication_year = book.GetPublicationYear(),
                                            Language = book.GetLanguage(),
                                            Category = book.GetCategory(),
                                            Is_borrowed = book.GetIsBorrowed(),
                                            Is_reserved = book.GetIsReserved()
                                        };
                                        booksList.Add(singleBook);
                                    }
                                    var borrowingVar = new
                                    {
                                        Books = booksList,
                                        Borrow_date = bor.GetBorrowDate(),
                                        Due_date = bor.GetDueDate(),
                                        Reader_id = bor.GetReaderId(),
                                        Borrow_id = bor.GetBorrowId()
                                    };
                                    borrowingsList.Add(borrowingVar);
                                }
                                var data = new
                                {
                                    Membership_fee = r.GetMembershipFee(),
                                    Overdue_fee = r.GetOverdueFee(),
                                    Name = r.GetName(),
                                    ID = r.GetID(),
                                    Address = r.GetAddress(),
                                    Tel = r.GetPhoneNumber(),
                                    Email = r.GetEmail(),
                                    Password = r.GetPassword(),
                                    Permission = r.GetPermission(),
                                    Borrowings = borrowingsList
                                };

                                dataList.Add(data);
                            }

                            // Create JSON file and write data into it
                            var jsonData = JsonConvert.SerializeObject(dataList, Formatting.Indented);
                            File.WriteAllText("data.json", jsonData);

                            // Write users
                            using (StreamWriter writer = new StreamWriter("users.txt"))
                            {
                                foreach (User u in users.GetList())
                                {
                                    if (u is Librarian)
                                    {
                                        Librarian lib = u as Librarian;
                                        writer.WriteLine(lib.GetObjectAsString());
                                    }

                                    if (u is Administrator)
                                    {
                                        Administrator admin = u as Administrator;
                                        writer.WriteLine(admin.GetObjectAsString());
                                    }
                                }
                            }

                            // Write books
                            using (StreamWriter writer = new StreamWriter("books.txt"))
                            {
                                foreach (Book book in inventory.GetList())
                                {
                                    writer.WriteLine(book.GetObjectAsString());
                                }
                            }

                            goto Login;
                    }
                } while (input != ConsoleKey.Enter && input != ConsoleKey.Escape);
            }
        }
    }
}

