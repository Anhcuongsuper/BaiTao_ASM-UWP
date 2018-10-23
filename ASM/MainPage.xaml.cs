using ASM.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ASM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static string API_GET_UPLOAD_URL;
        private string currentUploadUrl;
        private member currentMember;
        private StorageFile photo;




        public static string API_GET_UPLOAD_URL1 { get => API_GET_UPLOAD_URL; set => API_GET_UPLOAD_URL = value; }
        public MainPage()

        {
            this.currentMember = new member();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private async void doSubmit_Click(object sender, RoutedEventArgs e)
        {
            this.currentMember.firstName = this.t_firstName.Text;
            this.currentMember.lastName = this.t_lastName.Text;
            this.currentMember.email = this.t_email.Text;
            this.currentMember.phone = this.t_phone.Text;
            this.currentMember.password = this.t_passWord.Password;
            this.currentMember.introduction = this.t_introduction.Text;
            this.currentMember.avatar = this.t_avatarUrl.Text;
            this.currentMember.address = this.t_address.Text;


            //string content = responseMessage = httpClient.PostAsync("https://2-dot-backup-server-002.appspot.com/_api/v2/members" stringContent).Result;
            string jsonMember = JsonConvert.SerializeObject(currentMember);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(Service.Service.API_REGISTER, content);
            var contents = await response.Result.Content.ReadAsStringAsync();

            Debug.WriteLine(contents);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                Debug.WriteLine("success login :D");
            }
            else
            {
                // báo error
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
                    }
                }
            }
        }
        private async void Choose_Image(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                // User cancelled photo capture
                return;
            }
            HttpClient httpClient = new HttpClient();
            currentUploadUrl = await httpClient.GetStringAsync("https://2-dot-backup-server-002.appspot.com/get-upload-token");
            Debug.WriteLine("Upload url:" + currentUploadUrl);
            HttpUploadFile(currentUploadUrl, "myFile", "image/png");


        }
        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string imageUrl = reader2.ReadToEnd();
                avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                t_avatarUrl.Text = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        private static CameraCaptureUIMode GetPhoto()
        {
            return CameraCaptureUIMode.Photo;
        }

       


        

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            this.currentMember.gender = Int32.Parse(radio.Tag.ToString());

        }
        private void BirthdayPicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            this.currentMember.birthday = t_birthdayPicker.Date.Value.ToString("yyyy-MM-dd");
        }



        private void doReset_Click(object sender, RoutedEventArgs e)
        {
            this.t_firstName.Text = string.Empty;
            this.t_lastName.Text = string.Empty;
            this.t_phone.Text = string.Empty;
            this.t_email.Text = string.Empty;
            this.t_passWord.Password = string.Empty;
            this.t_avatarUrl.Text = string.Empty;
            this.t_address.Text = string.Empty;
            this.t_introduction.Text = string.Empty;


        }

    }
}

