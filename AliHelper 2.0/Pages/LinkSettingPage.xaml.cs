using AliHelper_2._0.Models.Settings;
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

namespace AliHelper_2._0.Pages
{
    /// <summary>
    /// Логика взаимодействия для LinkSettingPage.xaml
    /// </summary>
    public partial class LinkSettingPage : Page
    {
        public LinkSettingPage(SettingForm settingForm)
        {
            InitializeComponent();
            this.SettingForm = settingForm;

           


            if (settingForm.LinkSettings.ShorterLink == true)
            {
                YesShortLinkRadio.IsChecked = true;
            }
            else
            {
                NoShortLinkRadio.IsChecked = true;
            }

            if (SettingForm.LinkSettings.ShorterLinkType == "ali.pub")
            {
                AliPubShortLinkRadio.IsChecked = true;
            }
            else
            {
                GotByShortLinkRadio.IsChecked = true;
            }

        }

        public SettingForm SettingForm { get; set; }

        private void YesShortLinkRadio_Checked(object sender, RoutedEventArgs e)
        {
            SettingForm.LinkSettings.ShorterLink = true;

            SettingForm.SaveChangesButton.Visibility = Visibility.Visible;
            SettingForm.SaveChangesLabel.Visibility = Visibility.Hidden;
        }

        private void NoShortLinkRadio_Checked(object sender, RoutedEventArgs e)
        {
            SettingForm.LinkSettings.ShorterLink = false;

            SettingForm.SaveChangesButton.Visibility = Visibility.Visible;
            SettingForm.SaveChangesLabel.Visibility = Visibility.Hidden;
        }

        private void AliPubShortLinkRadio_Checked(object sender, RoutedEventArgs e)
        {
            SettingForm.LinkSettings.ShorterLinkType = "ali.pub";
           

            SettingForm.SaveChangesButton.Visibility = Visibility.Visible;
            SettingForm.SaveChangesLabel.Visibility = Visibility.Hidden;
        }

        private void GotByShortLinkRadio_Checked(object sender, RoutedEventArgs e)
        {
            SettingForm.LinkSettings.ShorterLinkType = "got.by";

            SettingForm.SaveChangesButton.Visibility = Visibility.Visible;
            SettingForm.SaveChangesLabel.Visibility = Visibility.Hidden;
        }
    }
}
