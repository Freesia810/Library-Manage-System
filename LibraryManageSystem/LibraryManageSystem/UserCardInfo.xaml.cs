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
    /// UserCardInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UserCardInfo : UserControl
    {
        static Reader reader = new Reader();
        static CardPrint cardPrint = new CardPrint();
        public UserCardInfo()
        {
            InitializeComponent();


            DatabaseCon.UserRefreshData();

            DataContext = cardPrint;
            idL.Content = "编号：" + User.UserID;
            nameL.Content = "姓名：" + reader.Name;
            deptL.Content = "部门：" + reader.Depart;
            typeL.Content = "身份：" + (reader.Type == "T" ? "教师" : "学生");
            
        }
        public static void RefreshData()
        {
            reader.ID = User.UserID;
            DatabaseCon.SelectSingleReader(ref reader);
            cardPrint.num = "借阅数量：" + reader.Num;
            cardPrint.credit = "信用分数："+reader.Credit;
        }
    }
}
