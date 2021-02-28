using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        void Delete(int id);
        void Update(Employee employee);
        Employee GetById(int id);
        Employee GetByUserId(string userId);
        List<Employee> GetAll();
        List<Employee> GetByCompanyId(int companyId);
        List<ApplicationUser> GetUsersWithoutEmployee();
    }
}
