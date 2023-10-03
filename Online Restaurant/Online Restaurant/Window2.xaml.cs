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

namespace Online_Restaurant
{
    //Register
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void btn_Check_Register(object sender, RoutedEventArgs e)
        {
            try
            {
                if (name.Text == "" || fname.Text == "") throw new Exception("نام و نام خانوادگی نباید خالی باشد");
                if (User.Email.One_Info(@"..\..\sign in\user.csv").Contains(email.Text)) throw new Exception("قبلا با این ایمیل ثبت نام صورت گرفته است");
                CheckUserInfo check = new CheckUserInfo(number.Text, email.Text, ID.Text, password.Text, Rpassword.Text, User_Admin.user);
                AddUser(name.Text, fname.Text, number.Text, email.Text, ID.Text, password.Text);//register
                this.Visibility = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddUser(string name, string fname, string number, string email, string ID, string password)//add to database
        {
            var data = File.ReadAllText(@"..\..\sign in\user.csv");
            StreamWriter users = new StreamWriter(@"..\..\sign in\user.csv");
            users.Write(data);
            users.Write($"{name},{fname},{number},{email},{ID},{password},{0}");
            users.Close();
        }
    }
}