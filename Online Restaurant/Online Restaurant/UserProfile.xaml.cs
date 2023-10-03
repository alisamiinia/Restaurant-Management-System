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
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        public delegate void Check_for_admin(string username,List<UserData> all,int ProfileIndex);
        Check_for_admin CheckInfo;
        User_Admin ua;
        string ImagePath;
        string UserName;
        int profile;
        List<UserData> all;
        public UserProfile(string username, string address, User_Admin ua ,Check_for_admin CheckInfo)
        {
            this.ua = ua;
            UserName = username;
            this.CheckInfo = CheckInfo;
            all = File.ReadAllLines(address).Skip(1).Select(line => new UserData(line)).ToList();
            if(ua == User_Admin.user) foreach (UserData x in all) if (x.Email == username) profile = all.IndexOf(x);
            else foreach (UserData y in all) if (y.Name == username) profile = all.IndexOf(y);
            //profile = User.Email.One_Person(username, "../../sign in/user.csv");
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            name.Text = all[profile].Name;
            Fname.Text = all[profile].LastName;
            email.Text = all[profile].Email;
            password.Text = all[profile].Password;
            Rpassword.Text = all[profile].Password;
            ID.Text = all[profile].ID;
            number.Text = all[profile].Number;
            if (all[profile].ProfileImage.Length != 0)//image path index in database
            {
                Uri fileUri = new Uri(all[profile].ProfileImage);
                image.Source = new BitmapImage(fileUri); //change image
            }
            else
            {
                var uriSource = new Uri("/img/profile.png", UriKind.Relative);
                image.Source = new BitmapImage(uriSource);
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckUserInfo check = new CheckUserInfo(number.Text, email.Text, ID.Text, password.Text, Rpassword.Text , ua);
                all[profile].Name = name.Text;
                all[profile].LastName = Fname.Text;
                all[profile].Number = number.Text;
                all[profile].ID = ID.Text;
                all[profile].ProfileImage = ImagePath;
                all[profile].Password = password.Text;
                CheckInfo(name.Text,all,profile);//check and add to database
                MessageBox.Show("موفق", "موفق", MessageBoxButton.OK, MessageBoxImage.Information);
                Visibility = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProfileImage_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Files (*.png)|*.png";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                ImagePath = dlg.FileName;
            }
        }
    }
}
