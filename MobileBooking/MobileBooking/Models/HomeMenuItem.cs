using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBooking.Models
{
    public enum MenuItemType
    {
        Home,
        VideoCalls,
        MyReservations,
        Stories,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
