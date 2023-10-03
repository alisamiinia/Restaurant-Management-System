using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Online_Restaurant
{
    public enum Food { Sandwich, Pizza, Hamburger, Kentucky, Chips, Beverages }
    public enum FoodBase { Name, Type, Price, Number, SD, GD, Date, ImagePath}
    public class FoodData
    {
        public string Name;
        public Food Type;
        public string Price;
        public string Number;
        public string SD;
        public string GD;
        public string Date;
        public string ImagePath;
        public FoodData(string line)
        {
            var food = line.Split(',');
            Name = food[0];
            Type = (Food)int.Parse(food[1]);
            Price = food[2];
            Number = food[3];
            SD = food[4];
            GD = food[5];
            Date = food[6];
            if (food.Length == 7) ImagePath = "";
            else ImagePath = food[7];
        }
        public FoodData(string name, Food type, string price, string number, string SD, string GD, string date, string Image)
        {
            Name = name;
            Type = type;
            Price = price;
            Number = number;
            this.SD = SD;
            this.GD = GD;
            Date = date;
            ImagePath = Image;
        }
        public void Add()
        {
            var data = File.ReadAllText(@"..\..\food\foods.csv");
            StreamWriter add = new StreamWriter(@"..\..\food\foods.csv");
            add.Write(data);
            add.WriteLine($"{Name},{(int)Type},{Price},{Number},{SD},{GD},{Date},{ImagePath}");
            add.Close();
        }
    }
}
