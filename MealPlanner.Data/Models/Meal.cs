using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }
        public ICollection<MealAllergen> MealAllergens { get; set; }
        public ICollection<MealIngredient> MealIngredients { get; set; }
    }
}
