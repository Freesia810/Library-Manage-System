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
    /// UserControlBookRemove.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBookRemove : UserControl
    {
        public UserControlBookRemove()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String bID = idBox.Text;
            if(bID.Length == 0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (idBox.Text.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            if (DatabaseCon.RemoveBook(bID))
            {
                DatabaseCon.AdminRefreshData();
                MessageBox.Show("下架完成！");
                idBox.Text = null;
                return;
            }
            else
            {
                MessageBox.Show("下架失败，请检查编号是否正确！");
                return;
            }
        }
    }
}
