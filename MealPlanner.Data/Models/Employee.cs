using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Rfid { get; set; }
        public string UserId { get; set; }
        public int CompanyId { get; set; }
        public ApplicationUser User { get; set; }
        public Company Company { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
