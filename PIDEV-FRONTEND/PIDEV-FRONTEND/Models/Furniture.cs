using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class Furniture
    {

        public int furnitureId { get; set; }
        public String furnitureName { get; set; }
        public String fabricator { get; set; }
        public String category { get; set; }
        public double shippingPrice { get; set; }
        public int numberOfPieces { get; set; }
        public double FurniturePrice { get; set; }
        public Boolean verified { get; set; }
        public Boolean active { get; set; }

    }
}

