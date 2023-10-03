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
    /// <summary>
    /// Interaction logic for admin_singin.xaml
    /// </summary>
    public partial class admin_singin : Window
    {
        public admin_singin()
        {
            InitializeComponent();
        }

        private void btn_admin(object sender, RoutedEventArgs e)
        {
            if (check_username(username.Text) && check_password(username.Text, password.Text))
            {                //open window
                admin singin = new admin(username.Text, password.Text);
                this.Visibility = Visibility.Hidden;
                singin.Show();

                //find user data in database
                int profile = 0;
                List<UserData> all = File.ReadAllLines("../../sign in/admin.csv").Skip(1).Select(line => new UserData(line)).ToList();
                foreach (UserData x in all) if (x.Name == username.Text) profile = all.IndexOf(x);
                ///////////
                all[profile].login = (int.Parse(all[profile].login) + 1).ToString();
                all.WriteAdminData();
            }
            else
            {
                MessageBox.Show("چنین ادمینی وجود ندارد", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool check_username(string username)
        {
            return User.Name.One_Info(@"..\..\sign in\admin.csv").Contains(username);
        }
        private bool check_password(string username,string password)
        {
            return password == User.Name.One_Person(username, @"..\..\sign in\admin.csv")[(int)User.Password];
        }
    }
}
