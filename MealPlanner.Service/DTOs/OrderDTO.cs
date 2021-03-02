using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsDelivered { get; set; }
        public int Shift { get; set; }
        public int MealId { get; set; }
        public MealDTO Meal { get; set; }
        public int EmployeeId { get; set; }
    }
}
