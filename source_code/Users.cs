using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Users
    {
        private List<User> users = new List<User>();

        public void CreateUser(User user)
        {
            users.Add(user);
            // Write data to file
        }

        public void Add(User user)
        {
            users.Add(user);
        }

        public void DeleteUser(string userId)
        {
            int indexToDelete = -1;

            for (int i = 0; i < users.Count(); i++)
            {
                if (users[i].GetID() == userId)
                {
                    indexToDelete = i;
                }
            }

            if (indexToDelete != -1)
            {
                users.Remove(users[indexToDelete]);
                Console.WriteLine("User deletion successful!");
            }
            else
            {
                Console.WriteLine("User deletion failed! User not found!");
            }
        }
        public void UpdateUser()
        {
            Console.Write("ID of the user to modify: ");
            string id = Console.ReadLine();
            int indexToModify = GetIndexByID(id);

            if (indexToModify != -1)
            {
                string operationNumber;

                Console.WriteLine("Name modification: 031");
                Console.WriteLine("ID modification: 032");
                Console.WriteLine("Address modification: 033");
                Console.WriteLine("Phone number modification: 034");
                Console.WriteLine("Email modification: 035");
                Console.WriteLine("Password modification: 036");
                Console.WriteLine("Permission modification: 037");

                if (users[indexToModify].GetPermissions() == "admin" || users[indexToModify].GetPermissions() == "librarian")
                {
                    Console.WriteLine("Position modification: 038");
                }
                if (users[indexToModify].GetPermissions() == "reader")
                {
                    Console.WriteLine("Membership fee addition: 039");
                    Console.WriteLine("Late fee addition: 040");
                }

                Console.Write("Enter the desired operation number: ");
                operationNumber = Console.ReadLine();

                switch (operationNumber)
                {
                    case "031":
                        Console.Write("New name: ");
                        string new_name = Console.ReadLine();
                        if (new_name != "")
                        {
                            users[indexToModify].SetName(new_name);
                            Console.WriteLine("Modification successful!");
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "032":
                        Console.Write("New ID: ");
                        string new_id = Console.ReadLine();
                        if (new_id != "")
                        {
                            users[indexToModify].SetUserId(new_id);
                            Console.WriteLine("Modification successful!");
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "033":
                        Console.Write("New address: ");
                        string new_address = Console.ReadLine();
                        if (new_address != "")
                        {
                            users[indexToModify].SetAddress(new_address);
                            Console.WriteLine("Modification successful!");
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "034":
                        Console.Write("New phone number: ");
                        string new_phone = Console.ReadLine();
                        if (new_phone != "")
                        {
                            users[indexToModify].SetPhoneNumber(new_phone);
                            Console.WriteLine("Modification successful!");
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "035":
                        Console.Write("New email: ");
                        string new_email = Console.ReadLine();
                        if (new_email != "")
                        {
                            users[indexToModify].SetEmail(new_email);
                            Console.WriteLine("Modification successful!");
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "036":
                        Console.Write("New password: ");
                        string new_password = Console.ReadLine();
                        if (new_password != "")
                        {
                            users[indexToModify].SetPassword(new_password);
                            Console.WriteLine("Modification successful!");
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "037":
                        Console.Write("New permission: ");
                        string new_permission = Console.ReadLine();
                        if (new_permission == "admin" || new_permission == "reader" || new_permission == "librarian")
                        {
                            users[indexToModify].SetPermissions(new_permission);
                            Console.WriteLine("Modification successful!");
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "038":
                        if (users[indexToModify] is Administrator || users[indexToModify] is Librarian)
                        {
                            Console.Write("New position: ");
                            string new_position = Console.ReadLine();
                            if (new_position != "")
                            {
                                if (users[indexToModify] is Administrator)
                                {
                                    Administrator admin = users[indexToModify] as Administrator;
                                    admin.SetPosition(new_position);
                                    Console.WriteLine("Modification successful!");
                                }
                                else if (users[indexToModify] is Librarian)
                                {
                                    Librarian librarian = users[indexToModify] as Librarian;
                                    librarian.SetPosition(new_position);
                                    Console.WriteLine("Modification successful!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Modification unsuccessful!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "039":
                        if (users[indexToModify] is Reader)
                        {
                            Console.Write("Amount to add (membership fee): ");
                            double amount;
                            string input = Console.ReadLine();
                            if (Double.TryParse(input, out amount) && amount > 0.00)
                            {
                                Reader reader = users[indexToModify] as Reader;
                                reader.SetMembershipFeeToPay(Convert.ToDouble(amount));
                                Console.WriteLine("Modification successful!");
                            }
                            else
                            {
                                Console.WriteLine("Modification unsuccessful!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    case "040":
                        if (users[indexToModify] is Reader)
                        {
                            Console.Write("Amount to add (late fee): ");
                            double amount;
                            string input = Console.ReadLine();
                            if (Double.TryParse(input, out amount) && amount > 0.00)
                            {
                                Reader reader = users[indexToModify] as Reader;
                                reader.SetLateFeeToPay(Convert.ToDouble(amount));
                                Console.WriteLine("Modification successful!");
                            }
                            else
                            {
                                Console.WriteLine("Modification unsuccessful!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Modification unsuccessful!");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid operation! ");
                        UpdateUser();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid ID! ");
            }
        }


        public void UserCountByType()
        {
            int adminCount = 0, librarianCount = 0, readerCount = 0;

            foreach (User user in users)
            {
                switch (user.GetPermissions())
                {
                    case "admin":
                        adminCount++;
                        break;
                    case "librarian":
                        librarianCount++;
                        break;
                    case "reader":
                        readerCount++;
                        break;
                }
            }

            Console.WriteLine($"Admins: {adminCount}");
            Console.WriteLine($"Librarians: {librarianCount}");
            Console.WriteLine($"Readers: {readerCount}");
        }


        public void ListAllUsers()
        {
            foreach (User user in users)
            {
                Console.WriteLine($"Permissions: {user.GetPermissions()}");
                Console.WriteLine($"Name: {user.GetName()}");
                Console.WriteLine($"ID: {user.GetID()}");
                Console.WriteLine($"Address: {user.GetAddress()}");
                Console.WriteLine($"Phone number: {user.GetPhoneNumber()}");
                Console.WriteLine($"Email: {user.GetEmail()}");

                if (user is Librarian)
                {
                    Librarian librarian = user as Librarian;
                    Console.WriteLine($"Position: {librarian.GetPosition()}");
                }

                if (user is Administrator)
                {
                    Administrator admin = user as Administrator;
                    Console.WriteLine($"Position: {admin.GetPosition()}");
                }

                if (user is Reader)
                {
                    Reader reader = user as Reader;
                    Console.WriteLine($"Membership fee to pay: {reader.GetMembershipFeeToPay()}");
                    Console.WriteLine($"Late fee to pay: {reader.GetLateFeeToPay()}");
                }

                Console.WriteLine();
            }
        }

        public void ListReaders()
        {
            foreach (User user in users)
            {
                if (user.GetPermissions() == "reader")
                {
                    Reader reader = user as Reader;
                    Console.WriteLine($"Permissions: {reader.GetPermissions()}");
                    Console.WriteLine($"Name: {reader.GetName()}");
                    Console.WriteLine($"ID: {reader.GetID()}");
                    Console.WriteLine($"Address: {reader.GetAddress()}");
                    Console.WriteLine($"Phone number: {reader.GetPhoneNumber()}");
                    Console.WriteLine($"Email: {reader.GetEmail()}");
                    Console.WriteLine($"Membership fee to pay: {reader.GetMembershipFeeToPay()}");
                    Console.WriteLine($"Late fee to pay: {reader.GetLateFeeToPay()}");
                    Console.WriteLine();
                }
            }
        }

        public User GetObjectByIndex(int index)
        {
            if (index >= 0 && index < users.Count)
            {
                return users[index];
            }
            return null;
        }

        public User GetObjectByID(string ID)
        {
            foreach (User user in users)
            {
                if (user.GetID() == ID)
                    return user;
            }
            return null;
        }

        public string GetNameByIndex(int index)
        {
            if (index >= 0 && index < users.Count)
            {
                return users[index].GetName();
            }
            return null;
        }

        public string GetPasswordByIndex(int index)
        {
            if (index >= 0 && index < users.Count)
            {
                return users[index].GetPassword();
            }
            return null;
        }


        public string GetAuthorityByIndex(int index)
        {
            if (index >= 0 && index < users.Count)
            {
                return users[index].GetPermissions();
            }
            return null;
        }

        public string GetIDByIndex(int index)
        {
            if (index >= 0 && index < users.Count)
            {
                return users[index].GetID();
            }
            return null;
        }

        public int GetIndexByID(string id)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].GetID() == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetSize()
        {
            return users.Count;
        }

        public List<User> GetList() { return users; }
    }
}