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
    /// UserControlReaderUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlReaderUpdate : UserControl
    {
        public UserControlReaderUpdate()
        {
            InitializeComponent();
            List<TypeCategory> readerTypes = new List<TypeCategory>();
            readerTypes.Add(new TypeCategory { Name = "学生", Value = "S" });
            readerTypes.Add(new TypeCategory { Name = "教师", Value = "T" });
            typeBox.ItemsSource = readerTypes;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //查询
        {
            String rID = idBox.Text;
            if (rID.Length == 0)
            {
                MessageBox.Show("请输入编号！");
                return;
            }
            if (rID.Length > 60)
            {
                MessageBox.Show("输入超过最大字符！");
                return;
            }
            Reader reader = new Reader();
            reader.ID = rID;
            if (DatabaseCon.SelectSingleReader(ref reader))
            {
                CLabel.Visibility = NLabel.Visibility = nameLabel.Visibility = deptLabel.Visibility = typeLabel.Visibility = numLabel.Visibility = creditLabel.Visibility = Visibility.Visible;
                nameBox.Visibility = deptBox.Visibility = typeBox.Visibility = sliderN.Visibility = sliderC.Visibility = Visibility.Visible;
                submitButton.Visibility = Visibility.Visible;

                nameBox.Text = reader.Name;
                deptBox.Text = reader.Depart;
                typeBox.SelectedValue = reader.Type;
                sliderN.Value = reader.Num;
                sliderC.Value = reader.Credit;

                return;
            }
            else
            {
                MessageBox.Show("查询失败，请检查编号是否正确！");
                return;
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e) //更改
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

            if (DatabaseCon.UpdateReader(ref reader))
            {
                DatabaseCon.AdminRefreshData();
                CLabel.Visibility = NLabel.Visibility = nameLabel.Visibility = deptLabel.Visibility = typeLabel.Visibility = numLabel.Visibility = creditLabel.Visibility = Visibility.Collapsed;
                nameBox.Visibility = deptBox.Visibility = typeBox.Visibility = sliderN.Visibility = sliderC.Visibility = Visibility.Collapsed;
                idBox.Text = null;
                submitButton.Visibility = Visibility.Collapsed;
                MessageBox.Show("更改成功！");
                return;
            }
            else
            {
                MessageBox.Show("更改失败，请检查输入是否正确！");
                return;
            }
        }
    }
}
