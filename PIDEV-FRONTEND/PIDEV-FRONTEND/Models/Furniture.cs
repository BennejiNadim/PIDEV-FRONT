using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIDEV_FRONTEND.Models
{
    public class Furniture
    {

        private String furnitureName { get; set; }
        private String fabricator { get; set; }
        private String category { get; set; }
        private double shippingPrice { get; set; }
        private int numberOfPieces { get; set; }
        private double FurniturePrice { get; set; }
        private Boolean verified { get; set; }
        private Boolean active { get; set; }
    }
}