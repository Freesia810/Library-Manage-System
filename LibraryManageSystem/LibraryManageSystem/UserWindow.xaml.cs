using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace LibraryManageSystem
{
    /// <summary>
    /// UserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();

            UpdatePW.Close_user += () =>
            {
                this.Close();
            };

            var menuBook = new List<SubItem>();
            menuBook.Add(new SubItem("书籍信息汇总", new UserControlBookInfo()));
            menuBook.Add(new SubItem("书籍查询", new BookQuery()));
            var itemBook = new ItemMenu("书籍服务", menuBook, PackIconKind.Book);

            var menuBorrow = new List<SubItem>();
            menuBorrow.Add(new SubItem("我的借阅信息", new UserBorrowInfo()));
            menuBorrow.Add(new SubItem("我要借书", new UserBorrowBook()));
            menuBorrow.Add(new SubItem("我要还书", new UserReturnBook()));
            menuBorrow.Add(new SubItem("我的借书卡", new UserCardInfo()));
            var itemBorrow = new ItemMenu("借阅管理", menuBorrow, PackIconKind.Library);

            var menuSettings = new List<SubItem>();
            menuSettings.Add(new SubItem("账户设置", new AccountSetting()));
            menuSettings.Add(new SubItem("关于", new About()));
            var itemSettings = new ItemMenu("设置", menuSettings, PackIconKind.Settings);



            Menu.Children.Add(new UserControlReaderMenuItem(itemBook, this));
            Menu.Children.Add(new UserControlReaderMenuItem(itemBorrow, this));
            Menu.Children.Add(new UserControlReaderMenuItem(itemSettings, this));
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
