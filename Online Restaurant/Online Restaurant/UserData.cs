using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Restaurant
{
    public enum User{ Name, LastName, Number, Email, ID, Password, Login}
    public enum User_Admin { user, admin}
    public class UserData
    {
        public string Name;
        public string LastName;
        public string Number;
        public string Email;
        public string ID;
        public string Password;
        public string login;
        public string ProfileImage;
        public UserData(string line)
        {
            var toks = line.Split(',');
            Name = toks[0];
            LastName = toks[1];
            Number = toks[2];
            Email = toks[3];
            ID = toks[4];
            Password = toks[5];
            login = toks[6];
            if (toks.Length == 7) ProfileImage = "";
            else ProfileImage = toks[7];
        }
    }
}
