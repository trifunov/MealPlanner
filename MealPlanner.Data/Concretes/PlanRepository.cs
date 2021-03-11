using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data.Concretes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly MealPlannerContext _context;

        public PlanRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public void Add(List<Plan> plans)
        {
            _context.Plans.AddRange(plans);
            _context.SaveChanges();
        }

        public void Delete(List<int> ids)
        {
            var plans = _context.Plans.Where(x => ids.Contains(x.Id));

            if (plans.Count() > 0)
            {
                _context.Plans.RemoveRange(plans);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Plans not found");
            } 
        }

        public List<Plan> GetActivePlans(int companyId)
        {
            return _context.Plans.Where(x => x.CompanyId == companyId && DateTime.Now > x.Date).ToList();
        }

        public List<GroupedPlan> GetByCompanyIdGrouped(int companyId)
        {
            return _context.Plans.Where(x => x.CompanyId == companyId).GroupBy(p => new 
            { 
                p.CompanyId, 
                p.Shifts, 
                p.Date,
                p.EditableFrom,
                p.EditableTo
            }).Select(g => new GroupedPlan
            { 
                Ids = g.Select(x => x.Id).ToList(),
                CompanyId = g.Key.CompanyId,
                Shifts = g.Key.Shifts,
                Date = g.Key.Date,
                EditableFrom = g.Key.EditableFrom,
                EditableTo = g.Key.EditableTo,
                MealIds = g.Select(x => x.MealId).ToList(),
                TotalMeals = g.Count() 
            }).ToList();
        }

        public Plan GetById(int id)
        {
            return _context.Plans.Find(id);
        }

        public GroupedPlan GetByIds(List<int> ids)
        {
            return _context.Plans.Where(x => ids.Contains(x.Id)).GroupBy(p => new
            {
                p.CompanyId,
                p.Shifts,
                p.Date,
                p.EditableFrom,
                p.EditableTo
            }).Select(g => new GroupedPlan
            {
                Ids = g.Select(x => x.Id).ToList(),
                CompanyId = g.Key.CompanyId,
                Shifts = g.Key.Shifts,
                Date = g.Key.Date,
                EditableFrom = g.Key.EditableFrom,
                EditableTo = g.Key.EditableTo,
                MealIds = g.Select(x => x.MealId).ToList()
            }).FirstOrDefault();
        }
    }
}
