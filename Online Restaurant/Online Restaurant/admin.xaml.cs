using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;

namespace Online_Restaurant
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        string username;
        string password;
        public admin(string username,string password)
        {
            InitializeComponent();
            this.username = username;
            SetNewPassword(username);
            Save();
        }

        private void btn_foods(object sender, RoutedEventArgs e)
        {
            foods a = new foods();
            a.Show();
        }
        private void btn_check_orders(object sender, RoutedEventArgs e)
        {
            if(order.SelectedIndex == 0)
            {
                Order_Admin orders = new Order_Admin(true);
                orders.Show();
            }
            else
            {
                Order_Admin orders = new Order_Admin(false);
                orders.Show();
            }
        }
        private void btn_economic_info(object sender, RoutedEventArgs e)
        {

        }
        private void btn_menu(object sender, RoutedEventArgs e)
        {
            menu a = new menu();
            this.Visibility = Visibility.Visible;
            a.Show();
        }
        private void btn_username(object sender, RoutedEventArgs e)
        {
            check_info check = new check_info(username, password, OpenAdminProfile);
            check.Show();
        }
        void OpenAdminProfile(string username)
        {
            UserProfile admin = new UserProfile(username, "../../sign in/admin.csv", User_Admin.user, Select_Username_Password);
            admin.Show();
        }
        void Select_Username_Password(string username,List<UserData> all,int ProfileIndex)//check for good username and select the password for admin
        {
            if (!CheckUsername(username)) throw new Exception("باشد admin نام کاربری باید شامل عبارت");
            SetNewPassword(username);
            all[ProfileIndex].Password = password;
            all.WriteAdminData();

        }
        bool CheckUsername(string username)
        {
            if(username.Contains("admin"))
                return (true);
            else
                return (false);
        }
        void SetNewPassword(string username)
        {
            int NumOf1 = (int.Parse(User.Name.One_Person(this.username, "../../sign in/admin.csv")[(int)User.Login]) + 1) % 10;
            int NumOf0 = 0;
            char[] x = username.ToCharArray();
            foreach (char ch in x) if (ch == 'a' || ch == 'A' || ch == 'e' || ch == 'E' || ch == 'u' || ch == 'U' || ch == 'i' || ch == 'I' || ch == 'o' || ch == 'O') NumOf0++;
            password = "";
            for (int tedad1 = 0; tedad1 < NumOf1; tedad1++) password += "1";
            for (int tedad0 = 0; tedad0 < NumOf0; tedad0++) password += "0";
        }
        void Save()
        {
            int profile = 0;
            List<UserData> all = File.ReadAllLines("../../sign in/admin.csv").Skip(1).Select(line => new UserData(line)).ToList();
            foreach (UserData x in all) if (x.Email == username) profile = all.IndexOf(x);
            all[profile].Password = password;
            all.WriteAdminData();
        }
    }
}
