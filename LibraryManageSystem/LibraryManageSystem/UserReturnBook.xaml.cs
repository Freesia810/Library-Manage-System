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
    /// UserReturnBook.xaml 的交互逻辑
    /// </summary>
    public partial class UserReturnBook : UserControl
    {
        public UserReturnBook()
        {
            InitializeComponent();
        }
        void skip()
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String borrowID = idBox.Text;
            if (idBox.Text.Length == 0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (idBox.Text.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }

            Borrow borrow = new Borrow();
            borrow.ID = int.Parse(borrowID);
            skip();
            if(DatabaseCon.SelectSingleBorrow(ref borrow))
            {
                if(DatabaseCon.ReaderReturnBook(ref borrow))
                {
                    DatabaseCon.UserRefreshData();
                    MessageBox.Show("归还成功！");
                    return;
                }
                else
                {
                    MessageBox.Show("归还失败！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("归还失败！");
                return;
            }
        }
    }
}
