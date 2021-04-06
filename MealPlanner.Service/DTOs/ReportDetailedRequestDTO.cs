using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class ReportDetailedRequestDTO
    {
        public int CompanyId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int Delivered { get; set; }

        public int Shift { get; set; }
    }
}
