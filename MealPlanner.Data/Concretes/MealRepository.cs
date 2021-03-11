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

        public List<MealJoined> GetValid(int companyId, int shift, DateTime date)
        {
            var mealIds = _context.Plans.Where(x => x.CompanyId == companyId && x.Shifts.Contains(shift.ToString()) && date == x.Date).Select(x => x.MealId).Distinct().ToList();
            var meals = _context.Meals.Include(x => x.Plans).Include(x => x.MealAllergens).ThenInclude(x => x.Allergen).Include(x => x.MealIngredients).ThenInclude(x => x.Ingredient).Where(x => mealIds.Contains(x.Id)).ToList();

            var result = meals.Select( 
                      meal => new MealJoined
                      {
                          Id = meal.Id,
                          ImageBase64 = meal.ImageBase64,
                          Name = meal.Name,
                          NameForeign = meal.NameForeign,
                          MealAllergens = meal.MealAllergens,
                          MealIngredients = meal.MealIngredients,
                          PlanId = meal.Plans.First(x => x.Date == date).Id
                      });

            return result.ToList();
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
