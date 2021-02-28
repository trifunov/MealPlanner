using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data.Concretes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MealPlannerContext _context;

        public EmployeeRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Employee not found");
            }
        }

        public void Update(Employee employeeInput)
        {
            var employee = _context.Employees.Find(employeeInput.Id);

            if (employee != null)
            {
                employee.Rfid = employeeInput.Rfid;
                employee.Company = employeeInput.Company;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Employee not found");
            }
        }

        public Employee GetById(int id)
        {
            var employee = _context.Employees.Include(x => x.Company).FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                return employee;
            }
            else
            {
                throw new Exception("Employee not found");
            }
        }

        public Employee GetByUserId(string userId)
        {
            var employee = _context.Employees.Include(x => x.Company).FirstOrDefault(x => x.UserId == userId);

            if (employee != null)
            {
                return employee;
            }
            else
            {
                throw new Exception("Employee not found");
            }
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public List<ApplicationUser> GetUsersWithoutEmployee()
        {
            return _context.Users.Include(x => x.Employee).Where(x => x.Employee == null).ToList();
        }

        public List<Employee> GetByCompanyId(int companyId)
        {
            return _context.Employees.Include(x => x.Company).Include(x => x.User).Where(x => x.Company.Id == companyId).ToList();
        }
    }
}
