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
    /// UserBorrowBook.xaml 的交互逻辑
    /// </summary>
    public partial class UserBorrowBook : UserControl
    {
        public UserBorrowBook()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String bID = idBox.Text;
            if (bID.Length == 0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (idBox.Text.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            Reader reader = new Reader();
            Book book = new Book();
            reader.ID = User.UserID;
            book.ID = bID;
            if(DatabaseCon.SelectSingleReader(ref reader)&&DatabaseCon.SelectSingleBook(ref book))
            {
                if(DatabaseCon.ReaderBorrowBook(ref reader,ref book))
                {
                    DatabaseCon.UserRefreshData();
                    MessageBox.Show("借阅成功！");
                    return;
                }
                else
                {
                    MessageBox.Show("借阅失败！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("借阅失败！");
                return;
            }
        }
    }
}
