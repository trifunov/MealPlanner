using MealPlanner.Data.Interfaces;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class IngredientManager : IIngredientManager
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientManager(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public List<CommonNameDTO> GetAll()
        {
            var ingredients = _ingredientRepository.GetAll();
            var ingredientDtos = new List<CommonNameDTO>();
            foreach (var item in ingredients)
            {
                ingredientDtos.Add(new CommonNameDTO
                {
                    Name = item.Name,
                    NameForeign = item.NameForeign
                });
            }

            return ingredientDtos;
        }
    }
}
