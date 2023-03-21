using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.ModelsPagination
{
    public class EmployeePagination : PaginationBase
    {
        public List<EmployeeJoined> Employees { get; set; }
    }
}
