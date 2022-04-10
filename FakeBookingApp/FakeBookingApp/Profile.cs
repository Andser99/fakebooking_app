using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBookingApp
{
    class User
    {
        public static User CurrentUser = null;
        public int UserId = -1;

        public User (int id = -1)
        {

        }

        public string Login(string username, string password)
        {
            var res = "";



            return res;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
