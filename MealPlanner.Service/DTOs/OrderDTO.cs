using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public bool IsDelivered { get; set; }
        public int Shift { get; set; }
        public int PlanId { get; set; }
        public PlanDTO Plan { get; set; }
        public int EmployeeId { get; set; }
    }
}
