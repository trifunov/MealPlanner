using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }

        public int TotalEmployees { get; set; }
    }
}
