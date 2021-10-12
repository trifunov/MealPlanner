using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.DTOs
{
    public class PlanDTO
    {
        public List<int> Ids { get; set; }
        public List<int> Shifts { get; set; }
        public List<string> ShiftNames { get; set; }
        public DateTime EditableFrom { get; set; }
        public DateTime EditableTo { get; set; }
        public DateTime Date { get; set; }
        public int CompanyId { get; set; }
        public List<int> MealIds { get; set; }
        public List<MealDTO> Meals { get; set; }
    }
}
