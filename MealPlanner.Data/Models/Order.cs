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
        public bool IsDelivered { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public int PlanId { get; set; }
        public int Shift { get; set; }
        public int EmployeeId { get; set; }
        public Plan Plan { get; set; }
        public Employee Employee { get; set; }
    }
}
