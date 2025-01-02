using System;
using System.Collections.Generic;
using System.IO;
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

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for MenuScoreNormal.xaml
    /// </summary>
    public partial class MenuScoreNormal : UserControl
    {
        string normalfilePath = "pack://application:,,,/ScoreData/NormalScore.txt";
        string timerfilePath = "pack://application:,,,/ScoreData/TimerScore.txt";

        public MenuScoreNormal()
        {
            InitializeComponent();
            ShowScore(normalfilePath);
        }

        void ShowScore(string uriPath)
        {
            var data = new List<TableRow>();

            // Sử dụng Application.GetResourceStream để đọc file
            var resourceUri = new Uri(uriPath, UriKind.RelativeOrAbsolute);
            var resourceStream = Application.GetResourceStream(resourceUri);

            if (resourceStream != null)
            {
                using (var reader = new StreamReader(resourceStream.Stream))
                {
                    string line;
                    int count = 1;
                    while ((line = reader.ReadLine()) != null && count <=4 )
                    {
                        var trimmedLine = line.Trim();
                        var columns = trimmedLine.Split('.'); // Phân tách bằng dấu chấm
                        if (columns.Length == 2) // Kiểm tra đủ 2 cột
                        {
                            data.Add(new TableRow { Column1 = columns[0], Column2 = columns[1] });
                        }
                        count++;
                    }
                }
            }

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
            ShowScore(normalfilePath);
            
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            pNormal.Visibility = Visibility.Collapsed;
            pTimer.Visibility = Visibility.Visible;
            borderNormal.Visibility = Visibility.Collapsed;
            borderTimer.Visibility = Visibility.Visible;
            ShowScore(timerfilePath);
        }
    }
}
