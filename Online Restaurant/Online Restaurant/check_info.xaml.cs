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

namespace Online_Restaurant
{
    /// <summary>
    /// Interaction logic for check_info.xaml
    /// </summary>
    public partial class check_info : Window
    {
        public delegate void OpenWindow(string username);//تعریف دلگیت برای جلوگیری از تکرار
        string Username;
        string Password;
        OpenWindow openWindow;
        public check_info(string username, string password, OpenWindow openWindow)
        {
            InitializeComponent();
            this.openWindow = openWindow;
            Username = username;
            Password = password;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(username.Text == Username && password.Text == Password)
            {
                openWindow(Username);
            }
            else if(username.Text != Username) MessageBox.Show("نام کاربری به درستی وارد نشده است", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            else MessageBox.Show("نام کاربری با رمز عبور تطابق ندارند", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
