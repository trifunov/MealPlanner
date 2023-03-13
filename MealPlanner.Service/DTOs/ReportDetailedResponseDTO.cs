using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class ReportDetailedResponseDTO
    {
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string MealName { get; set; }
        public string Username { get; set; }
        public string Rfid { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}
