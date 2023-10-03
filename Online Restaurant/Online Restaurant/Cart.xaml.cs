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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        double TotPrice;
        string Username;
        int FoodIndex;
        List<Order> cart;
        List<Order> all;
        public Cart(string username)
        {
            Username = username;
            FoodIndex = 0;
            all = File.ReadAllLines("../../orders/orders.csv").Skip(1).Select(line => new Order(line)).ToList();
            cart = File.ReadAllLines("../../orders/orders.csv").Skip(1).Select(line => new Order(line)).ToList();
            foreach (Order x in cart) if (x.UserName != Username) cart.Remove(x);
            InitializeComponent();
            ShowFood();
            sumprice();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            all.Remove(cart[FoodIndex]);
            all.WriteOrderData();
            MessageBox.Show("deleted!", "deleting", MessageBoxButton.OK, MessageBoxImage.Information);
            if (cart.Count == 0) this.Visibility = Visibility.Hidden;
            else if (FoodIndex != 0) FoodIndex--;
            ShowFood();
            sumprice();
        }

        private void Delete_all_Click(object sender, RoutedEventArgs e)
        {
            foreach (var x in cart) all.Remove(x);
            all.WriteOrderData();
            this.Visibility = Visibility.Hidden;
            MessageBox.Show("deleted!", "deleting", MessageBoxButton.OK, MessageBoxImage.Information);
            sumprice();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            foreach (var x in cart) if (x.Registered) cart.Remove(x);
            Factor factor = new Factor(Username, TotPrice, cart);
            factor.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (FoodIndex == 0) MessageBox.Show("صفحه ای وجود ندارد", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                FoodIndex--;
                ShowFood();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (FoodIndex == cart.Count - 1) MessageBox.Show("صفحه ای وجود ندارد", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                FoodIndex++;
                ShowFood();
            }
        }

        private void ShowFood()
        {
            try
            {
                List<string> food = FoodBase.Name.One_Food(cart[FoodIndex].FoodName);
                username.Text = cart[FoodIndex].UserName;
                date.Text = cart[FoodIndex].Date;
                count.Text = cart[FoodIndex].Count;
                name.Text = food[(int)FoodBase.Name];
                type.Text = food[(int)FoodBase.Type];
                price.Text = (int.Parse(food[(int)FoodBase.Price]) * 1.24).ToString();
                number.Text = food[(int)FoodBase.Number];
                SumPrice.Text = TotPrice.ToString();
                if (food[(int)FoodBase.ImagePath].Length != 0)
                {
                    Uri fileUri = new Uri(food[(int)FoodBase.ImagePath]);
                    FoodImage.Source = new BitmapImage(fileUri); //change image
                }
                else
                {
                    var uriSource = new Uri("/img/NoImage.png", UriKind.Relative);
                    FoodImage.Source = new BitmapImage(uriSource);
                }
            }
            catch
            {
                MessageBox.Show("خطا.ممکن است موجودی غذا کمتر از تعداد درخواستی شما باشد یا غذای مورد نظر ناموجود باشد", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private void ChangeCount_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(number.Text) < int.Parse(count.Text)) MessageBox.Show("خطا.ممکن است موجودی غذا کمتر از تعداد درخواستی شما باشد یا غذای مورد نظر ناموجود باشد", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                all[all.IndexOf(cart[FoodIndex])].Count = count.Text;
                edit();
            }
            MessageBox.Show("changed!", "changing", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void sumprice()
        {
            double Sum = 0;
            foreach (var x in cart) Sum += (int.Parse(x.Count)) * (int.Parse(FoodBase.Name.One_Food(x.FoodName)[(int)FoodBase.Price])) * 1.24;
            TotPrice = Sum;
        }
        
        private void edit()
        {
            all.WriteOrderData();
        }
    }
}
