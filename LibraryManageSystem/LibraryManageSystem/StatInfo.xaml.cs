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
    /// StatInfo.xaml 的交互逻辑
    /// </summary>
    public partial class StatInfo : UserControl
    {
        static StatPrint stat = new StatPrint();
        public StatInfo()
        {
            InitializeComponent();

            DatabaseCon.AdminRefreshData();
            DataContext = stat;
        }
        public static void RefreshData()
        {
            DatabaseCon.StatData(ref stat);

        }
    }
}
