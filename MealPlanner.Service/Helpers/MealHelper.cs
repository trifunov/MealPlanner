using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Helpers
{
    public static class MealHelper
    {
        public static List<MealDTO> ResultMeals(List<Meal> meals)
        {
            var mealDtos = new List<MealDTO>();

            foreach (var meal in meals)
            {
                var ingredients = new List<CommonNameDTO>();
                foreach (var mealIngredient in meal.MealIngredients)
                {
                    ingredients.Add(new CommonNameDTO
                    {
                        Id = mealIngredient.Ingredient.Id,
                        Name = mealIngredient.Ingredient.Name,
                        NameForeign = mealIngredient.Ingredient.NameForeign,
                    });
                }

                var allergens = new List<CommonNameDTO>();
                foreach (var mealAllergen in meal.MealAllergens)
                {
                    allergens.Add(new CommonNameDTO
                    {
                        Id = mealAllergen.Allergen.Id,
                        Name = mealAllergen.Allergen.Name,
                        NameForeign = mealAllergen.Allergen.NameForeign,
                    });
                }

                mealDtos.Add(new MealDTO
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    NameForeign = meal.NameForeign,
                    ImageBase64 = meal.ImageBase64,
                    Ingredients = ingredients,
                    Allergens = allergens
                });
            }

            return mealDtos;
        }
    }
}
