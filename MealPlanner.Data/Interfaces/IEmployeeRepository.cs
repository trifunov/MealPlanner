using MealPlanner.Data.Models;
using MealPlanner.Data.ModelsPagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        EmployeePagination GetByCompanyId(int companyId, string employeeName, int page, int itemsPerPage, bool paged);
        List<ApplicationUser> GetUsersWithoutEmployee();
    }
}
