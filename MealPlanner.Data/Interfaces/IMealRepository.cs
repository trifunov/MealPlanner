using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IMealRepository
    {
        void Add(Meal meal);
        void Delete(int id);
        void Update(Meal meal);
        Meal GetById(int id);
        List<Meal> GetAll();
        List<MealJoined> GetValid(int companyId, int shift, DateTime date);
    }
}
