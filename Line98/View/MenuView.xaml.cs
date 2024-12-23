using Microsoft.Windows.Themes;
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

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void btnStyle_Click(object sender, RoutedEventArgs e)
        {
            borderStyle.Visibility = Visibility.Visible;
            borderScore.Visibility = Visibility.Collapsed;
            borderHelp.Visibility = Visibility.Collapsed;
        }

        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            borderStyle.Visibility = Visibility.Collapsed;
            borderScore.Visibility = Visibility.Visible;
            borderHelp.Visibility = Visibility.Collapsed;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            borderStyle.Visibility = Visibility.Collapsed;
            borderScore.Visibility = Visibility.Collapsed;
            borderHelp.Visibility = Visibility.Visible;
        }
    }
}
