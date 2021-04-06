using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class ReportResponseDTO
    {
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string MealName { get; set; }
        public int TotalOrders { get; set; }
    }
}
