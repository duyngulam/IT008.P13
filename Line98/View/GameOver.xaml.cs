using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        private int scoreSave;
        public GameOver(int score)
        {
            InitializeComponent();
            scoreSave = score;
        }

        private string placeholder;
        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                tbPlaceholder.Text = placeholder;
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text)) { tbPlaceholder.Visibility = Visibility.Visible; }
            else tbPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "D:\\KTPM\\HK3\\LapTrinhTrucQuan\\DoAn\\Line98\\ScoreData\\TimerScore.txt"; // Đường dẫn file
            string content = txtName.Text;

            File.AppendAllText(filePath, content + "." + scoreSave.ToString());
        }
    }
}
