using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for MenuScoreNormal.xaml
    /// </summary>
    public partial class MenuScoreNormal : UserControl
    {
        public MenuScoreNormal()
        {
            InitializeComponent();
            ShowScore(1);
        }

        public static void BubbleSort(List<TableRow> data)
        {
            int n = data.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (string.Compare(data[j].Column2, data[j + 1].Column2) < 0)
                    {
                        // Hoán đổi 2 phần tử
                        var temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
        }

        void ShowScore(int type)
        {
            // Lấy dữ liệu từ Slot 1 (danh sách các chuỗi)
            List<string> x = ScoreSaveManager.LoadFromSlot(1);
            var data = new List<TableRow>();

            int count = 1;

            // Duyệt qua từng phần tử trong danh sách x (mỗi phần tử là một chuỗi)
            foreach (var line in x)
            {
                var trimmedLine = line.Trim(); // Loại bỏ khoảng trắng thừa
                var columns = trimmedLine.Split('.'); // Phân tách bằng dấu chấm

                if (columns.Length == 2) // Kiểm tra nếu có 2 phần tử sau khi phân tách
                {
                    data.Add(new TableRow { Column1 = columns[0], Column2 = columns[1] });
                }
                count++;
            }

            // Sắp xếp dữ liệu (nếu cần)
            BubbleSort(data);

            // Gắn dữ liệu vào ListBox
            lbNormal.ItemsSource = data;
        }

        public class TableRow
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
        }
        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            pNormal.Visibility = Visibility.Visible;
            pTimer.Visibility = Visibility.Collapsed;
            borderNormal.Visibility = Visibility.Visible;
            borderTimer.Visibility = Visibility.Collapsed;
            ShowScore(1);
            
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            pNormal.Visibility = Visibility.Collapsed;
            pTimer.Visibility = Visibility.Visible;
            borderNormal.Visibility = Visibility.Collapsed;
            borderTimer.Visibility = Visibility.Visible;
            ShowScore(2);
        }
    }
}
