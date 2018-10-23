using ASM.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.Login
{
  
    public sealed partial class Login : Page
    {
        member CurrentMember;
        private string CurrentTag = "";
        public Login()

        {
            CurrentMember = new member();
            this.InitializeComponent();
        }

        private async void loGin_Click(object sender, RoutedEventArgs e)
        {
            string email = this.Email.Text;
            string password = this.Password.Password;
            Dictionary<String, String> memberLogin = new Dictionary<string, string>();
            memberLogin.Add("email", email);
            memberLogin.Add("password", password);
            HttpClient httpClient = new HttpClient();

            string jsonMember = JsonConvert.SerializeObject(memberLogin);
            Debug.WriteLine(jsonMember);

            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/members/authentication", content);
            var contents = await response.Result.Content.ReadAsStringAsync();
            if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {

            }
            else
            {
                //thông báo lỗi 
                Eroor eroor = JsonConvert.DeserializeObject<Eroor>(contents);

                if (eroor.error.Count > 0)
                {
                    foreach (var key in eroor.error.Keys)
                    {
                        var objectBykey = this.FindName(key);
                        var value = eroor.error[key];
                        if (objectBykey != null)
                        {

                            TextBlock textblock = objectBykey as TextBlock;
                            textblock.Text = "* " + value;
                            textblock.Visibility = Visibility.Visible;
                        }

                        Debug.WriteLine(contents);

                    }
                }
            }
        }
                
            

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
            }
        }
    }

