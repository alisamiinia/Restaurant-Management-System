using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Online_Restaurant
{
    public static class Extention
    {
        public static bool CorrectID(this string stri)
        {
            int[] str = new int[10];
            if (str.Length != 10) return false;
            for(int i = 0; i < 10; i++)
            {
                str[i] = int.Parse(stri[i].ToString());
            }
            int a = str[9];
            int b = str[0] * 10 + str[1] * 9 + str[2] * 8 + str[3] * 7 + str[4] * 6 + str[5] * 5 + str[6] * 4 + str[7] * 3 + str[8] * 2;
            int c = b % 11;
            if (a == str[0] && a == str[1] && a == str[2] && a == str[3] && a == str[4] && a == str[5] && a == str[6] && a == str[7] && a == str[8])
            {
                return false;
            }
            else
            {
                if (c == 0 && a == c)
                {
                    return true;
                }
                else if (c == 1 && a == 1)
                {
                    return true;
                }
                else if (c > 1 && a == 1)
                {
                    return true;
                }
                else if (c > 1 && a == 11 - c)
                {
                    return true;
                }
                else if (a == 0) return true;
                else return false;
            }
        }
        public static void WriteFoodData(this List<FoodData> All)
        {
            StreamReader file = new StreamReader(@"..\..\food\foods.csv");
            string firstline = file.ReadLine();
            file.Close();
            StreamWriter food = new StreamWriter(@"..\..\food\foods.csv");
            food.WriteLine(firstline);
            foreach (FoodData x in All) food.WriteLine(x.Name + "," + (int)x.Type + "," + x.Price + "," + x.Number + "," + x.SD + "," + x.GD + "," + x.Date + "," + x.ImagePath);
            food.Close();
        }
        public static void WriteUserData(this List<UserData> All)
        {
            StreamReader file = new StreamReader(@"..\..\sign in\user.csv");
            string firstline = file.ReadLine();
            file.Close();
            StreamWriter user = new StreamWriter(@"..\..\sign in\user.csv");
            user.WriteLine(firstline);
            foreach (UserData x in All) user.WriteLine(x.Name + "," + x.LastName + "," + x.Number + "," + x.Email + "," + x.ID + "," + x.Password + "," + x.login + "," + x.ProfileImage);
            user.Close();
        }
        public static void WriteAdminData(this List<UserData> All)// like WriteUserData func
        {
            StreamReader file = new StreamReader(@"..\..\sign in\admin.csv");
            string firstline = file.ReadLine();
            file.Close();
            StreamWriter admin = new StreamWriter(@"..\..\sign in\admin.csv");
            admin.WriteLine(firstline);
            foreach (UserData x in All) admin.WriteLine(x.Name + "," + x.LastName + "," + x.Number + "," + x.Email + "," + x.ID + "," + x.Password + "," + x.login);
            admin.Close();
        }
        public static void WriteOrderData(this List<Order> All)// like WriteUserData func
        {
            StreamReader file = new StreamReader(@"..\..\orders\orders.csv");
            string firstline = file.ReadLine();
            file.Close();
            StreamWriter order = new StreamWriter(@"..\..\orders\orders.csv");
            order.WriteLine(firstline);
            foreach (var x in All) order.WriteLine(x.UserName + "," + x.FoodName + "," + x.Price + "," + x.DiscountPercent + "," + x.Count + "," + x.Date + "," + x.Registered + "," + x.RegisterDate);
            order.Close();
        }
        public static List<string> One_Info(this User column,string address)//get all data in same column in user data!
        {
            List<string> data = File.ReadAllLines(address).Skip(1).Select(line => line.Split(',')[(int)column]).ToList();
            return data;
        }
        public static List<string> One_Person(this User e, string info, string FileAddress)
        {
            int row = e.One_Info(FileAddress).IndexOf(info);
            List<string> data = File.ReadAllLines(FileAddress).ToList();
            data = data[1 + row].Split(',').ToList();
            return data;
        }
        public static List<string> One_Food(this FoodBase e, string info)
        {
            int row = ((User)((int)e)).One_Info("../../food/foods.csv").IndexOf(info);
            List<string> data = File.ReadAllLines("../../food/foods.csv").ToList();
            data = data[1 + row].Split(',').ToList();
            return data;
        }
        public static List<string> One_Order(this string foodname)
        {
            int row = User.LastName.One_Info("../../orders/orders.csv").IndexOf(foodname);
            List<string> data = File.ReadAllLines("../../orders/orders.csv").ToList();
            data = data[1 + row].Split(',').ToList();
            return data;
        }
    }
}
