using FakeBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FakeBookingApp
{
    class User
    {
        public static User CurrentUser = null;
        public int UserId = -1;

        public User (int id = -1)
        {
            UserId = id;
        }

        public static async Task<string> Login(string username, string password)
        {
            var res = await RestService.PostLogin(username, password);
            if (res.UserId != -1)
            {
                CurrentUser = new User(res.UserId);
            }
            return res.ResultMessage;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
