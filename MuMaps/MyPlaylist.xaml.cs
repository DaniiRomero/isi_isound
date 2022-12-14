using MuMaps.Helpers;
using MuMaps.Models;
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
using System.Windows.Shapes;

namespace MuMaps
{
    /// <summary>
    /// Lógica de interacción para MyPlaylist.xaml
    /// </summary>
    public partial class MyPlaylist : Window
    {
        public MyPlaylist()
        {
            InitializeComponent();
        }
        private void tbx_Artista_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbx_Artista.Text == string.Empty)
            {
                lst_Artistas.ItemsSource = null;
                return;
            }

            var result = SearchHelper.SearchArtistOrSong(tbx_Artista.Text);

            if (result == null)
            {
                return;
            }

            var listaArtistas = new List<SpotifyArtist>();

            foreach (var item in result.artists.items)
            {
                listaArtistas.Add(new SpotifyArtist()
                {
                    ID = item.id,
                    Image = item.images.Any() ? item.images[0].url : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png",
                    Name = item.name,
                    Followers = $"{item.followers.total.ToString("N")} seguidores"
                });
            }

            lst_Artistas.ItemsSource = listaArtistas;
        }
    }
}
