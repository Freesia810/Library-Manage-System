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
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(idBox.Text == null || pw.Password == null || pw2.Password == null)
            {
                MessageBox.Show("请检查输入");
                return;
            }
            if(pw.Password != pw2.Password)
            {
                MessageBox.Show("请检查密码输入");
                return;
            }
            Reader reader = new Reader();
            reader.ID = idBox.Text;

            if(DatabaseCon.SelectSingleReader(ref reader))
            {
                string user = reader.ID;
                string password = pw.Password;
                if(DatabaseCon.CreateAccount(ref user, ref password))
                {
                    MessageBox.Show("注册成功！");
                    Close();
                    return;
                }
                else
                {
                    MessageBox.Show("注册失败，请重试！");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请检查借书卡编号，或联系管理员");
                return;
            }

        }
    }
}
