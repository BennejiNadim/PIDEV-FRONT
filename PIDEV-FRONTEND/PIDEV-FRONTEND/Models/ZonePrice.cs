using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class ZonePrice
    {
        public int zoneId { get; set; }

        public double pricem2 { get; set; }
        public string zone { get; set; }
        /*
        [ForeignKey("Announcement")]
        public int AnnouncementFk { get; set; }
        public Announcement Announcement { get; set; }
        */
    }
}