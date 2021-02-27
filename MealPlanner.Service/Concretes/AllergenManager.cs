using MealPlanner.Data.Interfaces;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class AllergenManager : IAllergenManager
    {
        private readonly IAllergenRepository _allergenRepository;

        public AllergenManager(IAllergenRepository allergenRepository)
        {
            _allergenRepository = allergenRepository;
        }

        public List<CommonNameDTO> GetAll()
        {
            var allergens = _allergenRepository.GetAll();
            var allergenDtos = new List<CommonNameDTO>();
            foreach(var item in allergens)
            {
                allergenDtos.Add(new CommonNameDTO
                {
                    Name = item.Name,
                    NameForeign = item.NameForeign
                });
            }

            return allergenDtos;
        }
    }
}
