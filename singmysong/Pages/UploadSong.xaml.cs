using singmysong.Entity;
using singmysong.Service;
using System;
using System.Collections.Generic;
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
    public sealed partial class UploadSong : Page
    {
        private SongService songService;
        private LocalFileService LocalFileService;
        public UploadSong()
        {
            this.songService = new SongService();
            this.LocalFileService = new LocalFileService();
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var song = new Song()
            {
                name = this.Name.Text,
                description = this.Description.Text,
                singer = this.Singer.Text,
                author = this.Arthor.Text,
                link = this.Link.Text,
                thumbnail = this.Thumbnail.Text,
            };
            songService.CreateSong(ProjectConfiguration.CurrentMemberCredential, song);

        }
    }
}
