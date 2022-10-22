using Microsoft.Win32;
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
    /// UserControlBookAdd.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlBookAdd : UserControl
    {
        public UserControlBookAdd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Book book = new Book();
            if(idBox.Text.Length==0||caBox.Text.Length==0||nameBox.Text.Length==0||authorBox.Text.Length==0||yearBox.Text.Length==0||pressBox.Text.Length==0||isbnBox.Text.Length==0||priceBox.Text.Length==0||totalBox.Text.Length==0||curBox.Text.Length==0)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            if(idBox.Text.Length > 60 || nameBox.Text.Length > 60 || authorBox.Text.Length > 60 || pressBox.Text.Length > 60 || isbnBox.Text.Length > 13)
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

            if (DatabaseCon.AddBook(ref book))
            {
                DatabaseCon.AdminRefreshData();
                idBox.Text = nameBox.Text = authorBox.Text = pressBox.Text = caBox.Text = yearBox.Text = priceBox.Text = totalBox.Text = curBox.Text = isbnBox.Text = null;
                MessageBox.Show("上架完成！");
                return;
            }
            else
            {
                MessageBox.Show("上架失败，请检查输入信息！");
                return;
            }
        }

        private void multiBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "图书管理系统书籍记录|*.lmsb";
            openFile.RestoreDirectory = true;
            string filePath;
            if ((bool)openFile.ShowDialog())
            {
                filePath = openFile.FileName;
                int resRow = DatabaseCon.MultiInsert(filePath, "books");
                
                if(resRow == -1)
                {
                    MessageBox.Show("批量导入错误");
                    return;
                }
                else
                {
                    MessageBox.Show("影响记录数：" + resRow.ToString());
                    DatabaseCon.AdminRefreshData();
                    return;
                }
            }

        }
    }
}
