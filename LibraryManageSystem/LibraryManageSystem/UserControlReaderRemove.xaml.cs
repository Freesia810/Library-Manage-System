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
    /// UserControlReaderRemove.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlReaderRemove : UserControl
    {
        public UserControlReaderRemove()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String rID = idBox.Text;
            if (rID.Length == 0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (rID.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            if (DatabaseCon.RemoveReader(rID))
            {
                DatabaseCon.AdminRefreshData();
                MessageBox.Show("删除完成！");
                idBox.Text = null;
                return;
            }
            else
            {
                MessageBox.Show("删除失败，请检查编号是否正确！");
                return;
            }
        }
    }
}
