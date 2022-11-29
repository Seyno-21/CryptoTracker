using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using CryptoTrackerV2.Model;
using System.Xml.Serialization;
using System.Collections;
using static System.Net.WebRequestMethods;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;


namespace CryptoTrackerV2
{
    public partial class MainPage : ContentPage
    {
        private string ApiKey = "0E6679CE-2503-4765-BFEC-ADF87BAF68F0";
        private string Image = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_256/";
        public MainPage()
        {
            InitializeComponent();
            coinList.ItemsSource = GetCoins();

        }

        private List<Coin> GetCoins()
        {
            List<Coin> coins;

            var client = new RestClient("http://rest.coinapi.io/v1/assets/BTC;ETH;DOGE;LTC;NMC;VEN;XRP;NVC;PPC;TRC;TOR;KST;XMR");
            var request = new RestRequest();
            request.AddHeader("X-CoinAPI-Key", ApiKey);
            var response = client.Execute(request);

            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);

            foreach (var c in coins)
            {
                if (!string.IsNullOrEmpty(c.Id_icon))
                {
                    c.Icon_url = Image + c.Id_icon.Replace("-", "") + ".png";
                }
                else
                {
                    c.Icon_url = "";
                }
            }
            return coins;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            coinList.ItemsSource = GetCoins();
        }
    }
}