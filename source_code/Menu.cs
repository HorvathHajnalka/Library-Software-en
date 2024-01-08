using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Menu
    {
        public static User Login_Logout(Users users)
        {
            bool loggedIn = false;
            string username, password;
            User user = null;

            while (!loggedIn)
            {
                Console.Write("Username: ");
                username = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();
                for (int i = 0; i < users.GetSize(); i++)
                {
                    if (users.GetNameByIndex(i) == username && users.GetPasswordByIndex(i) == password)
                    {
                        loggedIn = true;
                        Console.WriteLine("Login successful!");
                        user = users.GetObjectByIndex(i);
                        break;
                    }
                }
                if (!loggedIn)
                { Console.WriteLine("Login failed!"); }
            }
            return user;
        }

        public static string Operations(string type)
        {
            string operationNumber;
            Console.WriteLine($"Logged in as {type}");
            Console.WriteLine("Possible operations:");

            if (type == "admin")
            {
                Console.WriteLine("Create user: 001");
                Console.WriteLine("Delete user: 002");
                Console.WriteLine("Update user data: 003");
                Console.WriteLine("List number of users: 004");
                Console.WriteLine("List all users: 005");
            }
            else if (type == "librarian")
            {
                Console.WriteLine("Search and filter: 001");
                Console.WriteLine("Add book to the system: 002");
                Console.WriteLine("Delete book: 003");
                Console.WriteLine("Add lending to the system: 004");
                Console.WriteLine("Add lending fulfillment to the system: 005");
                Console.WriteLine("List available books: 006");
            }
            else if (type == "reader")
            {
                Console.WriteLine("Search and filter: 001");
                Console.WriteLine("List available books: 002");
                Console.WriteLine("Reservation: 003");
                Console.WriteLine("Payment: 004");
                Console.WriteLine("Extension: 005");
                Console.WriteLine("List borrowings: 006");
            }
            Console.WriteLine();
            Console.Write("Enter the desired operation number: ");
            operationNumber = Console.ReadLine();
            Console.WriteLine();
            return operationNumber;
        }

        public static void Control(string type, string op_number, Users users, Inventory inv, User user)
        {
            if (type == "admin")
            {
                AdminOperations(op_number, users);
            }
            else if (type == "librarian")
            {
                LibrarianOperations(op_number, inv, users);
            }
            else if (type == "reader")
            {
                Reader r = user as Reader;
                ReaderOperations(op_number, inv, r);
            }
        }

        public static void AdminOperations(string operationNumber, Users users)
        {
            if (operationNumber == "001")
            {
                string new_name, new_id, new_address, new_phoneNumber, new_email, new_password, new_permission, new_designation;
                bool emptyParam = false;

                Console.Write("Name: ");
                new_name = Console.ReadLine();
                Console.Write("ID: ");
                new_id = Console.ReadLine();
                Console.Write("Address: ");
                new_address = Console.ReadLine();
                Console.Write("Phone Number: ");
                new_phoneNumber = Console.ReadLine();
                Console.Write("Email: ");
                new_email = Console.ReadLine();
                Console.Write("Password: ");
                new_password = Console.ReadLine();
                Console.Write("Permission(admin/librarian/reader): ");
                new_permission = Console.ReadLine();

                if (new_name == "" || new_id == "" || new_address == "" || new_phoneNumber == "" || new_email == "" || new_password == "" || new_permission == "")
                {
                    emptyParam = true;
                }

                if (!emptyParam)
                {
                    if (new_permission == "admin")
                    {
                        Console.Write("Designation: ");
                        new_designation = Console.ReadLine();
                        Administrator admin = new Administrator(new_designation, new_name, new_id, new_address, new_phoneNumber, new_email, new_password, new_permission);
                        users.CreateUser(admin);
                        Console.WriteLine("User creation successful! ");
                    }
                    else if (new_permission == "librarian")
                    {
                        Console.Write("Designation: ");
                        new_designation = Console.ReadLine();
                        Librarian librarian = new Librarian(new_designation, new_name, new_id, new_address, new_phoneNumber, new_email, new_password, new_permission);
                        users.CreateUser(librarian);
                        Console.WriteLine("User creation successful! ");
                    }
                    else if (new_permission == "reader")
                    {
                        Reader reader = new Reader(0.0, 0.0, new_name, new_id, new_address, new_phoneNumber, new_email, new_password, new_permission);
                        users.CreateUser(reader);
                        Console.WriteLine("User creation successful! ");
                    }
                    else
                    {
                        Console.WriteLine("Invalid permission!");
                    }
                }
                else
                {
                    Console.WriteLine("Empty parameter!");
                }
            }
            else if (operationNumber == "002")
            {
                string toDelete;
                users.ListAll();
                Console.Write("ID of the user to delete: ");
                toDelete = Console.ReadLine();
                users.DeleteUser(toDelete);
            }
            else if (operationNumber == "003")
            {
                users.ListAll();
                users.UpdateUser();
            }
            else if (operationNumber == "004")
            {
                users.UserTypeCounter();
            }
            else if (operationNumber == "005")
            {
                users.ListAll();
            }
            else
            {
                Console.WriteLine("Invalid operation! ");
                Console.Write("Enter the desired operation number: ");
                operationNumber = Console.ReadLine();
                AdminOperations(operationNumber, users);
            }
        }

        public static void LibrarianOperations(string operationNumber, Inventory inventory, Users users)
        {
            if (operationNumber == "001")
            {
                AdvancedSearchFilter(inventory);
            }
            else if (operationNumber == "002")
            {
                inventory.AddBook();
            }
            else if (operationNumber == "003")
            {
                AdvancedSearchFilter(inventory);
                string bookToDeleteId;
                Console.Write("Enter the ID of the book to delete: ");
                bookToDeleteId = Console.ReadLine();
                inventory.DeleteBook(bookToDeleteId);
            }
            else if (operationNumber == "004")
            {
                users.ListReaders();
                Console.Write("Enter the reader's ID: ");
                string readerId = Console.ReadLine();
                Console.Write("Enter the borrowing ID: ");
                string loanId = Console.ReadLine();
                if (users.GetByID(readerId) is Reader && users.GetByID(readerId) != null)
                {
                    Loan loan = new Loan(readerId, loanId);

                    AdvancedSearchFilter(inventory);

                    Console.Write("Number of books to borrow (maximum 10): ");
                    string quantity = Console.ReadLine();
                    int intQuantity;
                    bool result = Int32.TryParse(quantity, out intQuantity);
                    if (result)
                    {
                        if (intQuantity > 0 && intQuantity < 11 && intQuantity < inventory.GetList().Count() && inventory.GetAvailableQuantity() > 0 && intQuantity <= inventory.GetAvailableQuantity())
                        {
                            for (int i = 0; i < intQuantity; i++)
                            {
                                if (inventory.GetAvailableQuantity() > 0)
                                {
                                    Console.Write("Enter the book ID to borrow: ");
                                    string book_id = Console.ReadLine();
                                    if (inventory.BookIsAvailableForLoan(book_id))
                                    {
                                        loan.AddBook(book_id, inventory);
                                    }
                                    else
                                    {
                                        Console.WriteLine("The book with the given ID cannot be borrowed!");
                                        i--;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Not enough books available for borrowing!");
                                }
                            }
                            Reader r = users.GetByID(readerId) as Reader;
                            r.AddLoan(loan);
                            Console.WriteLine("Borrowing successful!");
                        }
                        else
                        {
                            Console.WriteLine("The value entered must be between 1 and 10 and cannot exceed the inventory, as well as the number of books available for borrowing!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The entered value is not a valid number.");
                    }
                }
                else
                {
                    Console.Write("Invalid ID!");
                }
            }
            else if (operationNumber == "005")
            {
                users.ListReaders();
                Console.Write("Enter the reader's ID: ");
                string readerID = Console.ReadLine();
                if (users.GetByID(readerID) is Reader)
                {
                    Reader r = users.GetByID(readerID) as Reader;
                    List<Loan> loans = r.GetLoans();
                    if (loans.Count() != 0)
                    {
                        r.DisplayLoans();
                        Console.Write("Which book ID do you want to remove from the loans? ");
                        string bookToDeleteId = Console.ReadLine();
                        bool successful = false;
                        int deleteIndex = -1;

                        foreach (Loan loan in loans)
                        {
                            if (loan.GetBooks() != null)
                            {
                                deleteIndex = loan.PerformLoanReturn(bookToDeleteId);
                                if (deleteIndex != -1)
                                {
                                    loan.GetBooks().Remove(loan.GetBooks()[deleteIndex]);
                                    Console.WriteLine("The removal of the book from the loan was successful!");
                                    successful = true;
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Book not found! ");
                            }
                        }

                        if (!successful)
                        {
                            Console.WriteLine("The removal of the book from the loan was unsuccessful! The book was not found!");
                        }

                        deleteIndex = -1;

                        for (int i = 0; i < loans.Count(); i++)
                        {
                            if (loans[i].GetBooks().Count == 0)
                            {
                                deleteIndex = i;
                                break;
                            }
                        }
                        if (deleteIndex != -1)
                        {
                            loans.Remove(loans[deleteIndex]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No loans available! ");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect ID! ");
                    LibrarianOperations(operationNumber, inventory, users);
                }
            }
            else if (operationNumber == "006")
            {
                inventory.ListAvailableBooks();
            }
            else
            {
                Console.WriteLine("Invalid operation! ");
                Console.Write("Enter the desired operation number: ");
                operationNumber = Console.ReadLine();
                LibrarianOperations(operationNumber, inventory, users);
            }
        }


        public static void ReaderOperations(string operationNumber, Inventory inventory, Reader reader)
        {
            if (operationNumber == "001")
            {
                AdvancedSearchFilter(inventory);
            }
            else if (operationNumber == "002")
            {
                inventory.ListAvailableBooks();
            }
            else if (operationNumber == "003")
            {
                AdvancedSearchFilter(inventory);
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyy.MM.dd");
                string readerId = reader.GetID();
                Reservation reservation = new Reservation(formattedDate, readerId);

                Console.Write("How many books do you want to reserve? ");
                string quantity = Console.ReadLine();
                int intQuantity;
                bool result = Int32.TryParse(quantity, out intQuantity);
                if (result)
                {
                    if (intQuantity > 0 && intQuantity < 11 && intQuantity < inventory.GetList().Count())
                    {
                        for (int i = 0; i < intQuantity; i++)
                        {
                            Console.Write("Enter the ID of the book you want to reserve: ");
                            string book_id = Console.ReadLine();
                            if (inventory.BookIsAvailableForReservation(book_id))
                            {
                                reservation.AddBook(book_id, inventory);
                            }
                            else
                            {
                                Console.WriteLine("The book with the given ID cannot be reserved!");
                                i--;
                            }
                        }
                        Console.WriteLine("Reservation successful!");
                    }
                    else
                    {
                        Console.WriteLine("The value entered must be between 1 and 10 and cannot exceed the inventory quantity!");
                    }
                }
                else
                {
                    Console.WriteLine("The entered value is not a valid number.");
                }
            }
            else if (operationNumber == "004")
            {
                reader.Payment();
            }
            else if (operationNumber == "005")
            {
                reader.DisplayLoans();
                Console.WriteLine("Which loan would you like to extend? Enter its ID: ");
                string id = Console.ReadLine();
                Loan loan = reader.GetLoanById(id);

                if (loan != null)
                {
                    loan.Extend();
                }
                else
                {
                    Console.WriteLine("No loan with such an ID exists!");
                }
            }
            else if (operationNumber == "006")
            {
                reader.DisplayLoans();
            }
            else
            {
                Console.WriteLine("Invalid operation! ");
                Console.Write("Enter the desired operation number: ");
                operationNumber = Console.ReadLine();
                ReaderOperations(operationNumber, inventory, reader);
            }
        }

        public static void AdvancedSearchFilter(Inventory inventory)
        {
            string idFilter, titleFilter, authorFilter, publisherFilter, languageFilter, categoryFilter, yearFilter, loanFilter, reservationFilter, value, value2;

            Console.WriteLine("Enter filter conditions (0 = no filter)");
            Console.Write("Enter book ID: ");
            idFilter = Console.ReadLine();
            Console.Write("Enter book title: ");
            titleFilter = Console.ReadLine();
            Console.Write("Enter author name: ");
            authorFilter = Console.ReadLine();
            Console.Write("Enter publisher: ");
            publisherFilter = Console.ReadLine();
            Console.Write("Enter publication year: ");
            yearFilter = Console.ReadLine();
            Console.Write("Enter language: ");
            languageFilter = Console.ReadLine();
            Console.Write("Enter category: ");
            categoryFilter = Console.ReadLine();
            Console.Write("Enter loan status (i/h): ");
            value = Console.ReadLine();
            loanFilter = "0";
            if (value == "0")
            {
                loanFilter = "0";
            }
            else if (value == "i")
            {
                loanFilter = "true";
            }
            else if (value == "h")
            {
                loanFilter = "false";
            }
            else if (value == "")
            {
                loanFilter = "";
            }
            else
            {
                loanFilter = "invalid";
            }
            Console.Write("Enter reservation status (i/h): ");
            value2 = Console.ReadLine();
            reservationFilter = "0";
            if (value2 == "0")
            {
                reservationFilter = "0";
            }
            else if (value2 == "i")
            {
                reservationFilter = "true";
            }
            else if (value2 == "h")
            {
                reservationFilter = "false";
            }
            else if (value2 == "")
            {
                reservationFilter = "";
            }
            else
            {
                loanFilter = "invalid";
            }
            if (idFilter == "" || titleFilter == "" || authorFilter == "" || publisherFilter == "" || yearFilter == "" || languageFilter == "" || categoryFilter == "" || reservationFilter == "" || loanFilter == "")
            {
                Console.WriteLine("Empty parameter! ");
            }
            else if (reservationFilter == "invalid" || loanFilter == "invalid")
            {
                Console.WriteLine("Invalid parameter! ");
            }
            else
            {
                inventory.SearchFilter(idFilter, titleFilter, authorFilter, publisherFilter, languageFilter, categoryFilter, yearFilter, loanFilter, reservationFilter);
            }
        }


    }
}
