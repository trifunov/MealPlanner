using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IIngredientRepository
    {
        void Add(List<Ingredient> ingredients);
        void AddMealIngredients(List<MealIngredient> mealIngredients);
        void DeleteMealIngredients(int mealId);
        List<Ingredient> GetAll();
    }
}
