using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManageSystem
{
    /// <summary>
    /// UserControlBorrowAdd.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBorrowAdd : UserControl
    {
        public UserControlBorrowAdd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Borrow borrow = new Borrow();
            if (bIDBox.Text.Length == 0 || rIDBox.Text.Length == 0 || bBox.SelectedDate == null)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            if (bIDBox.Text.Length > 60 || rIDBox.Text.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            borrow.reader_ID = rIDBox.Text;
            borrow.book_ID = bIDBox.Text;
            borrow.bDate = (DateTime)bBox.SelectedDate;
            borrow.pDate = pBox.SelectedDate == null?borrow.bDate.AddDays(30):(DateTime)pBox.SelectedDate;
            if(borrow.pDate < borrow.bDate)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            if(isReturnBox.IsChecked == false)  //未归还
            {
                borrow.aDate = DateTime.MaxValue;
            }
            else
            {
                borrow.aDate = aBox.SelectedDate == null ? borrow.pDate : (DateTime)aBox.SelectedDate;
            }

            if (DatabaseCon.AddBorrow(ref borrow))
            {
                DatabaseCon.AdminRefreshData();
                rIDBox.Text = bIDBox.Text = null;
                bBox.SelectedDate = pBox.SelectedDate = aBox.SelectedDate = null;
                MessageBox.Show("添加成功！");
                return;
            }
            else
            {
                MessageBox.Show("添加失败，请检查输入信息！");
                return;
            }

        }

        private void isReturnBox_Click(object sender, RoutedEventArgs e)
        {
            if(isReturnBox.IsChecked==true)
            {
                aLabel.Visibility = aBox.Visibility = Visibility.Visible;
            }
            else
            {
                aLabel.Visibility = aBox.Visibility = Visibility.Collapsed;
            }
        }

        private void multiBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "图书管理系统借阅记录|*.lmbr";
            openFile.RestoreDirectory = true;
            string filePath;
            if ((bool)openFile.ShowDialog())
            {
                filePath = openFile.FileName;
                int resRow = DatabaseCon.MultiInsert(filePath, "borrow");

                if (resRow == -1)
                {
                    MessageBox.Show("批量导入错误");
                    return;
                }
                else
                {
                    MessageBox.Show("影响记录数：" + resRow.ToString());
                    DatabaseCon.AdminRefreshData();
                    return;
                }
            }
        }
    }
}
