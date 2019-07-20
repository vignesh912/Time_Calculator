using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecondsToDays
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int TIME_CONSTANT = 60;
        const int HOURS_IN_DAY = 24;
        int ResetFlag = 0;
        public MainWindow()
        {
            InitializeComponent();
            ClearVal("00", "00", "00", "00");
        }
        private async void BtnOff_Click(object sender, RoutedEventArgs e)
        {
            ClearVal("Go", "od", " B", "ye");
            await Task.Delay(1500);
            this.Close();
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ClearVal("00", "00", "00", "00");
            ResetFlag = 1;
        }
        private async void UpdateTime(Label Lbl, double Value)
        {
            int i;
            int Del = 10;
            int MaxVal = 100;
            for (i = 0; i <= Value; i++)
            {
                if (ResetFlag == 1)
                {
                    ResetFlag = 0;
                    break;
                }      
                await Task.Delay(Del);
                if (i >= MaxVal)
                {
                    Lbl.FontSize = 30;
                    Lbl.VerticalContentAlignment = VerticalAlignment.Top;
                    Lbl.HorizontalContentAlignment = HorizontalAlignment.Right;
                }
                else
                {
                    Lbl.FontSize = 55;
                }

                Lbl.Content = i.ToString().PadLeft(2, '0');
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int sec;
            int min = 0;
            int hour = 0;
            int day = 0;
            try
            {
                sec = int.Parse(TxtTime.Text);
                if (sec >= 0)
                {
                    if (sec >= TIME_CONSTANT)
                    {
                        min = sec / TIME_CONSTANT;
                        sec %= TIME_CONSTANT;
                    }
                    if (min >= TIME_CONSTANT)
                    {
                        hour = min / TIME_CONSTANT;
                        min %= TIME_CONSTANT;
                    }
                    if (hour >= HOURS_IN_DAY)
                    {
                        day = hour / HOURS_IN_DAY;
                        hour %= HOURS_IN_DAY;
                    }
                    UpdateTime(LblSecs, sec);
                    UpdateTime(LblMins, min);
                    UpdateTime(LblHrs, hour);
                    UpdateTime(LblDays, day);
                }
                else
                {
                    ClearVal("Ne", "ga", "ti", "ve");
                }
            }
            catch
            {
                ClearVal("In", "va", "li", "d");
            }
        }
        private async void ClearVal(string v1, string v2, string v3, string v4)
        {
            TxtTime.Clear();
            TxtTime.Focus();
            bool BlinkOn = true;
            int i;
            for (i = 0; i < 5; i++)
            {
                await Task.Delay(120);
                LblDays.FontSize = 55;
                if (BlinkOn)
                {
                    LblDays.Content = v1;
                    LblHrs.Content = v2;
                    LblMins.Content = v3;
                    LblSecs.Content = v4;
                    BlinkOn = false;
                }
                else
                {
                    LblDays.Content = " ";
                    LblHrs.Content = " ";
                    LblMins.Content = " ";
                    LblSecs.Content = " ";
                    BlinkOn = true;
                }
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
