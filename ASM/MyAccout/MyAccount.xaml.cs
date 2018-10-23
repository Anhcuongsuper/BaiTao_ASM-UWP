using ASM.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.MyAccout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyAccount : Page

    {
        member member = new member();
        private static string API_MEMBER_INFOR_URL = "https://2-dot-backup-server-002.appspot.com/_api/v2/members/infor";
        public MyAccount()
        {
            this.InitializeComponent();          
            LoadMemberInformation();
        }
        private async void LoadMemberInformation()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "f2vLo0b9taD4byAvGs7iL2WQvTxEonEaEFi4L2UxLVzDM1DANBOWdJMjY3Skgpxa");
            var response = httpClient.GetAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/members/information");
            var content = await response.Result.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            member = JsonConvert.DeserializeObject<member>(content);
            Debug.WriteLine(member.email);
            this.txt_fullname.Text = member.firstName + " " + member.lastName;
            this.txt_birthday.Text = member.birthday;
            this.txt_email.Text = member.email;
            this.txt_address.Text = member.address;
            this.txt_phone.Text = member.phone;
            this.img_avatar.Source = new BitmapImage(new Uri(member.avatar, UriKind.Absolute));
        }
    }
}
