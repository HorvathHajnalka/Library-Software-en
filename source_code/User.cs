using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public abstract class User
    {
        protected string name;
        protected string userId;
        protected string address;
        protected string phoneNumber;
        protected string email;
        protected string password;
        protected string permissions;

        public User(string _name, string _userId, string _address, string _phoneNumber, string _email, string _password, string _permissions)
        {
            name = _name;
            userId = _userId;
            address = _address;
            phoneNumber = _phoneNumber;
            email = _email;
            password = _password;
            permissions = _permissions;
        }

        public string GetName() { return name; }
        public string GetPassword() { return password; }
        public string GetPermissions() { return permissions; }
        public string GetAddress() { return address; }
        public string GetPhoneNumber() { return phoneNumber; }
        public string GetEmail() { return email; }
        public string GetID() { return userId; }

        public void SetName(string _name) { name = _name; }
        public void SetUserId(string _userId) { userId = _userId; }
        public void SetAddress(string _address) { address = _address; }
        public void SetPhoneNumber(string _phoneNumber) { phoneNumber = _phoneNumber; }
        public void SetEmail(string _email) { email = _email; }
        public void SetPassword(string _password) { password = _password; }
        public void SetPermissions(string _permissions) { permissions = _permissions; }
    }
}
