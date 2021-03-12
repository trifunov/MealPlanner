using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class OrderDeliveryDTO
    {
        public int OrderId { get; set; }
        public bool IsDelivered { get; set; }
        public string Name { get; set; }
        public string NameForeign { get; set; }
        public string ImageBase64 { get; set; }
    }
}
