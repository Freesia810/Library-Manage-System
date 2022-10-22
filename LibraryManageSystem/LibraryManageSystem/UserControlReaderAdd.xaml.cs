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
    /// UserControlReaderAdd.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlReaderAdd : UserControl
    {
        public UserControlReaderAdd()
        {
            InitializeComponent();
            List<TypeCategory> readerTypes = new List<TypeCategory>();
            readerTypes.Add(new TypeCategory { Name = "学生", Value = "S" });
            readerTypes.Add(new TypeCategory { Name = "教师", Value = "T" });
            typeBox.ItemsSource = readerTypes;
            typeBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reader reader = new Reader();
            if (idBox.Text.Length == 0 || nameBox.Text.Length == 0 || deptBox.Text.Length == 0)
            {
                MessageBox.Show("请检查输入！");
                return;
            }
            if (idBox.Text.Length > 60 || nameBox.Text.Length > 60 || deptBox.Text.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            
            reader.ID = idBox.Text;
            reader.Name = nameBox.Text;
            reader.Depart = deptBox.Text;
            reader.Type = typeBox.SelectedValue.ToString();
            reader.Num = (int)sliderN.Value;
            reader.Credit = (int)sliderC.Value;
            if (DatabaseCon.AddReader(ref reader))
            {
                DatabaseCon.AdminRefreshData();
                idBox.Text = nameBox.Text = deptBox.Text = null;
                MessageBox.Show("添加成功！");
                return;
            }
            else
            {
                MessageBox.Show("添加失败，请检查输入信息！");
                return;
            }
        }

        private void multiBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "图书管理系统读者记录|*.lmsr";
            openFile.RestoreDirectory = true;
            string filePath;
            if ((bool)openFile.ShowDialog())
            {
                filePath = openFile.FileName;
                int resRow = DatabaseCon.MultiInsert(filePath, "readers");

                if (resRow == -1)
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
