using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Online_Restaurant
{
    //public enum OrderFood { Sandwich, Pizza, Hamburger, Kentucky, Chips, Beverages }
    public class Order
    {
        public string UserName;
        public string FoodName;
        public string Price;
        public string Count;
        public int DiscountPercent;
        public string Date;
        public bool Registered;
        public string RegisterDate;
        public Order(string line)
        {
            var order = line.Split(',');
            UserName = order[0];
            FoodName = order[1];
            Price = order[2];
            DiscountPercent = int.Parse(order[3]);
            Count = order[4];
            Date = order[5];
            Registered = bool.Parse(order[6]);
            if (order.Length == 7) RegisterDate = "";
            else RegisterDate = order[7];
        }
        public Order(string username, string foodname, string price, int discount, string count, string date, bool registered, string registerdate)
        {
            UserName = username;
            FoodName = foodname;
            Price = price;
            DiscountPercent = discount;
            Count = count;
            Date = date;
            Registered = registered;
            RegisterDate = registerdate;
        }
        public void Add()
        {
            var data = File.ReadAllText(@"..\..\orders\orders.csv");
            StreamWriter add = new StreamWriter(@"..\..\orders\orders.csv");
            add.Write(data);
            add.WriteLine($"{UserName},{FoodName},{Price},{DiscountPercent},{Count},{Date},{Registered},{RegisterDate}");
            add.Close();
        }
    }
}
