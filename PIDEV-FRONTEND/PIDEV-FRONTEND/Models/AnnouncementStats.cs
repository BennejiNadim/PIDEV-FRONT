using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
	public class AnnouncementStats
	{
		public int count { get; set; }
		public string sum { get; set; }
		public double min { get; set; }
		public double max { get; set; }
		public double average { get; set; }

	}
}