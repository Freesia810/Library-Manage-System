using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;


namespace LibraryManageSystem
{
    /// <summary>
    /// AdminWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            UpdatePW.Close_admin += () =>
            {
                this.Close();
            };

            var menuBook = new List<SubItem>();
            menuBook.Add(new SubItem("书籍信息汇总", new UserControlBookInfo()));
            menuBook.Add(new SubItem("书籍上架", new UserControlBookAdd()));
            menuBook.Add(new SubItem("书籍下架",new UserControlBookRemove()));
            menuBook.Add(new SubItem("信息更改",new UserControlBookUpdate()));
            menuBook.Add(new SubItem("书籍查询", new BookQuery()));
            var itemBook = new ItemMenu("书籍管理", menuBook, PackIconKind.Book);

            var menuReader = new List<SubItem>();
            menuReader.Add(new SubItem("读者信息汇总", new UserControlReaderInfo()));
            menuReader.Add(new SubItem("添加借书卡", new UserControlReaderAdd()));
            menuReader.Add(new SubItem("删除借书卡", new UserControlReaderRemove()));
            menuReader.Add(new SubItem("信息更改", new UserControlReaderUpdate()));
            var itemReader = new ItemMenu("借书卡管理", menuReader, PackIconKind.People);

            var menuBorrow = new List<SubItem>();
            menuBorrow.Add(new SubItem("借阅信息汇总", new UserControlBorrowInfo()));
            menuBorrow.Add(new SubItem("添加借阅数据", new UserControlBorrowAdd()));
            menuBorrow.Add(new SubItem("信息更改", new UserControlBorrowUpdate()));
            var itemBorrow = new ItemMenu("借阅管理", menuBorrow, PackIconKind.Library);

            var menuSettings = new List<SubItem>();
            menuSettings.Add(new SubItem("统计信息", new StatInfo()));
            menuSettings.Add(new SubItem("账户设置", new AccountSetting()));
            menuSettings.Add(new SubItem("关于", new About()));
            var itemSettings = new ItemMenu("设置", menuSettings, PackIconKind.Settings);

            Menu.Children.Add(new UserControlMenuItem(itemBook, this));
            Menu.Children.Add(new UserControlMenuItem(itemReader, this));
            Menu.Children.Add(new UserControlMenuItem(itemBorrow, this));
            Menu.Children.Add(new UserControlMenuItem(itemSettings, this));
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
