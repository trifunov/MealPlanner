using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class OrderRfidDTO
    {
        public string Rfid { get; set; }
        public DateTime Date { get; set; }
        public int Shift { get; set; }
    }
}
