using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class MealPaginationDTO : PaginationBaseDTO
    {
        public List<MealDTO> Meals { get; set; }
    }
}
