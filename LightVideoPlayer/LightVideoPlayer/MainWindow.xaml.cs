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
using System.Windows.Threading;

namespace LightVideoPlayer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool ispaused { get; set; }
        bool isfull { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            isfull = false;
            ispaused = true;
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
            TheMedia.Position = TimeSpan.FromSeconds(TimeSlider.Value);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void MediaFailedError(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void PausePlay(object sender, RoutedEventArgs e)
        {
            if (!ispaused)
            {
                TheMedia.Pause();
            }
            else
            {
                TheMedia.Play();
            }
            ispaused = !ispaused;
        }

        private void TheMediaLoaded(object sender, RoutedEventArgs e)
        {
            //TheMedia.Play();
            ispaused = false;
        }
        DispatcherTimer timer = null;
        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            
        }
        private void timer_tick(object sender, EventArgs e)
        {
            TimeSlider.Value = TheMedia.Position.TotalSeconds;
            TimeSlider.SelectionEnd = TimeSlider.Value;
        }
        private void TheMedia_MediaOpened(object sender, RoutedEventArgs e)
        {
            labelfulltime.Content = TheMedia.NaturalDuration.ToString();
            TimeSlider.Maximum = TheMedia.NaturalDuration.TimeSpan.TotalSeconds;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
            TimeSlider.SelectionStart = 0;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSlider.Value > 11)
                TimeSlider.Value -= 10;
            else
                TimeSlider.Value = 0;
        }

        private void BtnFore_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSlider.Value < TimeSlider.Maximum - 31)
                TimeSlider.Value += 30;
            else
                TimeSlider.Value = TimeSlider.Maximum;
        }

        private void BtnFull_Click(object sender, RoutedEventArgs e)
        {
            if(!isfull)
            {
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                this.ResizeMode = ResizeMode.NoResize;
                Topmost = true;
                this.Left = 0.0;
                this.Top = 0.0;
                this.Width = SystemParameters.PrimaryScreenWidth;
                this.Height = SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.SingleBorderWindow;
                this.ResizeMode = ResizeMode.CanResize;
                Topmost = false;
            }
            isfull = !isfull;
        }
    }
}
