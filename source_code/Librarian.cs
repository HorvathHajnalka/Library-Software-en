using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Librarian : User
    {
        private string position;

        public Librarian(string _position, string _name, string _userId, string _address, string _phoneNumber, string _email, string _password, string _permission)
            : base(_name, _userId, _address, _phoneNumber, _email, _password, _permission)
        {
            position = _position;
        }

        public string GetObjectAsString()
        {
            return $"{name};{userId};{address};{phoneNumber};{email};{password};{permission};{position}";
        }

        public string GetPosition() { return position; }

        public void SetPosition(string _position) { position = _position; }
    }
}
