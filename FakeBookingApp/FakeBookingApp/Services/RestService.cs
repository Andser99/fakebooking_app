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

        public static async Task<(int UserId, string ResultMessage)> PostLogin(string username, string password)
        {
            var parameterDict = new Dictionary<string, string>();
            parameterDict.Add("username", username);
            parameterDict.Add("password", password);

            var serializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(parameterDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "login");
            var result = await Client.PostAsync(uri, content);
            var resultText = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultText);

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultText);
            if (deserialized.ContainsKey("user_id"))
                return (Convert.ToInt32(deserialized["user_id"]), "Success");
            else if (deserialized.ContainsKey("error"))
            {
                return (-1, deserialized["error"]);
            }
            else
            {
                return (-1, "Nejde");
            }
        }

        public static async Task<(int UserId, string ResultMessage)> PostRegister(string username, string password, string passwordAgain)
        {
            var parameterDict = new Dictionary<string, string>();
            parameterDict.Add("username", username);
            parameterDict.Add("password", password);
            parameterDict.Add("password_again", passwordAgain);

            var serializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(parameterDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "register");
            var result = await Client.PostAsync(uri, content);
            var resultText = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultText);

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultText);
            if (deserialized.ContainsKey("user_id"))
                return (Convert.ToInt32(deserialized["user_id"]), "Success");
            else if (deserialized.ContainsKey("error"))
            {
                return (-1, deserialized["error"]);
            }
            else
            {
                return (-1, "Nejde");
            }
        }

        public static async Task<(int UserId, string ResultMessage)> PostReservation(string userId, string hotelId, string dateFrom, string dateTo)
        {
            var parameterDict = new Dictionary<string, string>();
            parameterDict.Add("user_id", userId);
            parameterDict.Add("hotel_id", hotelId);
            parameterDict.Add("date_from", dateFrom);
            parameterDict.Add("date_to", dateTo);

            var serializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(parameterDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "reservations");
            var result = await Client.PostAsync(uri, content);
            var resultText = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultText);

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultText);
            if (deserialized.ContainsKey("reservation_id"))
                return (Convert.ToInt32(deserialized["reservation_id"]), "Success");
            else if (deserialized.ContainsKey("error"))
            {
                return (-1, deserialized["error"]);
            }
            else
            {
                return (-1, "Nejde");
            }
        }

        public static async Task<DetailedHotelItem> GetHotel(HotelItem hotel)
        {

            Uri uri = new Uri(BaseAddress + "hotel/" + hotel.id);
            var result = await Client.GetStringAsync(uri);
            Console.WriteLine(result);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            var reviews = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(deserialized["reviews"]);
            DetailedHotelItem detailedHotelItem = new DetailedHotelItem();
            detailedHotelItem = (DetailedHotelItem)(hotel);
            detailedHotelItem.reviews = reviews;
            return detailedHotelItem;
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
