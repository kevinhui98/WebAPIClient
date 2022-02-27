using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class crypto
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        public List<Markets> Markets { get; set; }
    }
    public class Marketname
    {
        [JsonProperty("market")]
        public string Market { get; set; }
        [JsonProperty("price")]
        public string price { get; set; }

    }
    public class Markets
    {
        [JsonProperty("market")]
        public Marketname Market; 
    }
    class MainClass
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await Processrepositories();

        }
        private static async Task Processrepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter crypto name. ");
                    var cryptoName = Console.ReadLine();
                    if (cryptoName == null)
                    {
                        break;
                    }
                    //issues with getting cryto name. 
                    //after submitting btc or btc-usd both said incorrect crypto
                    var result = await client.GetAsync("https://api.cryptonator.com/api/full/" + cryptoName);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var crypto = JsonConvert.DeserializeObject<crypto>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("CryptoCurrency: " + crypto.Base);
                    Console.WriteLine("Price: " + crypto.Price);
                    Console.WriteLine("Market(s): ");
                    //trying to print multiple items from markets
                    crypto.Markets.ForEach(t =>
                        //Console.WriteLine(t.Marketname.market);
                        Console.WriteLine(t.Market.Market));
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR invalid crypto!!");
                }
            }
        }
    }
}