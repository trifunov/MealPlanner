using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data.Concretes
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly MealPlannerContext _context;

        public IngredientRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public void Add(List<Ingredient> ingredients)
        {
            var notFoundIngredients = ingredients.Where(x => _context.Ingredients.FirstOrDefault(y => y.Name == x.Name) == null);
            var foundIngredients = _context.Ingredients.Where(x => ingredients.FirstOrDefault(y => y.Name == x.Name) != null);

            if (foundIngredients.Count() > 0)
            {
                ingredients.ForEach(x => x.Id = (foundIngredients.FirstOrDefault(y => y.Name == x.Name) != null) ? foundIngredients.First(y => y.Name == x.Name).Id : 0);
            }

            if (notFoundIngredients.Count() > 0)
            {
                _context.Ingredients.AddRange(notFoundIngredients);
                _context.SaveChanges();
            }
        }

        public void AddMealIngredients(List<MealIngredient> mealIngredients)
        {
            _context.MealIngredients.AddRange(mealIngredients);
            _context.SaveChanges();
        }

        public void DeleteMealIngredients(int mealId)
        {
            var meals = _context.MealIngredients.Where(x => x.MealId == mealId);

            if (meals.Count() > 0)
            {
                _context.MealIngredients.RemoveRange(meals);
                _context.SaveChanges();
            }
        }

        public List<Ingredient> GetAll()
        {
            return _context.Ingredients.ToList();
        }
    }
}
