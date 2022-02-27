using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
   
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
                    Console.WriteLine("Enter a name. ");
                    var genderName = Console.ReadLine();
                    if (genderName == null)
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://api.genderize.io/?name=" + genderName.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var sex = JsonConvert.DeserializeObject<sex>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Name: " + sex.name);
                    Console.WriteLine("Gender: " + sex.gender);
                    Console.WriteLine("Probability: " + sex.probability);
                    Console.WriteLine("Count: " + sex.count);
                    Console.WriteLine("\n---");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR invalid name!!");
                    break; 
                }
            }
        }
    }
    class sex
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("gender")]
        public string gender { get; set; }
        [JsonProperty("probability")]
        public string probability { get; set; }
        [JsonProperty("count")]
        public string count { get; set; }
    }
}