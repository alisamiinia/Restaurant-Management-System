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
    /// Interaction logic for foods.xaml
    /// </summary>
    public partial class foods : Window
    {
        string ImagePath;
        List<FoodData> all;
        List<string> names;
        public foods()
        {
            names = new List<string>();
            all = File.ReadAllLines(@"../../food/foods.csv").Skip(1).Select(line => new FoodData(line)).ToList();
            foreach (FoodData x in all) names.Add(x.Name);
            // = names.ToArray();
            InitializeComponent();
            food.ItemsSource = names;
        }

        private void Initialize()
        {
            Sandwich.IsChecked = false;
            Pizza.IsChecked = false;
            Hamburger.IsChecked = false;
            Kentucky.IsChecked = false;
            Chips.IsChecked = false;
            Beverages.IsChecked = false;
            int index = names.IndexOf(food.SelectedItem.ToString());
            name.Text = all[index].Name;
            switch ((int)all[index].Type)
            {
                case 0:
                    Sandwich.IsChecked = true;
                    break;
                case 1:
                    Pizza.IsChecked = true;
                    break;
                case 2:
                    Hamburger.IsChecked = true;
                    break;
                case 3:
                    Chips.IsChecked = true;
                    break;
                case 4:
                    Kentucky.IsChecked = true;
                    break;
                case 5:
                    Beverages.IsChecked = true;
                    break;
            }
            price.Text = all[index].Price;
            number.Text = all[index].Number;
            SD.Text = all[index].SD;
            Gd.Text = all[index].GD;
            calendar.SelectedDate = DateTime.Parse(all[index].Date);
        }
        private void Btn_select_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = names.IndexOf(food.SelectedItem.ToString());
                all[index].Name = name.Text;
                all[index].Type = WitchCheckBox();
                all[index].Price = price.Text;
                all[index].Number = number.Text;
                all[index].SD = SD.Text;
                all[index].GD = Gd.Text;
                all[index].Date = calendar.SelectedDate.ToString();
                all[index].ImagePath = ImagePath;
                all.WriteFoodData();
                MessageBox.Show("تغییرات با موفقیت ذخیره شد", "عملیات موفق", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void Food_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Initialize();
        }
    }
}
