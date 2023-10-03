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
using Microsoft.Win32;

namespace Online_Restaurant
{
    public partial class AllFoods : Window
    {
        string Username;
        int FoodIndex;
        DateTime date;
        List<FoodData> foods;
        List<FoodData> DayFood;
        public AllFoods(DateTime? date, string username)
        {
            this.date = (DateTime)date;
            Username = username;
            DayFood = new List<FoodData>();
            foods = File.ReadAllLines(@"..\..\food\foods.csv").Skip(1).Select(line => new FoodData(line)).ToList();
            foreach (FoodData food in foods) if (DateTime.Parse(food.Date) == date) DayFood.Add(food);
            InitializeComponent();
            try
            {
                FoodIndex = 1;
                ShowFood(DayFood[0]);
            }
            catch
            {
                MessageBox.Show("غذایی برای این روز تعریف نشده است", "ناموجود", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Cart(object sender, RoutedEventArgs e)
        {
            Cart cart = new Cart(Username);
            cart.Show();
        }
        private void btn_next(object sender, RoutedEventArgs e)
        {
            FoodIndex++;
            try
            {
                ShowFood(DayFood[FoodIndex]);
            }
            catch
            {
                MessageBox.Show("صفحه دیگری وجود ندارد", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                FoodIndex--;
            }
        }
        private void btn_back(object sender, RoutedEventArgs e)
        {
            FoodIndex--;
            try
            {
                ShowFood(DayFood[FoodIndex]);
            }
            catch
            {
                MessageBox.Show("صفحه مورد نظر وجود ندارد", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                FoodIndex++;
            }
        }
        private void btn_add_cart(object sender, RoutedEventArgs e)
        {
            Order order = new Order(Username, Name.Text, Price.Text, 0, count.Text, date.ToString(), false, "");
            order.Add();
            MessageBox.Show("added!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ShowFood(FoodData food)
        {
            Type.Text = food.Type.ToString();
            Name.Text = food.Name;
            Price.Text = int.Parse(food.Price)*(1.24) + "تومان";
            Number.Text = food.Number;
            SD.Text = food.SD;
            GD.Text = food.GD;
            if (food.ImagePath.Length != 0)
            {
                Uri fileUri = new Uri(food.ImagePath);
                FoodImage.Source = new BitmapImage(fileUri); //change image
            }
            else
            {
                var uriSource = new Uri("/img/NoImage.png", UriKind.Relative);
                FoodImage.Source = new BitmapImage(uriSource);
            }
        }
    }
}