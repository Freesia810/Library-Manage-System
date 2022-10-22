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
using System.Windows.Shapes;

namespace LibraryManageSystem
{
    /// <summary>
    /// ServerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ServerWindow : Window
    {
        public ServerWindow()
        {
            InitializeComponent();
            ipBox.Text = Server.IP;
            ptBox.Text = Server.PORT;
            userBox.Text = Server.USER;
            pw.Password = Server.PASSWORD;
            dbBox.Text = Server.DATABASE;
        }

        private void Button_Click(object sender, RoutedEventArgs e)  //确认
        {
            Server.IP = ipBox.Text;
            Server.PORT = ptBox.Text;
            Server.USER = userBox.Text;
            Server.PASSWORD = pw.Password;
            Server.DATABASE = dbBox.Text;
            DatabaseCon.serverPath = "server=" + Server.IP + ";port=" + Server.PORT + ";user=" + Server.USER + ";password=" + Server.PASSWORD + "; database=" + Server.DATABASE + ";";
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //默认设置
        {
            ipBox.Text = "127.0.0.1";
            ptBox.Text = "3306";
            userBox.Text = "library";
            pw.Password = "123456";
            dbBox.Text = "library";
        }
    }
}
