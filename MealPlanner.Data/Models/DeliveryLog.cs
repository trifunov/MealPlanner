using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class DeliveryLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int SoftMealId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
