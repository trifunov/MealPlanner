using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class MealDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameForeign { get; set; }
        public string ImageBase64 { get; set; }
        public int PlanId { get; set; }
        public List<CommonNameDTO> Ingredients { get; set; }
        public List<CommonNameDTO> Allergens { get; set; }
    }

    public class CommonNameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameForeign { get; set; }
    }
}
