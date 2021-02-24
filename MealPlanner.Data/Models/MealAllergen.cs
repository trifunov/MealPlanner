using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class MealAllergen
    {
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public int AllergenId { get; set; }
        public Allergen Allergen { get; set; }
    }
}
