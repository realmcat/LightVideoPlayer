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

namespace LightVideoPlayer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TimeUpdate(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int h, m, s;
            h = (int)TimeSlider.Value / 3600;
            m = (int)TimeSlider.Value / 60 % 60; ;
            s = (int)TimeSlider.Value % 60;
            labelnowtime.Content = (h < 10 ? "0" + h.ToString() : h.ToString()) +
                ":" + (m < 10 ? "0" + m.ToString() : m.ToString()) +
                ":" + (s < 10 ? "0" + s.ToString() : s.ToString()); 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TheMedia.Close();
        }

        private void MediaFailedError(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void PausePlay(object sender, RoutedEventArgs e)
        {
            TheMedia.Play();
        }

        private void MediaOpened(object sender, RoutedEventArgs e)
        {
            labelfulltime.Content = TheMedia.NaturalDuration.ToString();


        }
    }
}
