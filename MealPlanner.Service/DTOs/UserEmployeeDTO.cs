using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class UserEmployeeDTO : EmployeeDTO
    {
        public string Username { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
