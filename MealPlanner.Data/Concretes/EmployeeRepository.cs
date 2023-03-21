using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Data.ModelsPagination;
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
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Employee not found");
            }
        }

        public EmployeeJoined GetById(int id)
        {
            var employee = _context.Employees.Include(x => x.Company).Include(x => x.User).FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                var resultRole = (from userRoles in _context.UserRoles
                                  join role in _context.Roles on userRoles.RoleId equals role.Id
                                  where userRoles.UserId == employee.UserId
                                  select role.Name).FirstOrDefault();

                return new EmployeeJoined
                {
                    Id = employee.Id,
                    Rfid = employee.Rfid,
                    User = employee.User,
                    Company = employee.Company,
                    UserId = employee.UserId,
                    Role = (resultRole == null) ? "" : resultRole
                };
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

        public Employee GetByRfid(string rfid)
        {
            var employee = _context.Employees.Include(x => x.Company).FirstOrDefault(x => x.Rfid == rfid);

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

        public EmployeePagination GetByCompanyId(int companyId, string employeeName, int page, int itemsPerPage, bool paged)
        {
            var employees = _context.Employees.Include(x => x.Company).Include(x => x.User).Where(x => x.Company.Id == companyId && x.User.UserName.Contains(employeeName));

            var query = from employee in employees
                        join userRole in _context.UserRoles on employee.UserId equals userRole.UserId 
                        into groupedUserRoles
                        from gur in groupedUserRoles.DefaultIfEmpty()
                        join role in _context.Roles on gur.RoleId equals role.Id
                        into groupedRoles
                        from gr in groupedRoles.DefaultIfEmpty()
                        select new EmployeeJoined 
                        { 
                            Id = employee.Id,
                            Rfid = employee.Rfid,
                            User = employee.User,
                            Company = employee.Company,
                            UserId = employee.UserId,
                            Role = (gr == null) ? "" : gr.Name
                        };

            if (paged)
            {
                return new EmployeePagination
                {
                    TotalRows = query.Count(),
                    Employees = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList()
                };
            }
            else
            {
                return new EmployeePagination
                {
                    TotalRows = query.Count(),
                    Employees = query.ToList()
                };
            }
        }
    }
}
