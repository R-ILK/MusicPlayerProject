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

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAlbum_Click(object sender, RoutedEventArgs e)
        {
            AlbumsView.Visibility = Visibility.Visible;
            PlaylistsView.Visibility = Visibility.Collapsed;
        }

        private void btnPlaylists_Click(object sender, RoutedEventArgs e)
        {
            AlbumsView.Visibility = Visibility.Collapsed;
            PlaylistsView.Visibility = Visibility.Visible;
        }

        private bool isPlaying = false;

        private void btnPlayStop_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                PlayStopIcon.Text = "▶";
                isPlaying = false;
            }
            else
            {
                PlayStopIcon.Text = "||";
                isPlaying = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tracks = new List<Track>
        {
            new Track
            {
                Cover = "/../../images/test.jpg",
                Title = "Bohemian Rhapsody",
                Artist = "Queen",
                Length = "5:55"
            },
            new Track
            {
                Cover = "",
                Title = "Hotel California",
                Artist = "Eagles",
                Length = "6:30"
            },
            new Track
            {
                Cover = "",
                Title = "Stairway to Heaven",
                Artist = "Led Zeppelin",
                Length = "8:02"
            },
            new Track
            {
                Cover = "",
                Title = "Imagine",
                Artist = "John Lennon",
                Length = "3:04"
            },
            new Track
            {
                Cover = "",
                Title = "Smells Like Teen Spirit",
                Artist = "Nirvana",
                Length = "4:38"
            }
        };

            lstvAlbums.ItemsSource = tracks;
            lstvwPlaylist.ItemsSource = tracks;
        }
    }
}
