using AliHelper_2._0.Models;
using AliHelper_2._0.Models.RequestModel;
using AliHelper_2._0.Models.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AliHelper_2._0
{
    /// <summary>
    /// Логика взаимодействия для DesktopForm.xaml
    /// </summary>
    public partial class DesktopForm : Window
    {
        public DesktopForm(string Token)
        {
            InitializeComponent();
            this.Token = Token;

            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"Settings.txt"));
            LinkSettings = JsonConvert.DeserializeObject<LinkSettings>(File.ReadAllText(@"LinkSettings.txt"));
            PostSettings = JsonConvert.DeserializeObject<PostSettings>(File.ReadAllText(@"PostSettings.txt"));

            var SettingsJson = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            File.WriteAllText(@"Settings.txt", SettingsJson);

            var LinkSettingsJson = JsonConvert.SerializeObject(LinkSettings);
            File.WriteAllText(@"LinkSettings.txt", LinkSettingsJson);

            var PostSettingsJson = JsonConvert.SerializeObject(PostSettings);
            File.WriteAllText(@"PostSettings.txt", PostSettingsJson);

            LoadedForm();
        }

        public void LoadedForm()
        {
            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"Settings.txt"));
            LinkSettings = JsonConvert.DeserializeObject<LinkSettings>(File.ReadAllText(@"LinkSettings.txt"));
            PostSettings = JsonConvert.DeserializeObject<PostSettings>(File.ReadAllText(@"PostSettings.txt"));

            if (Settings.MediaElement != null)
            {
                Media.Visibility = Visibility.Visible;
                Media.Source = new Uri(Settings.MediaElement, UriKind.RelativeOrAbsolute);
                Media.Opacity = 1 - Settings.BackgroundDark;
            }
        }

        string Token;
        public Settings Settings = new Settings();
        public LinkSettings LinkSettings = new LinkSettings();
        public PostSettings PostSettings = new PostSettings();

        void ParseButton_Click(object sender, RoutedEventArgs e)
        {

            //Task.Run(() =>
            //{

                if (CreativeBox.Text == "" || LinkTextBox.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (LinkTextBox.Text.IndexOf("https://ru.aliexpress.com/") != -1 || LinkTextBox.Text.IndexOf("https://aliexpress.ru/") != -1)
                {

                    var Url = new Uri(LinkTextBox.Text);

                    string Link = "";

                    HttpWebRequest request = HttpWebRequest.CreateHttp(Url);
                    request.AllowAutoRedirect = true;

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    var contentString = reader.ReadToEnd();

                    var price = ParseSignlePrice(contentString);

                    Creative Creative = new Creative();



                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://app.epn.bz/creative/create");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Headers.Add("X-ACCESS-TOKEN", Token);

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{  \"type\": \"link\",\"link\": \"" + LinkTextBox.Text + "\",\"description\": \"" + CreativeBox.Text + "\",\"offerId\": \"1\"}";

                        streamWriter.Write(json);

                    }

                    string Result = "";

                    try
                    {
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            Result = result;
                        }
                    }
                    catch (WebException Ex)
                    {


                        Console.WriteLine(StreamToString(Ex.Response.GetResponseStream()));
                        if (StreamToString(Ex.Response.GetResponseStream()) == "{\"errors\":[{\"error\":401002,\"error_description\":\"Unauthorized: invalid access token or incorrect token payload\"}],\"result\":false}")
                        {
                            MessageBox.Show("Время дейстия авторизации истекло, повторите авторизацию!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    Creative = JsonConvert.DeserializeObject<Creative>(Result);
                    Link = Creative.Data.Attributes.Code.ToString();


                    var LinkSettings = JsonConvert.DeserializeObject<LinkSettings>(File.ReadAllText(@"LinkSettings.txt"));

                    if (LinkSettings.ShorterLink == true)
                    {
                        var httpWebRequest2 = (HttpWebRequest)WebRequest.Create("https://app.epn.bz/link-reduction");
                        httpWebRequest2.ContentType = "application/json";
                        httpWebRequest2.Method = "POST";
                        httpWebRequest2.Headers.Add("X-ACCESS-TOKEN", Token);

                        using (var streamWriter = new StreamWriter(httpWebRequest2.GetRequestStream()))
                        {
                            //Models.RequestModel.UrlContainer urlContainer = new Models.RequestModel.UrlContainer();
                            //Models.RequestModel.UrlContainer[] urlContainers = { urlContainer };


                            //LinkReduction linkReduction = new LinkReduction
                            //{
                            //    UrlContainer = urlContainers,
                            //    DomainCutter = LinkSettings.ShorterLinkType
                            //};

                            string json = "{" +
                                  "\"urlContainer\": [" +
                                     "{" +
                                      "\"url\": \"" + Creative.Data.Attributes.Code + "\"," +
                                          "\"domainCutter\": \"" + LinkSettings.ShorterLinkType + "\"," +
                                    "\"id\": 1" +
                                   "}" +
                                   "]," +
                                     "\"domainCutter\": \"" + LinkSettings.ShorterLinkType + "\"" +
                                   "}";

                            streamWriter.Write(json);

                        }

                        string Result2 = "";

                        try
                        {
                            var httpResponse = (HttpWebResponse)httpWebRequest2.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                Result2 = result;
                                var ShortLink = JsonConvert.DeserializeObject<ShortLink>(Result2);
                                Link = ShortLink.Data2.Attributes.Select(n => n.Result).Single().ToString();
                            }
                        }
                        catch (WebException Ex)
                        {
                            Console.WriteLine(StreamToString(Ex.Response.GetResponseStream()));
                        }


                    }



                    PriceTextBlock.Dispatcher.BeginInvoke((Action)(() =>
                {
                    PriceTextBlock.Text = DescriptionTextBox.Text == "" ? "" : DescriptionTextBox.Text + "\n\n";
                    PriceTextBlock.Text += "💵Цена: " + price;
                    PriceTextBlock.Text += "\n\n🛍Купить:\n" + Link;
                }));


                }
                else
                {
                    MessageBox.Show("Ссылка должна вести на AliExpress!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            //});
        }

        public string GenerateLink(string CreativeText, string LinkText)
        {
            return null;
        }

        public static string StreamToString(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
        private String ParseSignlePrice(String content)
        {
            String fromTotalValue = content.Substring(
                content.IndexOf("formatedActivityPrice\":\"") + ("formatedActivityPrice\":\"").Length
            );

            if (fromTotalValue.Substring(0, fromTotalValue.IndexOf("\"")) == "xmlns:og=" || fromTotalValue.Substring(0, fromTotalValue.IndexOf("\"")) == " xmlns:og=")
            {
                fromTotalValue = content.Substring(content.IndexOf("formatedPrice\":\"") + ("formatedPrice\":\"").Length);
            }

            return fromTotalValue.Substring(0, fromTotalValue.IndexOf("\""));
        }

        private void SaveInTelegram_Click(object sender, RoutedEventArgs e)
        {
            CreativeBox.Text = "";
            DescriptionTextBox.Text = "";
            PriceTextBlock.Text = "";
            LinkTextBox.Text = "";
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();

                File.WriteAllText(@"Token.txt", "");

                this.Close();

            }
        }

        private void MediaElement_Unloaded(object sender, RoutedEventArgs e)
        {

        }



        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingForm settingForm = new SettingForm(this);
            settingForm.ShowDialog();

            //Media.Source = new Uri(Settings.MediaElement, UriKind.RelativeOrAbsolute);
        }

        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            Media.Position = TimeSpan.FromSeconds(0);
        }

        private void DescriptionTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //if (e.Key == Key.Enter) DescriptionTextBox.Text += ;
        }
    }
}

