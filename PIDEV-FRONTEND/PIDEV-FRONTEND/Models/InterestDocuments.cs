using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
	public class InterestDocuments
	{
		public int interestId { get; set; }
		public DateTime date { get; set; }
		public Boolean accepted { get; set; }
		public string email { get; set; }
		public string payslip { get; set; }
		public string idCard { get; set; }
		public string engagementLetter { get; set; }
		public string depositProof { get; set; }

	}
}