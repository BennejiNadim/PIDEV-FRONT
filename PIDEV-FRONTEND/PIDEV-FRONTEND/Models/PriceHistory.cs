using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
	public class PriceHistory
	{
		public int id { get; set; }
		public double price { get; set; }
		public string zone { get; set; }
		/*
        [ForeignKey("Announcement")]
        public int AnnouncementFk { get; set; }
        public Announcement Announcement { get; set; }
        */
	}
}