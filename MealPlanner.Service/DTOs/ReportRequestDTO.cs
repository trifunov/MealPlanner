using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class ReportRequestDTO
    {
        public int CompanyId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
