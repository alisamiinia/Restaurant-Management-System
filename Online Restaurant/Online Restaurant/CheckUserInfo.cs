using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Online_Restaurant
{
    class CheckUserInfo
    {
        public CheckUserInfo(string number, string email, string id, string password, string rpassword, User_Admin ua)
        {
            if (CheckNumber(number) == false) throw new Exception("شماره تلفن به درستی وارد نشده است");
            isValidEmail(email);//check the email address
            if (id.CorrectID() == false) throw new Exception("کد ملی به درستی وارد نشده است‌");
            if (ua == User_Admin.user) isGoodPassword(password);
            if (password != rpassword) throw new Exception("رمز عبور تطابق ندارد");
        }

        //method to check the number
        bool CheckNumber(string inputID)
        {
            string strRegex = @"^(\+989[0-9]{9})$|^(00989[0-9]{9})$|^(09[0-9]{9})$|^(9[0-9]{9})$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputID))
                return (true);
            else
                return (false);
        }
        // Method to check the Email ID 
        // do nothing when the email is valid and throw exception when invalid
        void isValidEmail(string inputEmail)
        {
            // This Pattern is use to verify the email 
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (!re.IsMatch(inputEmail)) throw new Exception("ایمیل به درستی وارد نشده است");
        }
        //check password : 1) It must contain at least a number 2) one upper case letter 3) 8 characters long.
        void isGoodPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            bool isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
            Console.WriteLine(isValidated);
            if (isValidated == false) throw new Exception("پسورد باید شامل یک حرف بزرگ و یک عدد و یک حرف کوچک باشد. حداقل اندازه ۸ کاراکتر است.");
        }
    }
}
