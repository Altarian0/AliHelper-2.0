using AliHelper_2._0.Models;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AliHelper_2._0.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewSettingPage.xaml
    /// </summary>
    public partial class ViewSettingPage : Page
    {
        public ViewSettingPage(SettingForm settingForm)
        {
            InitializeComponent();
            this.SettingForm = settingForm;

            WallpaperLabel.Content = SettingForm.Settings.MediaElement;
            BackgroundSlider.Value = SettingForm.Settings.BackgroundDark*100;

            BackgroundLabel.Content = Math.Round(SettingForm.Settings.BackgroundDark*100);
            
        }

        public SettingForm SettingForm { get; set; }
        

        private void WallpaperButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "mp4 files (*.mp4)|*.mp4";
            openFileDialog.ShowDialog();
            MediaElement mediaElement = new MediaElement();
            mediaElement.Source = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);

          
            SettingForm.Settings.MediaElement = openFileDialog.FileName;

            SettingForm.DesktopForm.Media.Source = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);

            WallpaperLabel.Content = SettingForm.Settings.MediaElement;
            SettingForm.SaveChangesButton.Visibility = Visibility.Visible;
            SettingForm.SaveChangesLabel.Visibility = Visibility.Hidden;
            
        }

        private void WallpaperClear_Click(object sender, RoutedEventArgs e)
        {
            

            SettingForm.Settings.MediaElement = "";

            WallpaperLabel.Content = SettingForm.Settings.MediaElement;
            SettingForm.DesktopForm.Media.Source = null;
            BackgroundSlider.Value = 0;
            BackgroundLabel.Content = 0;

            SettingForm.SaveChangesButton.Visibility = Visibility.Visible;
            SettingForm.SaveChangesLabel.Visibility = Visibility.Hidden;
            

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SettingForm.Settings.BackgroundDark = BackgroundSlider.Value / 100;

            SettingForm.DesktopForm.Media.Opacity = 1 - (BackgroundSlider.Value / 100);

            SettingForm.SaveChangesButton.Visibility = Visibility.Visible;
            SettingForm.SaveChangesLabel.Visibility = Visibility.Hidden;

            BackgroundLabel.Content = Math.Round(BackgroundSlider.Value);
        }
    }
}
