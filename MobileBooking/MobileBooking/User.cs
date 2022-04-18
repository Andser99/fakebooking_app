using MobileBooking.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileBooking
{
    class User
    {
        public static User CurrentUser = null;
        public int UserId = -1;
        public string Username = "";

        public User (int id = -1, string name = "")
        {
            UserId = id;
            Username = name;
        }

        public static async Task<string> Login(string username, string password)
        {
            var res = await RestService.PostLogin(username, password);
            if (res.UserId != -1)
            {
                CurrentUser = new User(res.UserId, username);
            }
            return res.ResultMessage;
        }

        public static async Task<string> Register(string username, string password, string passwordAgain)
        {
            var res = await RestService.PostRegister(username, password, passwordAgain);
            if (res.UserId != -1)
            {
                CurrentUser = new User(res.UserId, username);
            }
            return res.ResultMessage;
        }

        public void Logout()
        {
            CurrentUser = null;
            UserId = -1;
            Username = "";
        }
    }
}
