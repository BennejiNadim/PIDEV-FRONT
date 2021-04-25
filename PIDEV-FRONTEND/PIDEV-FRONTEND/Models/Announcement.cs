using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class Announcement
    {
        public int announcementId { get; set; }
        public string location { get; set; }
        public string estateType { get; set; }
        public double price { get; set; }
        public string descritpion { get; set; }
        public string numberOfRooms { get; set; }
    }
}