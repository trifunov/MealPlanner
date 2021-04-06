using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IPlanRepository
    {
        void Add(List<Plan> plans);
        void Delete(List<int> ids);
        List<Plan> GetActivePlans(int companyId);
        List<PlanGrouped> GetByCompanyIdGrouped(int companyId);
        PlanGrouped GetByIds(List<int> ids);
        Plan GetById(int id);
        Plan GetByOrderId(int orderId);
        List<PlanReport> GetReports(int companyId, DateTime fromDate, DateTime toDate);
        List<PlanReport> GetDetailedReports(int companyId, DateTime fromDate, DateTime toDate, int shift, int delivered);
    }
}
