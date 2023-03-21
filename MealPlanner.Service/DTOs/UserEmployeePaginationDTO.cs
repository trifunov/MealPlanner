using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class UserEmployeePaginationDTO : PaginationBaseDTO
    {
        public List<UserEmployeeDTO> UserEmployees { get; set; }
    }
}
