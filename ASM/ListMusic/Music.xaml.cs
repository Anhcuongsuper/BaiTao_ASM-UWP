using ASM.Entity;
using ASM.MusicLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace ASM.ListMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Music : Page
    {
        private bool _isPlaying = false;

        private int _currentIndex = 0;

        private ObservableCollection<Song> listSong;

        internal ObservableCollection<Song> ListSong { get => listSong; set => listSong = value; }

        public Music()
        {
            this.ListSong = new ObservableCollection<Song>();      
            this.ListSong.Add(new Song()
            {
                name = "Vệt Nắng Cuối Trời",
                singer = "Hoàng Bách",
                thumbnail = "http://nguoi-noi-tieng.com/photo/tieu-su-ca-si-nhac-si-hoang-bach-9979.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui154/VetNangCuoiTroi-HoangBach_3553y.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Phía sau một cô gái",
                singer = "Sobin Hoàng Sơn",
                thumbnail = "http://phapluatnet.vn/Uploads/Originals/2017/5/28/20170528172129_sobin-hoang-son.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui929/PhiaSauMotCoGaiBeat-SoobinHoangSon-4632332.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Đường về Thanh Hóa",
                singer = "Lý Cường Singer",
                thumbnail = "https://scontent.fhan5-5.fna.fbcdn.net/v/t1.0-9/42173481_708980126134329_1523122255020687360_n.jpg?_nc_cat=108&oh=b296def90a90e8fcbb62c5d952109ca5&oe=5C552AB8",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui794/DuongVeThanhHoa-AnhTho_4ebup.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Về Quê Ngoại",
                singer = "Quang Lê",
                thumbnail = "https://avatar-nct.nixcdn.com/singer/avatar/2017/02/06/3/4/7/f/1486358917132_600.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui211/VeQueNgoai-QuangLe_3vg4w.mp3?st=LU5TMv6erWZEaOwGIa7bYg&e=1540064634&download=true"
            });
            this.ListSong.Add(new Song()
            {
                name = "Trộm nhìn nhau",
                singer = "Hoài Lâm",
                thumbnail = "https://upload.wikimedia.org/wikipedia/commons/c/c0/Ch%C3%A2n_dung_Ho%C3%A0i_L%C3%A2m.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui934/TromNhinNhau-HoaiLam-4677141.mp3"
            });
            this.ListSong.Add(new Song()
            {
                name = "Nhớ mãi một miền quê",
                singer = "Anh Thơ",
                thumbnail = "https://anh.24h.com.vn/upload/4-2014/images/2014-10-13/1413196089-anhtho.jpg",
                link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui969/HongKong1-NguyenTrongTai-5663439_hq.mp3"
            });
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Song song = new Song()
            {
                name = this.SongName.Text,
                thumbnail = this.SongThumbnail.Text
            };
            this.ListSong.Add(song);
            this.SongName.Text = string.Empty;
            this.SongThumbnail.Text = string.Empty;
            MusicModel.Add(song);
        }
        private void Choosed_Song(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            Song choosed = panel.Tag as Song;
            _currentIndex = this.MyListSong.SelectedIndex;
            Uri mp3Link = new Uri(choosed.link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.ListSong[_currentIndex].name + " - " + this.ListSong[_currentIndex].singer;
            //Play_Song();
        }
        //private void Play_Song()
        //{
        //    _isPlaying = true;
        //    this.Player_Status.Text = "Now Playing: ";
        //    this.MyPlayer.Play();
        //    this.Play_Button.Symbol = Symbol.Pause;
        //}

        //private void Pause_Song()
        //{
        //    _isPlaying = false;
        //    this.Player_Status.Text = "Paused: ";
        //    this.MyPlayer.Pause();
        //    this.Play_Button.Symbol = Symbol.Play;
        //}

        //private void Click_Play(object sender, RoutedEventArgs e)
        //{
        //    if (_isPlaying)
        //    {
        //        Pause_Song();
        //    }
        //    else
        //    {
        //        Play_Song();
        //    }
        //}

        //private void Do_Next(object sender, RoutedEventArgs e)
        //{
        //    Pause_Song();
        //    _currentIndex += 1;
        //    if (_currentIndex >= this.ListSong.Count)
        //    {
        //        _currentIndex = 0;
        //    }
        //    Uri mp3Link = new Uri(this.ListSong[_currentIndex].link);
        //    this.MyPlayer.Source = mp3Link;
        //    this.Song_Name.Text = this.ListSong[_currentIndex].name + " - " + this.ListSong[_currentIndex].singer;
        //    this.MyListSong.SelectedIndex = _currentIndex;
        //    Play_Song();
        //}

        //private void Do_Previous(object sender, RoutedEventArgs e)
        //{
        //    Pause_Song();
        //    _currentIndex -= 1;
        //    if (_currentIndex < 0)
        //    {
        //        _currentIndex = this.ListSong.Count - 1;
        //    }
        //    Uri mp3Link = new Uri(this.ListSong[_currentIndex].link);
        //    this.MyPlayer.Source = mp3Link;
        //    this.Song_Name.Text = this.ListSong[_currentIndex].name + " - " + this.ListSong[_currentIndex].singer;
        //    this.MyListSong.SelectedIndex = _currentIndex;
        //    Play_Song();
        //}

        //private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    string msg = String.Format("Current value: {0}", e.NewValue);
        //    this.time.Text = msg;

        //}
        //private void Slider_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        //{

        //}

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {

        }
    }
}
