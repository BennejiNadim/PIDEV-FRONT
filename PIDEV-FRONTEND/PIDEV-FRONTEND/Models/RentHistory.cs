using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class RentHistory
    {
        public int id { get; set; }
        public DateTime rentDate { get; set; }
        /*
        [ForeignKey("Announcement")]
        public int AnnouncementFk { get; set; }
        public Announcement Announcement { get; set; }
        */
    }
}