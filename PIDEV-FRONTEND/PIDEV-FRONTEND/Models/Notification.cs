using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class Notification
    {
        public int notificationId { get; set; }
        public string notifType { get; set; }
        public Boolean isRead { get; set; }
        public DateTime notificationDate { get; set; }
        /*
        [ForeignKey("Announcement")]
        public int AnnouncementFk { get; set; }
        public Announcement Announcement { get; set; }
        */
    }
}