using System.Windows.Controls;
using System.Windows;

namespace Line98.View
{
    /// <summary>
    /// Interaction logic for MenuStyleView.xaml
    /// </summary>
    public partial class MenuStyleView : UserControl
    {
        public MenuStyleView()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            StyleBallManager.Instance.SetStyle("balls");
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            StyleBallManager.Instance.SetStyle("Ball2");
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            StyleBallManager.Instance.SetStyle("ball3");
        }
        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            StyleBallManager.Instance.SetStyle("ball4");
        }
    }
}
