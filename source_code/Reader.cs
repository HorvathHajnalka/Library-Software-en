using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Reader : User
    {
        private double membershipFeeToPay;
        private double overdueFeeToPay;
        private List<Borrowing> borrowings;

        public double MembershipFee { get; set; }
        public double OverdueFee { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<Borrowing> Borrowings { get; set; }

        public Reader(double membershipFeeToPay, double overdueFeeToPay, string name, string userId,
            string address, string phoneNumber, string email, string password, string role) :
            base(name, userId, address, phoneNumber, email, password, role)
        {
            this.membershipFeeToPay = membershipFeeToPay;
            this.overdueFeeToPay = overdueFeeToPay;
            borrowings = new List<Borrowing>();
        }

        public void Payment()
        {
            Console.WriteLine($"Membership fee to pay: {GetMembershipFeeToPay()} Ft");
            Console.WriteLine($"Overdue fee to pay: {GetOverdueFeeToPay()} Ft");

            if (GetOverdueFeeToPay() == 0 && GetMembershipFeeToPay() == 0)
            {
                Console.WriteLine("No amount to pay.");
            }
            else if (GetOverdueFeeToPay() != 0)
            {
                Console.WriteLine("Pay membership fee: 001");
                Console.WriteLine("Pay overdue fee: 002");
                Console.Write("Enter the desired action number: ");
                string actionNumber = Console.ReadLine();

                if (actionNumber == "001")
                {
                    if (GetMembershipFeeToPay() != 0)
                    {
                        Console.WriteLine("Press ENTER to make the payment!");
                        ConsoleKey inputKey;
                        do
                        {
                            inputKey = Console.ReadKey(true).Key;
                        } while (inputKey != ConsoleKey.Enter);

                        PayMembershipFee();
                    }
                    else
                        Console.WriteLine("No amount to pay.");
                }
                else if (actionNumber == "002")
                {
                    Console.WriteLine("Press ENTER to make the payment!");
                    ConsoleKey inputKey;
                    do
                    {
                        inputKey = Console.ReadKey(true).Key;
                    } while (inputKey != ConsoleKey.Enter);

                    PayOverdueFee();
                }
                else
                {
                    Console.WriteLine("Invalid action! ");
                    Payment();
                }
            }
            else
            {
                Console.WriteLine("Press ENTER to make the payment!");
                ConsoleKey inputKey;
                do
                {
                    inputKey = Console.ReadKey(true).Key;
                } while (inputKey != ConsoleKey.Enter);

                PayMembershipFee();
            }
        }

        public void PayMembershipFee()
        {
            membershipFeeToPay = 0;
            Console.WriteLine("Payment successful! ");
            Console.WriteLine($"Membership fee to pay: {membershipFeeToPay} Ft");
            Console.WriteLine($"Overdue fee to pay: {overdueFeeToPay} Ft");
        }

        public void PayOverdueFee()
        {
            overdueFeeToPay = 0;
            Console.WriteLine("Payment successful! ");
            Console.WriteLine($"Membership fee to pay: {membershipFeeToPay} Ft");
            Console.WriteLine($"Overdue fee to pay: {overdueFeeToPay} Ft");
        }

        public void AddBorrowing(Borrowing borrowing)
        {
            borrowings.Add(borrowing);
        }

        public void DisplayBorrowings()
        {
            foreach (Borrowing borrowing in borrowings)
            {
                Console.WriteLine($"Borrowing date: {borrowing.GetBorrowDate()}");
                Console.WriteLine($"Due date: {borrowing.GetDueDate()}");
                Console.WriteLine($"Borrowing ID: {borrowing.GetBorrowId()}");

                if (borrowing.GetBooks() != null)
                {
                    foreach (Book book in borrowing.GetBooks())
                    {
                        book.DisplayBook();
                    }
                }
                else
                {
                    Console.WriteLine("No books borrowed!");
                }

                Console.WriteLine();
            }
        }

        // Getters and setters
        public void SetMembershipFeeToPay(double _amount)
        {
            membershipFeeToPay += _amount;
        }

        public void SetOverdueFeeToPay(double _amount)
        {
            overdueFeeToPay += _amount;
        }

        public double GetOverdueFeeToPay() { return overdueFeeToPay; }
        public double GetMembershipFeeToPay() { return membershipFeeToPay; }

        public List<Borrowing> GetBorrowings() { return borrowings; }
        public Borrowing GetBorrowingById(string id)
        {
            foreach (Borrowing borrowing in borrowings)
            {
                if (borrowing.GetBorrowId() == id) { return borrowing; }
            }
            return null;
        }

        public string GetObjectAsString()
        {
            return "";
        }
    }
}
