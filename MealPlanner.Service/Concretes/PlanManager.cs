using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Helpers;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class PlanManager : IPlanManager
    {
        private readonly IPlanRepository _planRepository;

        public PlanManager(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public void Add(PlanDTO planDto)
        {
            var plans = new List<Plan>();
            for(var i = 0; i<planDto.MealIds.Count; i++)
            {
                plans.Add(new Plan
                {
                    ActiveFrom = planDto.ActiveFrom,
                    ActiveTo = planDto.ActiveTo,
                    EditableFrom = planDto.EditableFrom,
                    EditableTo = planDto.EditableTo,
                    Shifts = string.Join(",", planDto.Shifts),
                    CompanyId = planDto.CompanyId,
                    MealId = planDto.MealIds[i]
                });
            }
            _planRepository.Add(plans);
        }

        public void Delete(List<int> ids)
        {
            _planRepository.Delete(ids);
        }

        public List<PlanDTO> GetActivePlans()
        {
            throw new NotImplementedException();
        }

        public List<PlanDTO> GetByCompanyId(int companyId)
        {
            var plans = _planRepository.GetByCompanyIdGrouped(companyId);
            var planDtos = new List<PlanDTO>();

            foreach(var item in plans)
            {
                var shifts = PlanHelper.ShiftsToList(item.Shifts);
                var shiftNames = PlanHelper.GetShiftNames(shifts);

                planDtos.Add(new PlanDTO
                {
                    Ids = item.Ids,
                    Shifts = shifts,
                    ShiftNames = shiftNames,
                    ActiveFrom = item.ActiveFrom,
                    ActiveTo = item.ActiveTo,
                    EditableFrom = item.EditableFrom,
                    EditableTo = item.EditableTo,
                    CompanyId = item.CompanyId,
                    MealIds = item.MealIds,
                    TotalMeals = item.TotalMeals
                });
            }

            return planDtos;
        }

        public PlanDTO GetByIds(List<int> ids)
        {
            var plan = _planRepository.GetByIds(ids);

            var shifts = PlanHelper.ShiftsToList(plan.Shifts);
            var shiftNames = PlanHelper.GetShiftNames(shifts);
            var planDto = new PlanDTO
            {
                Ids = plan.Ids,
                Shifts = shifts,
                ShiftNames = shiftNames,
                ActiveFrom = plan.ActiveFrom,
                ActiveTo = plan.ActiveTo,
                EditableFrom = plan.EditableFrom,
                EditableTo = plan.EditableTo,
                CompanyId = plan.CompanyId,
                MealIds = plan.MealIds
            };

            return planDto;
        }
    }
}
