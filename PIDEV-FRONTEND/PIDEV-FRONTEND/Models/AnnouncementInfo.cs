using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
	public class AnnouncementInfo
	{
		public int AnnouncementId { get; set; }
		public string type { get; set; }
		public string estateType { get; set; }
		public string location { get; set; }
		public double price { get; set; }
		public string description { get; set; }
		public double surface { get; set; }
		public int numberOfFloors { get; set; }
		public int numberOfRooms { get; set; }
		public DateTime dateCreated { get; set; }
		public DateTime datePublished { get; set; }
		public AnnouncementStats stats { get; set; }
		public HashSet<PriceHistory> priceHistories { get; set; }

	}
}