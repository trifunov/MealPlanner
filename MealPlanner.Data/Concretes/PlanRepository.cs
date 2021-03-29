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

        public List<PlanGrouped> GetByCompanyIdGrouped(int companyId)
        {
            return _context.Plans.Where(x => x.CompanyId == companyId).GroupBy(p => new 
            { 
                p.CompanyId, 
                p.Shifts, 
                p.Date,
                p.EditableFrom,
                p.EditableTo
            }).Select(g => new PlanGrouped
            { 
                Ids = g.Select(x => x.Id).ToList(),
                CompanyId = g.Key.CompanyId,
                Shifts = g.Key.Shifts,
                Date = g.Key.Date,
                EditableFrom = g.Key.EditableFrom,
                EditableTo = g.Key.EditableTo,
                MealIds = g.Select(x => x.MealId).ToList(),
                TotalMeals = g.Count() 
            }).OrderByDescending(x => x.Date).ToList();
        }

        public Plan GetById(int id)
        {
            return _context.Plans.Include(x => x.Meal).FirstOrDefault(x => x.Id == id);
        }

        public Plan GetByOrderId(int orderId)
        {
            return (from plan in _context.Plans
                    join order in _context.Orders on plan.Id equals order.PlanId
                    where order.Id == orderId
                    select plan).FirstOrDefault();
        }

        public PlanGrouped GetByIds(List<int> ids)
        {
            return _context.Plans.Where(x => ids.Contains(x.Id)).GroupBy(p => new
            {
                p.CompanyId,
                p.Shifts,
                p.Date,
                p.EditableFrom,
                p.EditableTo
            }).Select(g => new PlanGrouped
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

        public List<PlanReport> GetReports(int companyId, DateTime fromDate, DateTime toDate)
        {
            var plans = _context.Plans.Include(x => x.Orders).Include(x => x.Meal).Where(x => x.CompanyId == companyId && x.Date >= fromDate && x.Date <= toDate);
            var orders = _context.Orders;

            return (from plan in plans
                        join order in orders on plan.Id equals order.PlanId
                        group plan by new 
                        { 
                            plan.Date, 
                            plan.CompanyId,
                            order.Shift,
                            plan.MealId,
                            plan.Meal.Name
                        } into grouped
                        select new PlanReport
                        { 
                            Date = grouped.Key.Date, 
                            Shift = grouped.Key.Shift,
                            MealName = grouped.Key.Name,
                            TotalOrders = grouped.Count()
                        }).ToList();
        }
    }
}
