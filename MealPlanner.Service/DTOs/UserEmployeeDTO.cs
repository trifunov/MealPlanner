using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class UserEmployeeDTO : EmployeeDTO
    {
        public string Username { get; set; }
        public string CompanyName { get; set; }
    }
}
