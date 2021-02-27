using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IAllergenRepository
    {
        void Add(List<Allergen> allergens);
        void AddMealAllergens(List<MealAllergen> allergens);
        void DeleteMealAllergens(int mealId);
        List<Allergen> GetAll();
    }
}
