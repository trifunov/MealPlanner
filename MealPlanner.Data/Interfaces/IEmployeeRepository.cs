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
        EmployeeJoined GetById(int id);
        Employee GetByUserId(string userId);
        Employee GetByRfid(string rfid);
        List<Employee> GetAll();
        List<EmployeeJoined> GetByCompanyId(int companyId);
        List<ApplicationUser> GetUsersWithoutEmployee();
    }
}
