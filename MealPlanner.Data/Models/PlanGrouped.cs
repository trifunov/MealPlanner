using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class PlanGrouped
    {
        public List<int> Ids { get; set; }
        public string Shifts { get; set; }
        public DateTime EditableFrom { get; set; }
        public DateTime EditableTo { get; set; }
        public DateTime Date { get; set; }
        public int CompanyId { get; set; }
        public List<Meal> Meals { get; set; }
        //public int TotalMeals { get; set; }
    }
}
