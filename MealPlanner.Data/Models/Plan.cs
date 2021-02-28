using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Shifts { get; set; }
        public DateTime EditableFrom { get; set; }
        public DateTime EditableTo { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public int MealId { get; set; } // added for usage in Unique Index
        public int CompanyId { get; set; } // added for usage in Unique Index
        public Company Company { get; set; }
        public Meal Meal { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
