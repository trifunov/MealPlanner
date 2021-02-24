using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Rfid { get; set; }
        public string UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
