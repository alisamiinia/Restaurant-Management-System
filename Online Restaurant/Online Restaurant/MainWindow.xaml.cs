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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Online_Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Register(object sender, RoutedEventArgs e)
        {
            Window2 register = new Window2();
            this.Visibility = Visibility.Visible;
            register.Show();
        }

        private void btn_singin(object sender, RoutedEventArgs e)
        {
            //find user data in database
            int profile = 0;
            List<UserData> all = File.ReadAllLines("../../sign in/user.csv").Skip(1).Select(line => new UserData(line)).ToList();
            foreach (UserData x in all) if (x.Email == username.Text) profile = all.IndexOf(x);
            ////
            if (profile == 0) MessageBox.Show("هیچ کاربری با این مشخصات یافت نشد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (all[profile].Password == password.Text)
                {
                    shop sing_in = new shop(username.Text);
                    this.Visibility = Visibility.Hidden;
                    sing_in.Show();
                    all[profile].login = (int.Parse(all[profile].login) + 1).ToString();
                    all.WriteUserData();
                }
                else MessageBox.Show("نام کاربری با پسورد همخوانی ندارند", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_admin_singin(object sender, RoutedEventArgs e)
        {
            admin_singin sing_in = new admin_singin();
            this.Visibility = Visibility.Visible;
            sing_in.Show();
        }
    }
}
