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

        public void Update(List<int> ids, Plan planToUpdate)
        {
            var plans = _context.Plans.Where(x => ids.Contains(x.Id));

            foreach (var plan in plans)
            {
                plan.Date = planToUpdate.Date;
                plan.Shifts = planToUpdate.Shifts;
                plan.EditableFrom = planToUpdate.EditableFrom;
                plan.EditableTo = planToUpdate.EditableTo;
            }

            _context.SaveChanges();
        }

        public List<Plan> GetActivePlans(int companyId)
        {
            return _context.Plans.Where(x => x.CompanyId == companyId && DateTime.Now > x.Date).ToList();
        }

        public PlanGroupedPagination GetByCompanyIdGrouped(int page, int itemsPerPage, int companyId)
        {
            var plans = _context.Plans.Include(x => x.Meal).Where(x => x.CompanyId == companyId);

            var query = (from plan in plans
                         group plan by new
                         {
                             plan.Date,
                             plan.CompanyId,
                             plan.Shifts,
                             plan.EditableFrom,
                             plan.EditableTo
                         } into g
                         select new PlanGrouped
                         {
                             Ids = g.Select(x => x.Id).ToList(),
                             CompanyId = g.Key.CompanyId,
                             Shifts = g.Key.Shifts,
                             Date = g.Key.Date,
                             EditableFrom = g.Key.EditableFrom,
                             EditableTo = g.Key.EditableTo,
                             Meals = g.Select(x => x.Meal).ToList(),
                         }).OrderByDescending(x => x.Date);

            return new PlanGroupedPagination
            {
                TotalRows = query.Count(),
                Plans = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList()
            };
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

        public Plan GetByMealId(int mealId, DateTime date, int companyId, DateTime editableFrom, DateTime editableTo, string shifts)
        {
            return _context.Plans.Include(x => x.Meal).Include(x => x.Company).FirstOrDefault(x => x.Meal.Id == mealId && x.Date == date && x.Company.Id == companyId && x.EditableFrom == editableFrom && x.EditableTo == editableTo && x.Shifts == shifts);
        }

        public PlanGrouped GetByIds(List<int> ids)
        {
            return _context.Plans.Include(x => x.Meal).Where(x => ids.Contains(x.Id)).GroupBy(p => new
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
                Meals = g.Select(x => x.Meal).ToList()
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

        public List<PlanReport> GetDetailedReports(int companyId, DateTime fromDate, DateTime toDate, int shift, int delivered)
        {
            var plans = _context.Plans.Include(x => x.Orders).Include(x => x.Meal).Where(x => x.CompanyId == companyId && x.Date >= fromDate && x.Date <= toDate);
            var orders = _context.Orders.AsQueryable();

            if (shift > -1)
            {
                orders = orders.Where(x => x.Shift == shift);
            }

            if(delivered == 0)
            {
                orders = orders.Where(x => x.IsDelivered == false);
            }
            else if(delivered == 1)
            {
                orders = orders.Where(x => x.IsDelivered == true);
            }

            return (from plan in plans
                    join order in orders on plan.Id equals order.PlanId
                    select new PlanReport
                    {
                        Date = plan.Date,
                        Shift = order.Shift,
                        MealName = plan.Meal.Name,
                        IsDelivered = order.IsDelivered
                    }).ToList();
        }
    }
}
