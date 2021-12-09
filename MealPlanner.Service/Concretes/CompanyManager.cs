using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class CompanyManager : ICompanyManager
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManager(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void Add(CompanyDTO companyDto)
        {
            var company = new Company();
            company.Name = companyDto.Name;
            company.ImageBase64 = companyDto.ImageBase64;
            _companyRepository.Add(company);
        }

        public void Delete(int id)
        {
            _companyRepository.Delete(id);
        }

        public void Update(CompanyDTO companyDto)
        {
            var company = new Company();
            company.Id = companyDto.Id;
            company.Name = companyDto.Name;
            company.ImageBase64 = companyDto.ImageBase64;
            _companyRepository.Update(company);
        }

        public List<CompanyDTO> GetAll(int companyId)
        {
            var companyDTOs = new List<CompanyDTO>();
            var companies = _companyRepository.GetAll(companyId);

            foreach (var company in companies)
            {
                var companyDto = new CompanyDTO();
                companyDto.Id = company.Id;
                companyDto.Name = company.Name;
                companyDto.ImageBase64 = "";
                companyDto.TotalEmployees = company.Employees.Count;
                companyDTOs.Add(companyDto);
            }

            return companyDTOs;
        }

        public CompanyDTO GetById(int id)
        {
            var company = _companyRepository.GetById(id);
            var companyDto = new CompanyDTO();
            companyDto.Id = company.Id;
            companyDto.Name = company.Name;
            companyDto.ImageBase64 = company.ImageBase64;

            return companyDto;
        }

        public CompanyNameDTO GetName(int id)
        {
            var company = _companyRepository.GetById(id);
            return new CompanyNameDTO 
            { 
                Name = company.Name,
                ImageBase64 = company.ImageBase64
            };
        }
    }
}
