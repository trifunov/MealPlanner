using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class PlanReport
    {
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public string MealName { get; set; }
        public int TotalOrders { get; set; }
        public int TotalDelivered { get; set; }
    }
}
