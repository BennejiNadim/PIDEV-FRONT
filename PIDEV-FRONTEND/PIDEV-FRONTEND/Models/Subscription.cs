using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class Subscription
    {
        public int subscriptionId { get; set; }
        public int amount { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string subType { get; set; }
        public bool isactive { get; set; }
    }
}