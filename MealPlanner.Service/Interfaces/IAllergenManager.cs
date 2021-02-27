using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IAllergenManager
    {
        List<CommonNameDTO> GetAll();
    }
}
