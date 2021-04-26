using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class SaleHistory
    {
        public int id { get; set; }
        public DateTime saleDate { get; set; }
        /*
        [ForeignKey("Announcement")]
        public int AnnouncementFk { get; set; }
        public Announcement Announcement { get; set; }
        */
    }
}