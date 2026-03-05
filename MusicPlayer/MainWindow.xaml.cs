using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TracksData db = new TracksData();

        private const string ClientID = "476dfd51";

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

        private async Task FetchTracks()
        {
            using (var client = new HttpClient())
            {
                string url = $"https://api.jamendo.com/v3.0/tracks/" +
                             $"?client_id={ClientID}" +
                             $"&format=json" +
                             $"&limit=20" +
                             $"&imagesize=200";

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);
                JArray results = (JArray)data["results"];

                foreach (var item in results)
                {
                    int durationSec = item["duration"]?.Value<int>() ?? 0;
                    string length = $"{durationSec / 60}:{(durationSec % 60):D2}";

                    var track = new Track
                    {
                        Cover = item["image"]?.ToString() ?? "",
                        Title = item["name"]?.ToString() ?? "Unknown",
                        Artist = item["artist_name"]?.ToString() ?? "Unknown",
                        Length = length,
                        AudioUrl = item["audio"]?.ToString() ?? ""
                    };

                    db.Tracks.Add(track);
                }

                await db.SaveChangesAsync();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!db.Tracks.Any())
            {
                try
                {
                    await FetchTracks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to fetch tracks from Jamendo:\n{ex.Message}",
                                    "Network Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            // Always load from database
            var tracks = db.Tracks.ToList();
            lstvAlbums.ItemsSource = tracks;
            lstvwPlaylist.ItemsSource = tracks;

        }
    }
}
