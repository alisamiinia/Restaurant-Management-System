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
    /// Interaction logic for shop.xaml
    /// </summary>
    public partial class shop : Window
    {
        static int background;
        string Username;
        List<string> profile;
        public shop(string username)
        {
            InitializeComponent();
            background = 0;
            Username = username;
            Initialize();
        }
        private void Initialize()
        {
            profile = User.Email.One_Person(Username, "../../sign in/user.csv");
            email.Text = profile[(int)User.Email];
            number.Text = profile[(int)User.Number];
            if (profile.Count == 8)
            {
                if (profile[7].Length != 0)//image path index in database
                {
                    Uri fileUri = new Uri(profile[7]);
                    profileimg.Source = new BitmapImage(fileUri); //change image
                }
                else
                {
                    var uriSource = new Uri("/img/profile.png", UriKind.Relative);
                    profileimg.Source = new BitmapImage(uriSource);
                }
            }
        }
        private void save_command(object sender, RoutedEventArgs e)
        {
            string temp = File.ReadAllText(@"..\..\commands\commands.txt");
            StreamWriter add = new StreamWriter(@"..\..\commands\commands.txt");
            add.WriteLine(command.Text);
            add.Close();
            MessageBox.Show("نظر شما با موفقیت ثبت گردید", "موفق", MessageBoxButton.OK, MessageBoxImage.Information);
            command.Text = null;
        }
        private void btn_Cart(object sender, RoutedEventArgs e)
        {
            Cart cart = new Cart(Username);
            this.Visibility = Visibility.Visible;
            cart.Show();
        }
        private void Background_Click(object sender, RoutedEventArgs e)
        {
            if (background % 4 == 0)
            {
                control.Background = new LinearGradientBrush(Colors.LightBlue, Colors.White, 45);
            }
            else if (background % 4 == 1)
            {
                control.Background = new LinearGradientBrush(Colors.DarkGray, Colors.White, 45);
            }
            else if (background % 4 == 2)
            {
                control.Background = new LinearGradientBrush(Colors.SkyBlue, Colors.LightYellow, 90);
            }
            else
            {
                control.Background = Brushes.White;
            }
            background++;
        }

        private void btn_show_foods(object sender, RoutedEventArgs e)
        {
            if (calendar1.SelectedDate < DateTime.Today) MessageBox.Show("شما نمیتوانید این روز را انتخاب کنید", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                AllFoods day = new AllFoods(calendar1.SelectedDate ,Username);
                day.Show();
            }
            //command.Text = calendar1.SelectedDate.ToString();
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            check_info check = new check_info(Username, User.Email.One_Person(Username, "../../sign in/user.csv")[(int)User.Password], OpenUserProfile);
            check.Show();
        }
        void OpenUserProfile(string username)
        {
            UserProfile user = new UserProfile(username, "../../sign in/user.csv", User_Admin.user, (email,all,index) => { all.WriteUserData(); });//delegate is just for admin!
            user.Show();
        }
    }
}