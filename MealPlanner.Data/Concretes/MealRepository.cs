using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data.Concretes
{
    public class MealRepository : IMealRepository
    {
        private readonly MealPlannerContext _context;

        public MealRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public void Add(Meal meal)
        {
            _context.Meals.Add(meal);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var meal = _context.Meals.Find(id);

            if (meal != null)
            {
                _context.Meals.Remove(meal);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Meal not found");
            }
        }

        public List<Meal> GetAll()
        {
            return _context.Meals.Include(x => x.MealAllergens).ThenInclude(x => x.Allergen).Include(x => x.MealIngredients).ThenInclude(x => x.Ingredient).ToList();
        }

        public Meal GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Meal> GetValid(int companyId, int shift, DateTime date)
        {
            var mealIds = _context.Plans.Where(x => x.CompanyId == companyId && x.Shifts.Contains(shift.ToString()) && date >= x.ActiveFrom && date <= x.ActiveTo).Select(x => x.MealId).Distinct();
            return _context.Meals.Where(x => mealIds.Contains(x.Id)).Include(x => x.MealAllergens).ThenInclude(x => x.Allergen).Include(x => x.MealIngredients).ThenInclude(x => x.Ingredient).ToList();
        }

        public void Update(Meal mealInput)
        {
            var meal = _context.Meals.Find(mealInput.Id);

            if (meal != null)
            {
                meal.Name = mealInput.Name;
                meal.NameForeign = mealInput.NameForeign;
                meal.ImageBase64 = mealInput.ImageBase64;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Meal not found");
            }
        }
    }
}
