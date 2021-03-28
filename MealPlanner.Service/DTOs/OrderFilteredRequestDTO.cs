using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class OrderFilteredRequestDTO
    {
        public List<int> EmployeeIds { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
