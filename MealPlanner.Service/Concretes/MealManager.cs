using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Helpers;
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
                NameForeign = mealDto.NameForeign
            };
            _mealRepository.Add(meal, mealDto.ImageBase64);

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

        public MealPaginationDTO GetAll(string mealName, int page, int itemsPerPage, bool paged)
        {
            var mealDtos = new List<MealDTO>();

            mealName = (mealName == null) ? "" : mealName;
            var meals = _mealRepository.GetAll(mealName, page, itemsPerPage, paged);

            foreach (var meal in meals.Meals)
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
                    ImageBase64 = "",
                    Ingredients = ingredients,
                    Allergens = allergens
                });
            }

            return new MealPaginationDTO
            { 
                Meals = mealDtos,
                TotalRows = meals.TotalRows
            };
        }

        public List<MealDTO> GetValid(int companyId, int shift, DateTime date)
        {
            var meals = _mealRepository.GetValid(companyId, shift, date);
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
                    Allergens = allergens,
                    PlanId = meal.PlanId
                });
            }

            return mealDtos;
        }

        public MealDTO GetById(int id)
        {
            var meal = _mealRepository.GetById(id);

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

            return new MealDTO
            {
                Id = meal.Id,
                Name = meal.Name,
                NameForeign = meal.NameForeign,
                ImageBase64 = meal.MealImage.ImageBase64,
                Ingredients = ingredients,
                Allergens = allergens
            };
        }

        public void Update(MealDTO mealDto)
        {
            var meal = new Meal
            {
                Id = mealDto.Id,
                Name = mealDto.Name,
                NameForeign = mealDto.NameForeign
            };
            _mealRepository.Update(meal, mealDto.ImageBase64);

            var ingredients = new List<Ingredient>();
            foreach (var item in mealDto.Ingredients)
            {
                ingredients.Add(new Ingredient
                {
                    Id = item.Id,
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
                    Id = item.Id,
                    Name = item.Name,
                    NameForeign = item.NameForeign
                });
            }
            _allergenRepository.Add(allergens);

            _ingredientRepository.DeleteMealIngredients(meal.Id);
            _allergenRepository.DeleteMealAllergens(meal.Id);

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
    }
}
