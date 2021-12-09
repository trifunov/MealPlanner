using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface ICompanyRepository
    {
        void Add(Company company);
        void Delete(int id);
        void Update(Company company);
        Company GetById(int id);
        List<CompanyNoImage> GetAll(int companyId);
    }
}
