using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
        }

        public void Add(EmployeeDTO employeeDto)
        {
            var employee = new Employee();
            employee.Rfid = employeeDto.Rfid;
            employee.UserId = employeeDto.UserId;
            employee.Company = _companyRepository.GetById(employeeDto.CompanyId);
            _employeeRepository.Add(employee);
        }

        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }

        public void Update(EmployeeDTO employeeDto)
        {
            var employee = new Employee();
            employee.Id = employeeDto.Id;
            employee.Rfid = employeeDto.Rfid;
            _employeeRepository.Update(employee);
        }

        public List<EmployeeDTO> GetAll()
        {
            var employeeDTOs = new List<EmployeeDTO>();
            var employees = _employeeRepository.GetAll();

            foreach (var employee in employees)
            {
                var employeeDTO = new EmployeeDTO();
                employeeDTO.Id = employee.Id;
                employeeDTO.Rfid = employee.Rfid;
                employeeDTO.UserId = employee.UserId;
                employeeDTO.CompanyId = employee.Company.Id;
                employeeDTOs.Add(employeeDTO);
            }

            return employeeDTOs;
        }

        public UserEmployeeDTO GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            var employeeDTO = new UserEmployeeDTO();
            employeeDTO.Id = employee.Id;
            employeeDTO.Rfid = employee.Rfid;
            employeeDTO.UserId = employee.UserId;
            employeeDTO.CompanyId = employee.Company.Id;
            employeeDTO.Email = employee.User.Email;
            employeeDTO.Username = employee.User.UserName;
            employeeDTO.Role = employee.Role;
            employeeDTO.Password = "";

            return employeeDTO;
        }

        public EmployeeDTO GetByUserId(string userId)
        {
            var employee = _employeeRepository.GetByUserId(userId);
            var employeeDTO = new EmployeeDTO();
            employeeDTO.Id = employee.Id;
            employeeDTO.Rfid = employee.Rfid;
            employeeDTO.UserId = employee.UserId;
            employeeDTO.CompanyId = employee.Company.Id;

            return employeeDTO;
        }

        public EmployeeDTO GetByRfid(string rfid)
        {
            var employee = _employeeRepository.GetByRfid(rfid);
            var employeeDTO = new EmployeeDTO();
            employeeDTO.Id = employee.Id;
            employeeDTO.Rfid = employee.Rfid;
            employeeDTO.UserId = employee.UserId;
            employeeDTO.CompanyId = employee.Company.Id;

            return employeeDTO;
        }

        public List<UserEmployeeDTO> GetByCompanyId(int companyId)
        {
            var employeeDTOs = new List<UserEmployeeDTO>();
            var employees = _employeeRepository.GetByCompanyId(companyId);

            foreach (var employee in employees)
            {
                var employeeDTO = new UserEmployeeDTO();
                employeeDTO.Id = employee.Id;
                employeeDTO.Rfid = employee.Rfid;
                employeeDTO.UserId = employee.UserId;
                employeeDTO.CompanyId = employee.Company.Id;
                employeeDTO.CompanyName = employee.Company.Name;
                employeeDTO.Username = employee.User.UserName;
                employeeDTO.Role = employee.Role;
                employeeDTOs.Add(employeeDTO);
            }

            return employeeDTOs;
        }

        public List<UserEmployeeDTO> GetUsersWithoutEmployee()
        {
            var employeeDTOs = new List<UserEmployeeDTO>();
            var users = _employeeRepository.GetUsersWithoutEmployee();

            foreach (var user in users)
            {
                var employeeDTO = new UserEmployeeDTO();
                employeeDTO.Id = 0;
                employeeDTO.UserId = user.Id;
                employeeDTO.Username = user.UserName;
                employeeDTO.Rfid = "";
                employeeDTO.CompanyId = 0;
                employeeDTO.CompanyName = "";
                employeeDTOs.Add(employeeDTO);
            }

            return employeeDTOs;
        }
    }
}
