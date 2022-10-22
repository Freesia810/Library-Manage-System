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
    /// UserControlBookUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBookUpdate : UserControl
    {
        public UserControlBookUpdate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //查询
        {
            String bID = idBox.Text;
            if (bID.Length == 0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (idBox.Text.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            Book book = new Book();
            book.ID = bID;
            if(DatabaseCon.SelectSingleBook(ref book))
            {
                caLabel.Visibility = nameLabel.Visibility = pressLabel.Visibility = yearLabel.Visibility = authorLabel.Visibility = isbnLabel.Visibility = priceLabel.Visibility = totalLabel.Visibility = curLabel.Visibility = Visibility.Visible;
                caBox.Visibility = nameBox.Visibility = pressBox.Visibility = yearBox.Visibility = authorBox.Visibility = isbnBox.Visibility = priceBox.Visibility = totalBox.Visibility = curBox.Visibility = Visibility.Visible;
                submitButton.Visibility=Visibility.Visible;

                caBox.Text = book.Category;
                nameBox.Text=book.Name;
                pressBox.Text = book.Press;
                yearBox.Text = book.Year.ToString();
                authorBox.Text=book.Author;
                isbnBox.Text = book.ISBN;
                priceBox.Text = book.Price.ToString();
                totalBox.Text = book.totalNum.ToString();
                curBox.Text = book.curNum.ToString();

                return;
            }
            else
            {
                MessageBox.Show("查询失败，请检查编号是否正确！");
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //确认更改
        {
            Book book = new Book();
            if (idBox.Text.Length == 0 || caBox.Text.Length == 0 || nameBox.Text.Length == 0 || authorBox.Text.Length == 0 || yearBox.Text.Length == 0 || pressBox.Text.Length == 0 || isbnBox.Text.Length == 0 || priceBox.Text.Length == 0 || totalBox.Text.Length == 0 || curBox.Text.Length == 0)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            if (nameBox.Text.Length > 60 || authorBox.Text.Length > 60 || pressBox.Text.Length > 60 || isbnBox.Text.Length > 13)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }

            book.ID = idBox.Text;
            book.Category = caBox.Text;
            book.Name = nameBox.Text;
            book.Press = pressBox.Text;
            book.Year = int.Parse(yearBox.Text);
            book.Author = authorBox.Text;
            book.ISBN = isbnBox.Text;
            book.Price = decimal.Parse(priceBox.Text);
            book.totalNum = int.Parse(totalBox.Text);
            book.curNum = int.Parse(curBox.Text);

            if (DatabaseCon.UpdateBook(ref book))
            {
                DatabaseCon.AdminRefreshData();
                caLabel.Visibility = nameLabel.Visibility = pressLabel.Visibility = yearLabel.Visibility = authorLabel.Visibility = isbnLabel.Visibility = priceLabel.Visibility = totalLabel.Visibility = curLabel.Visibility = Visibility.Collapsed;
                caBox.Visibility = nameBox.Visibility = pressBox.Visibility = yearBox.Visibility = authorBox.Visibility = isbnBox.Visibility = priceBox.Visibility = totalBox.Visibility = curBox.Visibility = Visibility.Collapsed;
                idBox.Text = null;
                submitButton.Visibility = Visibility.Collapsed;
                MessageBox.Show("更改成功！");
                return;
            }
            else
            {
                MessageBox.Show("更改失败，请检查编号是否正确！");
                return;
            }
        }
    }
}
