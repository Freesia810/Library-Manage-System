using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManageSystem
{
    /// <summary>
    /// UserControlBookInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBookInfo : UserControl
    {
        static ObservableCollection<Book> books = new ObservableCollection<Book>();
        public UserControlBookInfo()
        {
            InitializeComponent();

            DatabaseCon.AdminRefreshData();
            DataGridBook.AutoGenerateColumns = false;
            DataGridBook.ItemsSource = books;
        }

        public static void RefreshData()
        {
            DatabaseCon.BookInfo(ref books);
        }
    }
}
