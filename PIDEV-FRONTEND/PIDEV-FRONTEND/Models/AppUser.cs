using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class AppUser
    {
        public int userId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}