﻿using MealPlanner.Data.Models;
using MealPlanner.Data.ModelsPagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IPlanRepository
    {
        void Add(List<Plan> plans);
        void Delete(List<int> ids);
        void Update(List<int> ids, Plan planToUpdate);
        List<Plan> GetActivePlans(int companyId);
        PlanGroupedPagination GetByCompanyIdGrouped(int page, int itemsPerPage, int companyId, DateTime fromDate, DateTime toDate);
        PlanGrouped GetByIds(List<int> ids);
        Plan GetById(int id);
        Plan GetByOrderId(int orderId);
        Plan GetByMealId(int mealId, DateTime date, int companyId, DateTime editableFrom, DateTime editableTo, string shifts);
        List<PlanReport> GetReports(int companyId, DateTime fromDate, DateTime toDate);
        List<PlanReport> GetDetailedReports(int companyId, DateTime fromDate, DateTime toDate, int shift, int delivered);
    }
}
