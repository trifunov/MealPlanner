using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class MealImage
    {
        [Key]
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public string ImageBase64 { get; set; }
    }
}
