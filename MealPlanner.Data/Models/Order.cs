using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsDelivered { get; set; }
        public int MealId { get; set; }
        public int Shift { get; set; }
        public int EmployeeId { get; set; }
        public Meal Meal { get; set; }
        public Employee Employee { get; set; }
    }
}
