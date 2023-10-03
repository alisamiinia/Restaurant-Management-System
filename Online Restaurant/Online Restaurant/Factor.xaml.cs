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
    /// Interaction logic for Factor.xaml
    /// </summary>
    public partial class Factor : Window
    {
        int index;
        string Username;
        double totPrice;
        List<Order> orders;
        List<string> orderName;
        public Factor(string username, double TotPrice, List<Order> orders)
        {
            index = 0;
            this.orders = orders;
            totPrice = TotPrice;
            Username = username;
            InitializeComponent();
            foreach (var x in orders) orderName.Add(x.FoodName);
            food.ItemsSource = orderName;
            this.username.Text = Username;
            this.TotPrice.Text = totPrice.ToString();
        }

        private void Initialize()
        {
            username.Text = Username;
            TotPrice.Text = totPrice.ToString();
            Count.Text = orders[index].Count;
            Price.Text = orders[index].Price;
        }
        private void Order_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Food_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var x in orders) if (food.SelectedItem.ToString() == x.FoodName) index = orders.IndexOf(x);
            Initialize();
        }
    }
}
