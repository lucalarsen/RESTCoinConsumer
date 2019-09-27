using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestCoinConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //alle coins i listen
            IList<Coin> cList = GetCoinsAsync().Result;
            Console.WriteLine(string.Join("\n", cList.ToString()));
            //Fast write out
            for (int i = 0; i < cList.Count; i++)
                Console.WriteLine(cList[i].ToString());
            Console.WriteLine();
            Console.WriteLine("________________________");


            ////specific id på customer
            //Console.WriteLine("Angiv et id");

            //string idStr = Console.ReadLine();
            //int id = int.Parse(idStr);
            //Coin coin = GetOneCoinAsync(id).Result;
            ////Customer customer = TaskController.GetOneCustomerAsync(id).Result;
            //Console.WriteLine(coin.ToString());
            //Console.WriteLine();

            //Insert customer
            Console.WriteLine("Genstand navn: ");
            String genstand = Console.ReadLine();
            Console.WriteLine("Dit bud: ");
            String budstr = Console.ReadLine();
            int bud = Int32.Parse(budstr);
            Console.WriteLine("Dit navn: ");
            String navn = Console.ReadLine();


            Coin newCoin = new Coin(genstand, bud, navn);
            Coin coin = AddCustomerAsync(newCoin).Result;
            Console.WriteLine("Customer inserted");
            Console.WriteLine(coin.ToString());

            Console.WriteLine("________________________");
            //alle coins i listen
            IList<Coin> cList2 = GetCoinsAsync().Result;
            Console.WriteLine(string.Join("\n", cList2.ToString()));
            //Fast write out
            for (int i = 0; i < cList2.Count; i++)
                Console.WriteLine(cList2[i].ToString());
            Console.WriteLine();
            Console.WriteLine("________________________");

            Console.ReadLine();
        }

        //ISS express
        private static string CoinsUri = "https://localhost:44311/api/coin/";
        // webservice
        //private static string CoinsUri = "https://restcoinservicelucalarsen.azurewebsites.net/api/coin/";
        public static async Task<IList<Coin>> GetCoinsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CoinsUri);
                IList<Coin> cList = JsonConvert.DeserializeObject<IList<Coin>>(content);
                return cList;
            }
        }
        public static async Task<Coin> GetOneCoinAsync(int id)
        {
            string requestUri = CoinsUri + id;
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(requestUri);
                Coin c = JsonConvert.DeserializeObject<Coin>(content);
                return c;
            }
        }

        public static async Task<Coin> AddCustomerAsync(Coin newCustomer)
        {
            using (HttpClient client = new HttpClient())
            {

                var jsonString = JsonConvert.SerializeObject(newCustomer);
                Console.WriteLine("JSON: " + jsonString);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(CoinsUri, content);
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("Customer already exists. Try another id");
                }
                response.EnsureSuccessStatusCode();
                string str = await response.Content.ReadAsStringAsync();
                Coin copyOfNewCustomer = JsonConvert.DeserializeObject<Coin>(str);
                return copyOfNewCustomer;
            }

        }
    }
}
