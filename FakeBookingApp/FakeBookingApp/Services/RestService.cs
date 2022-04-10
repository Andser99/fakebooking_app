using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FakeBookingApp.Models;
using Newtonsoft.Json;

namespace FakeBookingApp.Services
{
    public static class RestService
    {
        public static HttpClient Client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(60),

        };

        public static string BaseAddress = "https://fakebooking.herokuapp.com/";

        public static async Task<int> PostLogin(string username, string password)
        {
            var parameterDict = new Dictionary<string, string>();
            parameterDict.Add("username", username);
            parameterDict.Add("password", password);

            var serializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(parameterDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "login");
            var result = await Client.PostAsync(uri, content);
            Console.WriteLine(result);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, List<HotelItem>>>(result);
            if (deserialized.HasKey("user_id"))
                return deserialized["user_id"];
            return deserialized["user_id"];
        }
        public static async Task<List<HotelItem>> GetHotels()
        {

            Uri uri = new Uri(BaseAddress + "hotels");
            var result = await Client.GetStringAsync(uri);
            Console.WriteLine(result);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, List<HotelItem>>>(result);
            return deserialized["list"];
        }

    }
}
