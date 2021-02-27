using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IMealManager
    {
        void Add(MealDTO mealDto);
        void Delete(int id);
        void Update(MealDTO mealDto);
        MealDTO GetById(int id);
        List<MealDTO> GetAll();
    }
}
