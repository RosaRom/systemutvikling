using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class User
    {
        private int userID;
        private string surname;
        private string firstname;
        private string username;
        private int phone;
        private string mail;

        public User(int _userID, string _surname, string _firstname, string _username, int _phone, string _mail)
        {
            userID = _userID;
            surname = _surname;
            firstname = _firstname;
            username = _username;
            phone = _phone;
            mail = _mail;
        }
        public User()
        { }

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public int Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        public List<string> toString()
        {
            List<string> list = new List<string>();
            string item = firstname + surname + phone + mail;
            list.Add(item);
            return list;
        }

    }
}
