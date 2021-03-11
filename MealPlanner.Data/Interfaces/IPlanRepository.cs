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
        List<GroupedPlan> GetByCompanyIdGrouped(int companyId);
        GroupedPlan GetByIds(List<int> ids);
        Plan GetById(int id);
    }
}
