using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class PlanPaginationDTO : PaginationBaseDTO
    {
        public List<PlanDTO> Plans { get; set; }
    }
}
