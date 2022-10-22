using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LibraryManageSystem
{
    public class Book
    {
        public string ID { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Press { get; set; }
        public int Year { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int totalNum { get; set; }
        public int curNum { get; set; }
    }
    public class Reader
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Depart { get; set; }
        public string Type { get; set; }
        public int Num { get; set; }
        public int Credit { get; set; }
    }
    public class TypeCategory
    { 
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class Borrow
    {
        public int ID { get; set; }
        public string reader_ID { get; set; }
        public string book_ID { get; set; }
        public string BorrowDate { get; set; }
        public string PlanDate { get; set; }
        public string AcutalDate { get; set; }
        public DateTime bDate { get; set; }
        public DateTime pDate { get; set; }
        public DateTime aDate { get; set; }
    }
    public static class User
    {
        public static string UserID { get; set; }
        public static string UserType { get; set; }
    }
    public class CardPrint:INotifyPropertyChanged
    {
        private string _Num;
        private string _Credit;
        public string num
        {
            set
            {
                _Num = value;
                if (PropertyChanged != null)//有改变
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("num"));
                }
            }
            get
            {
                return _Num;
            }
        }

        public string credit
        {
            set
            {
                _Credit = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("credit"));
                }
            }
            get
            {
                return _Credit;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class StatPrint : INotifyPropertyChanged
    {
        private string _total;
        private string _atv;
        private string _brw;
        private string _card;
        private string _tea;
        private string _stu;
        public string total
        {
            set
            {
                _total = value;
                if (PropertyChanged != null)//有改变
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("total"));
                }
            }
            get
            {
                return _total;
            }
        }
        public string atv
        {
            set
            {
                _atv = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("atv"));
                }
            }
            get
            {
                return _atv;
            }
        }
        public string brw
        {
            set
            {
                _brw = value;
                if (PropertyChanged != null)//有改变
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("brw"));
                }
            }
            get
            {
                return _brw;
            }
        }
        public string card
        {
            set
            {
                _card = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("card"));
                }
            }
            get
            {
                return _card;
            }
        }
        public string tea
        {
            set
            {
                _tea = value;
                if (PropertyChanged != null)//有改变
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("tea"));
                }
            }
            get
            {
                return _tea;
            }
        }
        public string stu
        {
            set
            {
                _stu = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("stu"));
                }
            }
            get
            {
                return _stu;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public static class Server
    {
        public static string IP = "127.0.0.1";
        public static string PORT = "3306";
        public static string USER = "library";
        public static string PASSWORD = "123456";
        public static string DATABASE = "library";
    }

}
