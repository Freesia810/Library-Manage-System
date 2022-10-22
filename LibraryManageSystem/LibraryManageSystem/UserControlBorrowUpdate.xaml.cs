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
    /// UserControlBorrowUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBorrowUpdate : UserControl
    {
        public UserControlBorrowUpdate()
        {
            InitializeComponent();
        }

        private void isReturnBox_Click(object sender, RoutedEventArgs e)
        {
            if (isReturnBox.IsChecked == true)
            {
                aLabel.Visibility = aBox.Visibility = Visibility.Visible;
            }
            else
            {
                aLabel.Visibility = aBox.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String ID = idBox.Text;
            if (ID.Length == 0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (ID.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            Borrow borrow = new Borrow();
            borrow.ID = int.Parse(ID);
            if (DatabaseCon.SelectSingleBorrow(ref borrow))
            {
                readerLabel.Visibility = bookLabel.Visibility = bLabel.Visibility = pLabel.Visibility = Visibility.Visible;
                readerBox.Visibility = bookBox.Visibility = bBox.Visibility = pBox.Visibility = isReturnBox.Visibility = Visibility.Visible;
                submitButton.Visibility = Visibility.Visible;

                readerBox.Text = borrow.reader_ID;
                bookBox.Text = borrow.book_ID;
                bBox.SelectedDate = borrow.bDate;
                pBox.SelectedDate = borrow.pDate;

                if(borrow.aDate == DateTime.MaxValue)
                {
                    isReturnBox.IsChecked = false;
                    aLabel.Visibility = Visibility.Collapsed;
                    aBox.Visibility = Visibility.Collapsed;
                    aBox.SelectedDate = null;
                }
                else
                {
                    isReturnBox.IsChecked = true;
                    aLabel.Visibility = Visibility.Visible;
                    aBox.Visibility = Visibility.Visible;
                    aBox.SelectedDate = borrow.aDate;
                }

                return;
            }
            else
            {
                MessageBox.Show("查询失败，请检查编号是否正确！");
                return;
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            Borrow borrow = new Borrow();
            if (readerBox.Text.Length == 0 || bookBox.Text.Length == 0 || bBox.SelectedDate == null)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            if (bookBox.Text.Length > 60 || readerBox.Text.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            borrow.ID = int.Parse(idBox.Text);
            borrow.reader_ID = readerBox.Text;
            borrow.book_ID = bookBox.Text;
            borrow.bDate = (DateTime)bBox.SelectedDate;
            borrow.pDate = pBox.SelectedDate == null ? borrow.bDate.AddDays(30) : (DateTime)pBox.SelectedDate;
            if (borrow.pDate < borrow.bDate)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            if (isReturnBox.IsChecked == false)  //未归还
            {
                borrow.aDate = DateTime.MaxValue;
            }
            else
            {
                borrow.aDate = aBox.SelectedDate == null ? borrow.pDate : (DateTime)aBox.SelectedDate;
            }

            if (DatabaseCon.UpdateBorrow(ref borrow))
            {
                DatabaseCon.AdminRefreshData();
                readerLabel.Visibility = bookLabel.Visibility = bLabel.Visibility = pLabel.Visibility = Visibility.Collapsed;
                readerBox.Visibility = bookBox.Visibility = bBox.Visibility = pBox.Visibility = isReturnBox.Visibility = Visibility.Collapsed;
                aLabel.Visibility = aBox.Visibility = Visibility.Collapsed;
                submitButton.Visibility = Visibility.Collapsed;
                idBox.Text = null;
                MessageBox.Show("更改成功！");
                return;
            }
            else
            {
                MessageBox.Show("更改失败，请检查输入信息！");
                return;
            }
        }
    }
}
