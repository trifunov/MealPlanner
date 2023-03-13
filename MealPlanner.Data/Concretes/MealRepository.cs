using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Data.ModelsPagination;
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

        public void Add(Meal meal, string image)
        {
            _context.Meals.Add(meal);
            _context.SaveChanges();

            if(meal.Id > 0)
            {
                _context.MealImages.Add(new MealImage { MealId = meal.Id, ImageBase64 = image });
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var meal = _context.Meals.Find(id);

            if (meal != null)
            {
                _context.Meals.Remove(meal);

                var mealImage = _context.MealImages.Find(meal.Id);
                if (mealImage != null)
                {
                    _context.MealImages.Remove(mealImage);
                }

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Meal not found");
            }
        }

        public MealPagination GetAll(string mealName, int page, int itemsPerPage, bool paged)
        {
            var meals = _context.Meals.Where(x => x.Name.Contains(mealName)).Include(x => x.MealAllergens).ThenInclude(x => x.Allergen).Include(x => x.MealIngredients).ThenInclude(x => x.Ingredient);

            if (paged)
            {
                return new MealPagination
                {
                    TotalRows = meals.Count(),
                    Meals = meals.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList()
                };
            }
            else
            {
                return new MealPagination
                {
                    TotalRows = meals.Count(),
                    Meals = meals.ToList()
                };
            }
        }

        public Meal GetById(int id)
        {
            var meal = _context.Meals.Include(x => x.MealImage).Include(x => x.MealAllergens).ThenInclude(x => x.Allergen).Include(x => x.MealIngredients).ThenInclude(x => x.Ingredient).FirstOrDefault(x => x.Id == id);

            if (meal != null)
            {
                return meal;
            }
            else
            {
                throw new Exception("Meal not found");
            }
        }

        public List<MealJoined> GetValid(int companyId, int shift, DateTime date)
        {
            var mealIds = _context.Plans.Where(x => x.CompanyId == companyId && x.Shifts.Contains(shift.ToString()) && date == x.Date).Select(x => x.MealId).Distinct().ToList();
            var meals = _context.Meals.Include(x => x.Plans).Include(x => x.MealImage).Include(x => x.MealAllergens).ThenInclude(x => x.Allergen).Include(x => x.MealIngredients).ThenInclude(x => x.Ingredient).Where(x => mealIds.Contains(x.Id)).ToList();

            var result = meals.Select( 
                      meal => new MealJoined
                      {
                          Id = meal.Id,
                          ImageBase64 = meal.MealImage.ImageBase64,
                          Name = meal.Name,
                          NameForeign = meal.NameForeign,
                          MealAllergens = meal.MealAllergens,
                          MealIngredients = meal.MealIngredients,
                          PlanId = meal.Plans.First(x => x.CompanyId == companyId && x.Shifts.Contains(shift.ToString()) && date == x.Date).Id
                      });

            return result.ToList();
        }

        public void Update(Meal mealInput, string image)
        {
            var meal = _context.Meals.Find(mealInput.Id);

            if (meal != null)
            {
                meal.Name = mealInput.Name;
                meal.NameForeign = mealInput.NameForeign;

                var mealImage = _context.MealImages.Find(mealInput.Id);
                if(mealImage != null)
                {
                    mealImage.ImageBase64 = image;
                }

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Meal not found");
            }
        }
    }
}
