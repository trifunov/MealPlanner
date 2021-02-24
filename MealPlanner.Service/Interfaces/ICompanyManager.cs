using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface ICompanyManager
    {
        void Add(CompanyDTO companyDto);
        void Delete(int id);
        void Update(CompanyDTO companyDto);
        CompanyDTO GetById(int id);
        List<CompanyDTO> GetAll();
    }
}
