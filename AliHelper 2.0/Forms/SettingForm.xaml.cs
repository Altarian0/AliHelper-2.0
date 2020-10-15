using AliHelper_2._0.Models.Settings;
using AliHelper_2._0.Pages;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AliHelper_2._0
{
    /// <summary>
    /// Логика взаимодействия для SettingForm.xaml
    /// </summary>
    public partial class SettingForm : Window
    {
        public SettingForm(DesktopForm desktopForm)
        {
            InitializeComponent();
            

            this.Settings = desktopForm.Settings;
            this.DesktopForm = desktopForm;
            this.LinkSettings = desktopForm.LinkSettings;
        }

        public Settings CancelSettings = new Settings();

        public Settings Settings { get; set; }
        public LinkSettings LinkSettings { get; set; }
        public DesktopForm DesktopForm { get; set; }


        private void ViewSettingsItem_Selected(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewSettingPage(this);
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        public void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Visibility = Visibility.Hidden;
            SaveChangesLabel.Visibility = Visibility.Visible;
            File.WriteAllText(@"Settings.txt", JsonConvert.SerializeObject(Settings));
            File.WriteAllText(@"LinkSettings.txt", JsonConvert.SerializeObject(LinkSettings));
            DesktopForm.LoadedForm();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelSettings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"Settings.txt"));
            DesktopForm.Media.Opacity = 1 - CancelSettings.BackgroundDark;
            DesktopForm.Media.Source = new Uri(CancelSettings.MediaElement, UriKind.RelativeOrAbsolute);
            this.Close();
        }

        private void SettingsItem_Selected(object sender, RoutedEventArgs e)
        {
            Main.Content = new LinkSettingPage(this);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PostSetting_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
