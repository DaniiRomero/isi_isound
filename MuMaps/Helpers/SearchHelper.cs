using MuMaps.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using static MuMaps.Models.HereGeoSearch;
using static MuMaps.Models.HereRouSearch;
using static MuMaps.Models.SpotifySearch;

namespace MuMaps.Helpers
{
    public static class SearchHelper
    {
        public static SpotifyToken Stoken { get; set; }
        public static string Htoken = "lCDUb5Ol6Xos1uRifLp-DFTlgbuXjyMDn73PSETQXhc";

        public static async Task GetTokenAsync() 
        {
            #region SecretVault
            string clientID = "d6c62bd2908e49a68a29c204b9c716e5";

            string clientSecret = "7af3b3575a264224abfc9f22c380bbfb";
            #endregion

            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientID + ":" + clientSecret));

            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
            HttpContent content = new FormUrlEncodedContent(args);

            HttpResponseMessage resp = await client.PostAsync("https://accounts.spotify.com/api/token", content);
            string msg = await resp.Content.ReadAsStringAsync();

            Stoken = JsonConvert.DeserializeObject<SpotifyToken>(msg);
        }

        public static SpotifyResult SearchArtistOrSong(string searchWord) 
        {
            var client = new RestClient("https://api.spotify.com/v1/search");
            client.AddDefaultHeader("Authorization", $"Bearer {Stoken.access_token}");
            var request = new RestRequest($"?q={searchWord}&type=artist", Method.GET);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<SpotifyResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
                
        }
    
        public static HereGeoResult SearchCity(string place)
    {  
        var client = new RestClient("https://geocode.search.hereapi.com/v1/geocode");
        var request = new RestRequest($"?q={place}&apikey={Htoken}", Method.GET);
        var response = client.Execute(request);

        if (response.IsSuccessful)
        {
            HereGeoResult result = JsonConvert.DeserializeObject<HereGeoResult>(response.Content);
            return result;
        }
        else
        {
            return null;
        }
    }
        public static HereRouResult generateRoute(double lat1, double lng1, double lat2, double lng2, string transport)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var client = new RestClient("https://router.hereapi.com/v8/routes");
           
            var request = new RestRequest($"?transportMode={transport}&lang=es&origin={lat1},{lng1}&destination={lat2},{lng2}&return=polyline,actions,instructions,summary&apikey={Htoken}", Method.GET);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                HereRouResult result = JsonConvert.DeserializeObject<HereRouResult>(response.Content);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
