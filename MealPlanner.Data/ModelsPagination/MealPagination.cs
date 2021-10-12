using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.ModelsPagination
{
    public class MealPagination : PaginationBase
    {
        public List<Meal> Meals { get; set; }
    }
}
