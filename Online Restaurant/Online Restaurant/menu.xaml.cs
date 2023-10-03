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
    /// Interaction logic for menu.xaml
    /// </summary>
    public partial class menu : Window
    {
        string ImagePath;
        public menu()
        {
            ImagePath = null;
            InitializeComponent();
            MenuCalendar.SelectedDate = DateTime.Today;
        }
        
        private void btn_AddMenu(object sender, RoutedEventArgs e)
        {
            FoodData add = new FoodData(FoodName.Text, WitchCheckBox(), FoodPrice.Text, FoodCount.Text,  FoodSInfo.Text, FoodPInfo.Text, MenuCalendar.SelectedDate.ToString(), ImagePath);
            add.Add();
        }
        private Food WitchCheckBox()
        {
            int checkboxes = 0;
            Food food;
            if (Sandwich.IsChecked == true)
            {
                food = Food.Sandwich;
                checkboxes++;
            }
            else if (Hamburger.IsChecked == true)
            {
                food = Food.Hamburger;
                checkboxes++;
            }
            else if (Kentucky.IsChecked == true)
            {
                food = Food.Kentucky;
                checkboxes++;
            }
            else if (Chips.IsChecked == true)
            {
                food = Food.Chips;
                checkboxes++;
            }
            else if (Pizza.IsChecked == true)
            {
                food = Food.Pizza;
                checkboxes++;
            }
            else if (Beverages.IsChecked == true)
            {
                food = Food.Beverages;
                checkboxes++;
            }
            else throw new Exception("نوع غذا را انتخاب کنید");
            if (checkboxes > 1) throw new Exception("نوع غذا بیشتر از یک مورد نمی تواند باشد");
            return food;
        }

        private void SetAddress(object sender, RoutedEventArgs e)
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
