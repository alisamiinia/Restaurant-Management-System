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
    /// Interaction logic for Order_Admin.xaml
    /// </summary>
    public partial class Order_Admin : Window
    {
        List<Order> all;
        List<Order> cart;
        public Order_Admin(bool Registered)
        {
            cart = new List<Order>();
            all = File.ReadAllLines("../../orders/orders.csv").Skip(1).Select(line => new Order(line)).ToList();
            foreach (var x in all) if (x.Registered != Registered) all.Remove(x);
            InitializeComponent();
            User_Name.ItemsSource = User.Name.One_Info("../../orders/orders.csv");
            foodname.ItemsSource = "";
        }

        private void User_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> cartName = new List<string>();
            foreach (var x in all) if (x.UserName == User_Name.SelectedItem.ToString())
                {
                    cart.Add(x);
                    cartName.Add(x.FoodName);
                }
            foodname.ItemsSource = cartName;
        }

        private void Foodname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;
            foreach (var x in cart) if (x.FoodName == foodname.SelectedItem.ToString()) index = cart.IndexOf(x);
            Price.Text = cart[index].Price;
            count.Text = cart[index].Count;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
