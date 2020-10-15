using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using AliHelper_2._0.Models;
using System.Text;
using AliHelper_2._0.Methods;

namespace AliHelper_2._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (File.ReadAllText(@"Token.txt") != "")
            {
                DesktopForm desktopForm = new DesktopForm(File.ReadAllText(@"Token.txt"));
                desktopForm.Show();
                this.Close();
            }

            //LoginBox.Text = System.IO.File.ReadAllText(@"Login.txt");
            //PasswordBox.Text = File.ReadAllText(@"Password.txt");
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var SsidRequest = Repository.Get(new Uri("https://oauth2.epn.bz/ssid?client_id=web-client"));
            var SsidToken = JsonConvert.DeserializeObject<SSID>(SsidRequest);
                
            if (LoginBox.Text == "" || PasswordBox.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {

                var CredentialType = Repository.GetTokenWithClient(SsidToken.Data.Attributes.SsidToken, LoginBox.Text, PasswordBox.Text);

                var API = JsonConvert.DeserializeObject<AccessTokenResult>(CredentialType);

                DesktopForm desktopForm = new DesktopForm(API.Data.Attributes.AccessToken);
                desktopForm.Show();
                if (SaveDataCheckBox.IsChecked == true)
                {
                    File.WriteAllText(@"Token.txt", API.Data.Attributes.AccessToken);
                    //File.WriteAllText(@"Password.txt", PasswordBox.Text);
                }

                this.Close();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            LoginBox.Text = "";
            PasswordBox.Text = "";

            File.WriteAllText(@"Login.txt", "");
            File.WriteAllText(@"Password.txt", "");
        }
    }
}
