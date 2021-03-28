using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class OrderFilteredResponseDTO
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Employee { get; set; }
        public string Meal { get; set; }
        public bool IsDelivered { get; set; }
    }
}
