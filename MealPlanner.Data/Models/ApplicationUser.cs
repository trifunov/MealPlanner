using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Employee Employee { get; set; }
    }
}
