using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Application = System.Windows.Application;
using Color = System.Windows.Media.Color;
using Forms = System.Windows.Forms;
using TextBox = System.Windows.Controls.TextBox;

namespace ArdorRGBKeyboardController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ColorR = 1, ColorG = 1, ColorB = 1;
        Random rnd = new Random();
        RGBKB RGBController = new RGBKB();
        bool DynamicThreadActive = false;
        float ColorChangeSpeed = 0.03f;
        List<DynamicLightMode> DynamicLightModes;
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            RefreshDynamicLightModeList();
            DynamicModesComboBox.ItemsSource = DynamicLightModes;
            DynamicModesComboBox.SelectedIndex = 0;
            Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon();
            Forms.NotifyIcon icon = new Forms.NotifyIcon();
            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Resources/ArdorLogo.ico")).Stream;
            notifyIcon.Icon = new Icon(iconStream);
            notifyIcon.Text = "RGBController";
            notifyIcon.Click += NotifyIcon_Click;
            notifyIcon.Visible = true;
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Focus();
            Activate();
        }

        private void RefreshDynamicLightModeList()
        {
            DynamicLightModes = new List<DynamicLightMode>
            {
                new DynamicLightMode("Переливающиеся цвета", "Постоянное плавное изменение цветов на случайные", new Thread(DefaultCycleMode)),
                new DynamicLightMode("Переливающиеся цвета (флаг России)", "Постоянное плавное изменение цветов как на флаге России", new Thread(RussiaFlagCycleMode)),
                new DynamicLightMode("Адаптивная подсветка", "//", new Thread(ScreenAdaptiveColor)),
            };
        }
        private void OnlyDigit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorR = rnd.Next(50, 256);
            ColorG = rnd.Next(50, 256);
            ColorB = rnd.Next(50, 256);
            RSlider.Value = ColorR;
            GSlider.Value = ColorG;
            BSlider.Value = ColorB;
            CreatorLabel.Content = App.Creator;
            VersionLabel.Content = App.Version;
        }
        private void CheckNumInTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = "0";
            }
            int temp = int.Parse((sender as TextBox).Text);
            if (temp == 0)
            {
                (sender as TextBox).Text = "1";
            }
            if (temp >= 256)
            {
                (sender as TextBox).Text = "255";
            }
        }
        private void UpdateCircleColor()
        {
            ColorCircleOnBlackSquare.Fill = new SolidColorBrush(Color.FromRgb((byte)ColorR, (byte)ColorG, (byte)ColorB));
            ColorCircleOnWhiteSquare.Fill = new SolidColorBrush(Color.FromRgb((byte)ColorR, (byte)ColorG, (byte)ColorB));
        }

        private void RTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNumInTextBox_LostFocus(sender, e);
            ColorR = int.Parse((sender as TextBox).Text);
            if (RSlider != null)
            {
                RSlider.Value = ColorR;
            }
            UpdateCircleColor();
        }

        private void GTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNumInTextBox_LostFocus(sender, e);
            ColorG = int.Parse((sender as TextBox).Text);
            if (GSlider != null)
            {
                GSlider.Value = ColorG;
            }
            UpdateCircleColor();
        }

        private void BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNumInTextBox_LostFocus(sender, e);
            ColorB = int.Parse((sender as TextBox).Text);
            if (RSlider != null)
            {
                BSlider.Value = ColorB;
            }
            UpdateCircleColor();
        }

        private void RSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorR = (int)e.NewValue;
            RTextBox.Text = ColorR.ToString();
            UpdateCircleColor();
        }
        private void GSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorG= (int)e.NewValue;
            GTextBox.Text = ColorG.ToString();
            UpdateCircleColor();
        }

        private void BSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorB = (int)e.NewValue;
            BTextBox.Text = ColorB.ToString();
            UpdateCircleColor();
        }

        private void DynamicModesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DescriptionTextBlock.Text = (DynamicModesComboBox.SelectedItem as DynamicLightMode).Description;
        }
        public void DefaultCycleMode()
        {
            Color currentColor = Color.FromRgb((byte)ColorR, (byte)ColorG, (byte)ColorB);
            Color targetColor = Color.FromRgb(255, 255, 255);
            float step = 0;
            int R, G, B, mixR, mixG, mixB;
            while (true)
            {
                if (step >= 1f)
                {
                    step = 0;
                    R = rnd.Next(25, 255);
                    G = rnd.Next(25, 255);
                    B = rnd.Next(25, 255);
                    currentColor = targetColor;
                    targetColor = Color.FromRgb((byte)R, (byte)G, (byte)B);
                }
                mixR = (int)(currentColor.R * (1f - step) + targetColor.R * step);
                mixG = (int)(currentColor.G * (1f - step) + targetColor.G * step);
                mixB = (int)(currentColor.B * (1f - step) + targetColor.B * step);
                RGBController.All = Color.FromRgb((byte)mixR, (byte)mixG, (byte)mixB);
                RGBController.SetAllColor();
                step += ColorChangeSpeed;
                Thread.Sleep(5);
                if (!DynamicThreadActive)
                {
                    break;
                }
            }
        }

        private void RussiaFlagCycleMode()
        {
            Color currentColor = Color.FromRgb((byte)ColorR, (byte)ColorG, (byte)ColorB);
            Color targetColor = Color.FromRgb(255, 255, 255);
            float step = 0;
            int ColorChangeStep = 0;
            int R, G, B, mixR, mixG, mixB;
            while (true)
            {
                if (step >= 1f)
                {
                    step = 0;
                    if (ColorChangeStep == 0)
                    {
                        R = 255;
                        G = 255;
                        B = 255;
                    }
                    else if (ColorChangeStep == 1)
                    {
                        R = 0;
                        G = 0;
                        B = 255;
                    }
                    else
                    {
                        R = 255;
                        G = 0;
                        B = 0;
                        ColorChangeStep = -1;
                    }
                    ColorChangeStep++;
                    currentColor = targetColor;
                    targetColor = Color.FromRgb((byte)R, (byte)G, (byte)B);
                }
                mixR = (int)(currentColor.R * (1f - step) + targetColor.R * step);
                mixG = (int)(currentColor.G * (1f - step) + targetColor.G * step);
                mixB = (int)(currentColor.B * (1f - step) + targetColor.B * step);
                RGBController.All = Color.FromRgb((byte)mixR, (byte)mixG, (byte)mixB);
                RGBController.SetAllColor();
                step += ColorChangeSpeed;
                Thread.Sleep(5);
                if (!DynamicThreadActive)
                {
                    break;
                }
            }
        }
        private void ScreenAdaptiveColor()
        {
            Color currentColor = Color.FromRgb((byte)ColorR, (byte)ColorG, (byte)ColorB);
            Color targetColor = GetAverageColor();
            float step = 0;
            int R, G, B, mixR, mixG, mixB;
            while (true)
            {
                if (step >= 1f)
                {
                    step = 0;
                    currentColor = targetColor;
                    targetColor = GetAverageColor();
                    if (currentColor == targetColor)
                    {
                        Thread.Sleep(2000);
                    }
                }
                mixR = (int)(currentColor.R * (1f - step) + targetColor.R * step);
                mixG = (int)(currentColor.G * (1f - step) + targetColor.G * step);
                mixB = (int)(currentColor.B * (1f - step) + targetColor.B * step);
                RGBController.All = Color.FromRgb((byte)mixR, (byte)mixG, (byte)mixB);
                RGBController.SetAllColor();
                step += ColorChangeSpeed;
                Thread.Sleep(5);
                if (!DynamicThreadActive)
                {
                    break;
                }
            }
        }
        private void ApplyStaticRGB_Click(object sender, RoutedEventArgs e)
        {
            RGBController.All = Color.FromRgb((byte)ColorR, (byte)ColorG, (byte)ColorB);
            RGBController.SetAllColor();
        }
        private void ApplyDynamicRGB_Click(object sender, RoutedEventArgs e)
        {
            DynamicThreadActive = true;
            ColorChangeSpeed = (float)ColorChangeSpeedSlider.Value / 100;
            (DynamicModesComboBox.SelectedItem as DynamicLightMode).ModeThread.Start();
        }
        private void StopAllThreads_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DynamicThreadActive = false;
            Thread.Sleep(20);
            RefreshDynamicLightModeList();
            int SelectedIndex = DynamicModesComboBox.SelectedIndex;
            DynamicModesComboBox.ItemsSource = DynamicLightModes;
            DynamicModesComboBox.SelectedIndex = SelectedIndex;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DynamicThreadActive = false;
            Thread.Sleep(20);
        }
        public static Color GetAverageColor()
        {
            using (Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, bitmap.Size);

                int red = 0, green = 0, blue = 0;
                int count = 0;

                for (int y = 0; y < bitmap.Height; y += 32)
                {
                    for (int x = 0; x < bitmap.Width; x += 32)
                    {
                        System.Drawing.Color color = bitmap.GetPixel(x, y);

                        red += color.R;
                        green += color.G;
                        blue += color.B;

                        count++;
                    }
                }

                red /= count;
                green /= count;
                blue /= count;

                if (red < 100 && green < 100 && blue < 100)
                {
                    red += 51;
                    green += 51;
                    blue += 51;
                }

                return Color.FromRgb((byte)red, (byte)green, (byte)blue);
            }
        }
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized) this.Hide();

            base.OnStateChanged(e);
        }
    }
}