using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.ModelsPagination
{
    public class PlanGroupedPagination : PaginationBase
    {
        public List<PlanGrouped> Plans { get; set; }
    }
}
