using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Administrator : User
    {
        private string position;

        public Administrator(string _position, string _name, string _userId,
            string _address, string _phoneNumber, string _email, string _password, string _permissions) :
            base(_name, _userId, _address, _phoneNumber, _email, _password, _permissions)
        {            
            position = _position;
        }

        public string GetPosition() { return position; }

        public void SetPosition(string _position) { position = _position; }

        public string GetObjectAsString()
        {
            return $"{name};{userId};{address};{phoneNumber};{email};{password};{permissions};{position}";
        }
    }
}
