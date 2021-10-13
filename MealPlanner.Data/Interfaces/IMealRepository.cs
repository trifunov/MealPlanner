using MealPlanner.Data.Models;
using MealPlanner.Data.ModelsPagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IMealRepository
    {
        void Add(Meal meal, string image);
        void Delete(int id);
        void Update(Meal meal, string image);
        Meal GetById(int id);
        MealPagination GetAll(int page, int itemsPerPage, bool paged);
        List<MealJoined> GetValid(int companyId, int shift, DateTime date);
    }
}
