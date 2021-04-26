using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
	public class Criteria
	{
		public int criteriaId { get; set; }
		public string announcementType { get; set; }
		public string criteriaName { get; set; }
		public string estateType { get; set; }
		public string location { get; set; }
		public double minPrice { get; set; }
		public double maxPrice { get; set; }
		public double minSurface { get; set; }
		public double maxSurface { get; set; }
		public int minNumberOfFloors { get; set; }
		public int maxNumberOfFloors { get; set; }
		public int minNumberOfRooms { get; set; }
		public int maxNumberOfRooms { get; set; }
	}
}