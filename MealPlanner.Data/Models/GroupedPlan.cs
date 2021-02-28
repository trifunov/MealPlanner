using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class GroupedPlan
    {
        public List<int> Ids { get; set; }
        public string Shifts { get; set; }
        public DateTime EditableFrom { get; set; }
        public DateTime EditableTo { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public int CompanyId { get; set; }
        public List<int> MealIds { get; set; }
        public int TotalMeals { get; set; }
    }
}
