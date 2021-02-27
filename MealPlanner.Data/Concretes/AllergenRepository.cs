using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data.Concretes
{
    public class AllergenRepository : IAllergenRepository
    {
        private readonly MealPlannerContext _context;

        public AllergenRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public void Add(List<Allergen> allergens)
        {
            var notFoundAllergens = allergens.Where(x => _context.Allergens.FirstOrDefault(y => y.Name == x.Name) == null);
            var foundAllergens = _context.Allergens.Where(x => allergens.FirstOrDefault(y => y.Name == x.Name) != null);

            if (foundAllergens.Count() > 0)
            {
                allergens.ForEach(x => x.Id = (foundAllergens.FirstOrDefault(y => y.Name == x.Name) != null) ? foundAllergens.First(y => y.Name == x.Name).Id : 0);
            }

            if (notFoundAllergens.Count() > 0)
            {
                _context.Allergens.AddRange(notFoundAllergens);
                _context.SaveChanges();
            }
        }

        public void AddMealAllergens(List<MealAllergen> allergens)
        {
            _context.MealAllergens.AddRange(allergens);
            _context.SaveChanges();
        }

        public void DeleteMealAllergens(int mealId)
        {
            var meals = _context.MealAllergens.Where(x => x.MealId == mealId);

            if (meals.Count() > 0)
            {
                _context.MealAllergens.RemoveRange(meals);
                _context.SaveChanges();
            }
        }

        public List<Allergen> GetAll()
        {
            return _context.Allergens.ToList();
        }
    }
}
