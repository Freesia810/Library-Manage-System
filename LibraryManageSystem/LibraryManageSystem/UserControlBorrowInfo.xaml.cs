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
    /// UserControlBorrowInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBorrowInfo : UserControl
    {
        static ObservableCollection<Borrow> borrows = new ObservableCollection<Borrow>();
        public UserControlBorrowInfo()
        {
            InitializeComponent();

            DatabaseCon.AdminRefreshData();
            DataGridBorrow.AutoGenerateColumns = false;
            DataGridBorrow.ItemsSource = borrows;
        }

        public static void RefreshData()
        {
            DatabaseCon.BorrowInfo(ref borrows);
        }
    }
}
