using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class MealManager : IMealManager
    {
        private readonly IMealRepository _mealRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IAllergenRepository _allergenRepository;

        public MealManager(IMealRepository mealRepository, IIngredientRepository ingredientRepository, IAllergenRepository allergenRepository)
        {
            _mealRepository = mealRepository;
            _ingredientRepository = ingredientRepository;
            _allergenRepository = allergenRepository;
        }

        public void Add(MealDTO mealDto)
        {
            var meal = new Meal
            {
                Name = mealDto.Name,
                NameForeign = mealDto.NameForeign,
                ImageBase64 = mealDto.ImageBase64
            };
            _mealRepository.Add(meal);

            var ingredients = new List<Ingredient>();
            foreach(var item in mealDto.Ingredients)
            {
                ingredients.Add(new Ingredient
                {
                    Name = item.Name,
                    NameForeign = item.NameForeign
                });
            }
            _ingredientRepository.Add(ingredients);

            var allergens = new List<Allergen>();
            foreach (var item in mealDto.Allergens)
            {
                allergens.Add(new Allergen
                {
                    Name = item.Name,
                    NameForeign = item.NameForeign
                });
            }
            _allergenRepository.Add(allergens);

            var mealIngredients = new List<MealIngredient>();
            foreach (var item in ingredients)
            {
                mealIngredients.Add(new MealIngredient
                {
                    MealId = meal.Id,
                    IngredientId = item.Id
                });
            }
            _ingredientRepository.AddMealIngredients(mealIngredients);

            var mealAllergens = new List<MealAllergen>();
            foreach (var item in allergens)
            {
                mealAllergens.Add(new MealAllergen
                {
                    MealId = meal.Id,
                    AllergenId = item.Id
                });
            }
            _allergenRepository.AddMealAllergens(mealAllergens);
        }

        public void Delete(int id)
        {
            _mealRepository.Delete(id);
            _ingredientRepository.DeleteMealIngredients(id);
            _allergenRepository.DeleteMealAllergens(id);
        }

        public List<MealDTO> GetAll()
        {
            var meals = _mealRepository.GetAll();
            var mealDtos = new List<MealDTO>();

            foreach(var meal in meals)
            {
                var ingredients = new List<CommonNameDTO>();
                foreach(var mealIngredient in meal.MealIngredients)
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

        public MealDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(MealDTO mealDto)
        {
            throw new NotImplementedException();
        }
    }
}
