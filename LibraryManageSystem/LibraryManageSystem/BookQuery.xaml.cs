using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// BookQuery.xaml 的交互逻辑
    /// </summary>
    public partial class BookQuery : UserControl
    {
        static ObservableCollection<Book> res = new ObservableCollection<Book>();
        public BookQuery()
        {
            InitializeComponent();
            DataGridQuery.AutoGenerateColumns = false;
            DataGridQuery.ItemsSource = res;
            List<TypeCategory> queryTypes = new List<TypeCategory>();
            queryTypes.Add(new TypeCategory { Name = "类别", Value = "category" });
            queryTypes.Add(new TypeCategory { Name = "书名", Value = "book_name" });
            queryTypes.Add(new TypeCategory { Name = "出版社", Value = "press" });
            queryTypes.Add(new TypeCategory { Name = "年份", Value = "year" });
            queryTypes.Add(new TypeCategory { Name = "作者", Value = "author" });
            queryTypes.Add(new TypeCategory { Name = "ISBN号", Value = "ISBN" });
            queryTypes.Add(new TypeCategory { Name = "价格", Value = "price" });
            typeBox.ItemsSource = queryTypes;
            typeBox.SelectedIndex = 0;
            DatabaseCon.BookInfo(ref res);
        }

        private void typeBox_DropDownClosed(object sender, EventArgs e)
        {
            string type;

            object obj = typeBox.SelectedValue;
            type = obj as string;
            if (type == "year")
            {
                int max = 2099;
                int min = 0;
                inputBox.Visibility = Visibility.Collapsed;
                saBox.SliderTickFrequency = 1M;
                saBox.Visibility = Visibility.Visible;
                DatabaseCon.QueryYearMinMax(ref min, ref max);
                saBox.Minimum = min;
                saBox.Maximum = max;
                saBox.StartValue = min;
                saBox.EndValue = max;
            }
            else if(type == "price")
            {
                decimal max = 1000M;
                decimal min = 0;
                inputBox.Visibility = Visibility.Collapsed;
                saBox.SliderTickFrequency = 0.01M;
                saBox.Visibility = Visibility.Visible;
                DatabaseCon.QueryPriceMinMax(ref min, ref max);
                saBox.Minimum = min;
                saBox.Maximum = max;
                saBox.StartValue = min;
                saBox.EndValue = max;
            }
            else if(type == null)
            {
                inputBox.Visibility = Visibility.Collapsed;
                saBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                inputBox.Visibility = Visibility.Visible;
                saBox.Visibility = Visibility.Collapsed;
            }

        }
        private void queryBtn_Click(object sender, RoutedEventArgs e)
        {
            string type;

            object obj = typeBox.SelectedValue;
            type = obj as string;
            if (type == "year")
            {
                int start = (int)saBox.StartValue;
                int end = (int)saBox.EndValue;
                DatabaseCon.QueryYear(res, start, end);
                MessageBox.Show("查询完成");
                return;
            }
            else if (type == "price")
            {
                DatabaseCon.QueryPrice(res,saBox.StartValue,saBox.EndValue);
                MessageBox.Show("查询完成");
                return;
            }
            else if(type == null)
            {
                DatabaseCon.BookInfo(ref res);
                MessageBox.Show("查询完成");
                return;
            }
            else
            {
                string keyWord = inputBox.Text;

                if (keyWord == null)
                {
                    MessageBox.Show("请输入内容！");
                    return;
                }

                DatabaseCon.QueryBook(ref res, type, keyWord);
                MessageBox.Show("查询完成");
                return;
            }
        }
    }
}
