using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IEmployeeManager
    {
        void Add(EmployeeDTO employeeDto);
        void Delete(int id);
        void Update(EmployeeDTO employeeDto);
        UserEmployeeDTO GetById(int id);
        EmployeeDTO GetByUserId(string userId);
        EmployeeDTO GetByRfid(string rfid);
        List<EmployeeDTO> GetAll();
        List<UserEmployeeDTO> GetByCompanyId(int companyId);
        List<UserEmployeeDTO> GetUsersWithoutEmployee();
    }
}
