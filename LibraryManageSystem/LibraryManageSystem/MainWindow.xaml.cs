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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String userName = userBox.Text;
            String password = pwBox.Password;
            bool isAdmin = adminButton.IsChecked == true ? true : false;
            if(userName.Length == 0 || password.Length == 0)
            {
                MessageBox.Show("用户名或密码为空！");
                return;
            }

            if (DatabaseCon.LoginDect(ref userName,ref password,ref isAdmin))
            {
                if(isAdmin)
                {
                    User.UserID = userName;
                    User.UserType = "admin";
                    AdminWindow adminmenu = new AdminWindow();
                    Window main = Window.GetWindow(this);
                    main.Close();
                    adminmenu.Show();
                }
                else
                {
                    User.UserID = userName;
                    User.UserType = "user";
                    UserWindow usermenu = new UserWindow();
                    Window main = Window.GetWindow(this);
                    main.Close();
                    usermenu.Show();
                }
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServerWindow serverWindow = new ServerWindow();
            serverWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}
