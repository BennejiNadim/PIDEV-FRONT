using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
	public class Interest
	{
		public int id { get; set; }
		public DateTime date { get; set; }
		public Boolean accepted { get; set; }
		/*
        [ForeignKey("Announcement")]
        public int AnnouncementFk { get; set; }
        public Announcement Announcement { get; set; }
        */
	}
}