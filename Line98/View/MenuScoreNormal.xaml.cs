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
    /// Interaction logic for MenuScoreNormal.xaml
    /// </summary>
    public partial class MenuScoreNormal : UserControl
    {
        public MenuScoreNormal()
        {
            InitializeComponent();
        }
        private void btnNormal_Click(object sender, RoutedEventArgs e)
        {
            pNormal.Visibility = Visibility.Visible;
            pTimer.Visibility = Visibility.Collapsed;
            borderNormal.Visibility = Visibility.Visible;
            borderTimer.Visibility = Visibility.Collapsed;
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            pNormal.Visibility = Visibility.Collapsed;
            pTimer.Visibility = Visibility.Visible;
            borderNormal.Visibility = Visibility.Collapsed;
            borderTimer.Visibility = Visibility.Visible;
        }
    }
}
