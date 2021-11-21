using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class SoftMealDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImageBase64 { get; set; }
        public string Name { get; set; }
        public string NameForeign { get; set; }
        public ICollection<SoftMeal> SoftMeals { get; set; }
    }
}
