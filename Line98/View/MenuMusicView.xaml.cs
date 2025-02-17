﻿using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using Line98.Model;

namespace Line98.View
{
    public partial class MenuMusicView : UserControl
    {
        public MenuMusicView()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked_1(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeMusic.Instance.ChangeMusicTo("Song1");
        }

        private void RadioButton_Checked_2(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeMusic.Instance.ChangeMusicTo("Song2");
        }

        private void RadioButton_Checked_3(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeMusic.Instance.ChangeMusicTo("Song3");
        }

        private void RadioButton_Checked_4(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeMusic.Instance.ChangeMusicTo("Song4");
        }

        private void RadioButton_Checked_5(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeMusic.Instance.StopMusic();
        }
    }
}
