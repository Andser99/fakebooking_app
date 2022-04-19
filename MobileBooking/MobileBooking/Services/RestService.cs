using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MobileBooking.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileBooking.Services
{
    public static class RestService
    {
        public static HttpClient Client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(60),

        };

        public static string BaseAddress = "https://fakebooking.herokuapp.com";

        public static async Task<(int UserId, string ResultMessage)> PostLogin(string username, string password)
        {
            var parameterDict = new Dictionary<string, string>();
            parameterDict.Add("username", username);
            parameterDict.Add("password", password);

            var serializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(parameterDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "/login");
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

        public static async Task<string> PostStories(Stream stream)
        {
            Uri uri = new Uri(BaseAddress + "/story");

            using (var multipartFormContent = new MultipartFormDataContent())
            {
                //Add other fields
                multipartFormContent.Add(new StringContent(User.CurrentUser.Username), name: "username");

                //Add the file
                var fileStreamContent = new StreamContent(stream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                multipartFormContent.Add(fileStreamContent, name: "photo", fileName: "photo.jpg");

                //Send it
                var response = await Client.PostAsync(uri, multipartFormContent);

                var x = await response.Content.ReadAsStringAsync();

                return x;
            }
        }

        public static async Task<List<StoriesItem>> GetStories()
        {
            Uri uri = new Uri(BaseAddress + "/story");
            var result = await Client.GetStringAsync(uri);
            Console.WriteLine(result);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, List<StoriesItem>>>(result);
            Console.WriteLine(deserialized);
            if (deserialized.ContainsKey("list"))
            {
                return deserialized["list"];
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<ReservationItem>> GetReservations()
        {
            Uri uri = new Uri(BaseAddress + "/reservations/" + User.CurrentUser.UserId.ToString());
            var result = await Client.GetStringAsync(uri);

            Console.WriteLine(result);

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, List<ReservationItem>>>(result);
            Console.WriteLine(deserialized);

            if (deserialized.ContainsKey("list"))
            {
                return deserialized["list"];
            }
            else
            {
                return null;
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

            Uri uri = new Uri(BaseAddress + "/register");
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

        public static async Task<(int UserId, string ResultMessage)> PostReservations(string userId, string hotelId, string dateFrom, string dateTo)
        {
            var parameterDict = new Dictionary<string, string>();
            parameterDict.Add("user_id", userId);
            parameterDict.Add("hotel_id", hotelId);
            parameterDict.Add("date_from", dateFrom);
            parameterDict.Add("date_to", dateTo);

            var serializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(parameterDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "/reservations");
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

            Uri uri = new Uri(BaseAddress + "/hotel/" + hotel.id);
            var result = await Client.GetStringAsync(uri);
            Console.WriteLine(result);
            JObject jObject = JObject.Parse(result);
            JToken reviewsJson = jObject["reviews"];
            List<ReviewItem> reviews = reviewsJson.ToObject<List<ReviewItem>>();
            DetailedHotelItem detailedHotelItem = new DetailedHotelItem();
            detailedHotelItem.id = hotel.id;
            detailedHotelItem.name = hotel.name;
            detailedHotelItem.info = hotel.info;
            detailedHotelItem.image_path = hotel.image_path;
            detailedHotelItem.reviews = reviews;
            return detailedHotelItem;
        }

        public static async Task<string> GetHotelName(string hotel_id)
        {

            Uri uri = new Uri(BaseAddress + "/hotel/" + hotel_id);
            var result = await Client.GetStringAsync(uri);
            Console.WriteLine(result);
            JObject jObject = JObject.Parse(result);
            JToken hotelName_token = jObject["name"];
            string hotel_name = hotelName_token.ToObject<string>();
            return hotel_name;
        }

        public static async Task<List<HotelItem>> GetHotels()
        {

            Uri uri = new Uri(BaseAddress + "/hotels");
            var result = await Client.GetStringAsync(uri);
            Console.WriteLine(result);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, List<HotelItem>>>(result);
            return deserialized["list"];
        }

        public static async Task<(ReviewItem Review, string Message)> GetReview(int reviewId)
        {
            Uri uri = new Uri(BaseAddress + "/review/" + reviewId);
            var response = await Client.GetAsync(uri);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var deserialized = JsonConvert.DeserializeObject<ReviewItem>(result);
                return (deserialized, "ok");
            }
            else
            {
                var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return (null, deserialized["error"]);
            }
        }

        /// <summary>
        /// Returns the review ID as string if deletion was successful (http code 200)
        /// otherwise returns the content of the error message
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public static async Task<string> DeleteReview(int reviewId)
        {
            Uri uri = new Uri(BaseAddress + "/review/" + reviewId);
            var result = await Client.DeleteAsync(uri);
            var resultString = await result.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultString);
                return "Review successfully removed";
            }
            else
            {
                var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultString);
                return deserialized["error"];
            }
        }

        public static async Task<string> PostReview(ReviewItem review)
        {
            var serializer = new JsonSerializer();
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            paramDict.Add("reservation_id", review.hotel_id);
            paramDict.Add("stars", review.stars);
            paramDict.Add("text", review.text);
            string json = JsonConvert.SerializeObject(paramDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "/review");
            var result = await Client.PostAsync(uri, content);
            var resultString = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultString);
                return "Review created successfully";
            }
            else
            {
                return resultString;
            }
        }

        public static async Task<string> PutReview(ReviewItem review)
        {
            var serializer = new JsonSerializer();
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            paramDict.Add("review_id", review.id);
            paramDict.Add("stars", review.stars);
            paramDict.Add("text", review.text);
            string json = JsonConvert.SerializeObject(paramDict, Formatting.None);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri(BaseAddress + "/review");
            var result = await Client.PutAsync(uri, content);
            var resultString = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultString);
                return "Review updated successfully";
            }
            else
            {
                return resultString;
            }
        }
    }
}
