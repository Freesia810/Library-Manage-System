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
    /// UpdatePW.xaml 的交互逻辑
    /// </summary>
    public partial class UpdatePW : Window
    {
        public UpdatePW()
        {
            InitializeComponent();
        }

        public delegate void Close();
        public static event Close Close_admin;
        public static event Close Close_user;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(pw1.Password == null || pw2.Password == null)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            else if(pw1.Password != pw2.Password)
            {
                MessageBox.Show("两次密码应一致！");
                return;
            }
            else
            {
                string pw = pw1.Password;
                string ID = User.UserID;
                string type = User.UserType;
                if(DatabaseCon.UpdatePW(ref ID, ref pw, ref type))
                {
                    MessageBox.Show("修改成功，请重新登录!");
                    MainWindow login = new MainWindow();
                    Window cur = Window.GetWindow(this);
                    if(type == "admin")
                    {
                        Close_admin();
                    }
                    else
                    {
                        Close_user();
                    }
                    cur.Close();
                    login.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("修改失败！");
                    return;
                }
            }
        }
    }
}
