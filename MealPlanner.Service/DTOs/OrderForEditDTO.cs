using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class OrderForEditDTO
    {
        public DateTime Date { get; set; }
        public int Shift { get; set; }
        public int PlanId { get; set; }
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
    }
}
