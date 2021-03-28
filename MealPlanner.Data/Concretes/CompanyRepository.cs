using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data.Concretes
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly MealPlannerContext _context;

        public CompanyRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public void Add(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var company = _context.Companies.Find(id);

            if (company != null)
            {
                _context.Companies.Remove(company);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Company not found");
            }
        }

        public void Update(Company companyInput)
        {
            var company = _context.Companies.Find(companyInput.Id);

            if (company != null)
            {
                company.Name = companyInput.Name;
                company.ImageBase64 = companyInput.ImageBase64;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Company not found");
            }
        }

        public Company GetById(int id)
        {
            var company = _context.Companies.Find(id);

            if (company != null)
            {
                return company;
            }
            else
            {
                throw new Exception("Company not found");
            }
        }

        public List<Company> GetAll(int companyId)
        {
            if(companyId == 0)
            {
                return _context.Companies.Include(x => x.Employees).ToList();
            }
            else
            {
                return _context.Companies.Where(x => x.Id == companyId).Include(x => x.Employees).ToList();
            }
        }
    }
}
