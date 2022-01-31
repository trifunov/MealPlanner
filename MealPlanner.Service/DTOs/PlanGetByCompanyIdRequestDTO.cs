using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class PlanGetByCompanyIdRequestDTO
    {
        public int CompanyId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
