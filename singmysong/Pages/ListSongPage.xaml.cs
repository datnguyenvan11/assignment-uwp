using singmysong.Entity;
using singmysong.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace singmysong.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSongPage : Page
    {
        static ObservableCollection<Song> ListSong;
        static bool refresh = true;
        private ISongService _songService;
        private bool running = false;
        private int currentIndex = 0;
        private IFileService fileService;


        public ListSongPage()
        {

            this.Loaded += CheckMemberCredential;
            Debug.WriteLine(ProjectConfiguration.CurrentMemberCredential);

            this.InitializeComponent();
            this.fileService = new LocalFileService();
            this._songService = new SongService();
            LoadSongs();
            MyMediaElement.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            refresh = true;
        }




        private void LoadSongs()
        {
            if (refresh)
            {
                if (ProjectConfiguration.CurrentMemberCredential != null)
                {
                    var list = this._songService.GetAllSong(ProjectConfiguration.CurrentMemberCredential);
                    ListSong = new ObservableCollection<Song>(list);
                    refresh = false;
                }


            }
            else
            {
                Debug.WriteLine("Have all song");

            }
            ListViewSong.ItemsSource = ListSong;

        }

        private async void CheckMemberCredential(object sender, RoutedEventArgs e)
        {
            MemberCredential memberCredential = ProjectConfiguration.CurrentMemberCredential;
            if (ProjectConfiguration.CurrentMemberCredential == null)
            {
                memberCredential = await this.fileService.ReadMemberCredentialFromFile();
            }
            if (memberCredential == null)
            {
                this.Frame.Navigate(typeof(LoginPage));
            }


        }

        private void UIElement_OnDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Debug.WriteLine(ListViewSong.SelectedIndex);
            currentIndex = ListViewSong.SelectedIndex;
            var playIcon = sender as StackPanel;
            if (playIcon != null)
            {
                var currentSong = playIcon.Tag as Song;
                Debug.WriteLine(currentSong.name);
                MyMediaElement.Source = new Uri(currentSong.link);
                NowPlayingText.Text = currentSong.name + " - " + currentSong.singer;
                img.ImageSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(currentSong.thumbnail));
                AddressBar.Visibility = Visibility.Visible;
                backgroud.ImageSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(currentSong.thumbnail));
                volumeSlider.Value = 100;
            }
            MyMediaElement.Play();
            PlayAndPauseButton.Symbol = Symbol.Pause;
            running = true;



        }
        private void player1_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = MyMediaElement.NaturalDuration.TimeSpan;
            timelineSlider.Maximum = ts.TotalSeconds;
            timelineSlider.SmallChange = 1;
            timelineSlider.LargeChange = Math.Min(10, ts.Seconds / 10);
            timelineSlider.StepFrequency = 1;
        }


        private void timelineSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MyMediaElement.Position = TimeSpan.FromSeconds(timelineSlider.Value);

        }
        //private void player1_CurrentStateChanged(object sender, RoutedEventArgs e)
        //{
        //    timelineSlider.Value = MyMediaElement.Position.Milliseconds;
        //}

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            if (running)
            {
                MyMediaElement.Pause();
                PlayAndPauseButton.Symbol = Symbol.Play;
                running = false;
                timelineSlider.Value = timelineSlider.Value;
            }
            else
            {
                MyMediaElement.Play();
                PlayAndPauseButton.Symbol = Symbol.Pause;
                running = true;
                timelineSlider.Value = timelineSlider.Value;

            }
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            currentIndex += 1;
            if (currentIndex >= ListSong.Count)
            {
                currentIndex = 0;
            }
            var song = ListSong[currentIndex];
            ListViewSong.SelectedIndex = currentIndex;
            MyMediaElement.Source = new Uri(song.link);
            NowPlayingText.Text = song.name + " - " + song.singer;
            MyMediaElement.Play();
            running = true;
            PlayAndPauseButton.Symbol = Symbol.Pause;
            img.ImageSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(song.thumbnail));

        }
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;

            if (slider != null)
            {
                MyMediaElement.Volume = slider.Value / 100;
                if (slider.Value == 0)
                {
                    volume.Symbol = Symbol.Mute;

                }
                else
                {
                    volume.Symbol = Symbol.Volume;

                }

            }
        }
        private void Previous(object sender, RoutedEventArgs e)
        {
            currentIndex -= 1;
            if (currentIndex < 0)
            {
                currentIndex = ListSong.Count - 1;
            }
            var song = ListSong[currentIndex];
            ListViewSong.SelectedIndex = currentIndex;
            MyMediaElement.Source = new Uri(song.link);
            NowPlayingText.Text = song.name + " - " + song.singer;
            MyMediaElement.Play();
            running = true;
            PlayAndPauseButton.Symbol = Symbol.Pause;
            img.ImageSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(song.thumbnail));


        }

        private void Volume(object sender, RoutedEventArgs e)
        {
            if (volumeSlider.Value == 0)
            {
                MyMediaElement.Volume = 100;
                volumeSlider.Value = 100;
                volume.Symbol = Symbol.Volume;
            }
            else
            {
                MyMediaElement.Volume = 0;
                volumeSlider.Value = 0;
                volume.Symbol = Symbol.Mute;
            }


        }
    }
}
