using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Helpers;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
                    Date = planDto.Date,
                    EditableFrom = planDto.EditableFrom,
                    EditableTo = planDto.EditableTo,
                    Shifts = string.Join(",", planDto.Shifts),
                    CompanyId = planDto.CompanyId,
                    MealId = planDto.MealIds[i]
                });
            }
            _planRepository.Add(plans);
        }

        public void Update(PlanDTO planDto)
        {
            var existingPlans = _planRepository.GetByIds(planDto.Ids);
            var plansToRemove = new List<int>();
            var plansToAdd = new List<Plan>();
            var plansToUpdate = new List<int>();
            var planToUpdateData = new Plan();

            foreach (var existingMeal in existingPlans.Meals)
            {
                if(!planDto.MealIds.Contains(existingMeal.Id))
                {
                    var planToRemove = _planRepository.GetByMealId(existingMeal.Id, existingPlans.Date, existingPlans.CompanyId, existingPlans.EditableFrom, existingPlans.EditableTo, existingPlans.Shifts);
                    plansToRemove.Add(planToRemove.Id);
                }
            }

            foreach (var mealId in planDto.MealIds)
            {
                if (!existingPlans.Meals.Exists(x => x.Id == mealId))
                {
                    plansToAdd.Add(new Plan
                    {
                        Date = planDto.Date,
                        EditableFrom = planDto.EditableFrom,
                        EditableTo = planDto.EditableTo,
                        Shifts = string.Join(",", planDto.Shifts),
                        CompanyId = planDto.CompanyId,
                        MealId = mealId
                    });
                }
                else
                {
                    var planToUpdate = _planRepository.GetByMealId(mealId, existingPlans.Date, existingPlans.CompanyId, existingPlans.EditableFrom, existingPlans.EditableTo, existingPlans.Shifts);
                    plansToUpdate.Add(planToUpdate.Id);

                    planToUpdateData = new Plan
                    {
                        Date = planDto.Date,
                        EditableFrom = planDto.EditableFrom,
                        EditableTo = planDto.EditableTo,
                        Shifts = string.Join(",", planDto.Shifts),
                        CompanyId = planDto.CompanyId,
                        MealId = mealId
                    };
                }
            }

            if (plansToRemove.Count > 0)
            {
                _planRepository.Delete(plansToRemove);
            }

            if (plansToAdd.Count > 0)
            {
                _planRepository.Add(plansToAdd);
            }

            if (plansToUpdate.Count > 0)
            {
                _planRepository.Update(plansToUpdate, planToUpdateData);
            }
        }

        public void Delete(List<int> ids)
        {
            _planRepository.Delete(ids);
        }

        public List<PlanDTO> GetActivePlans()
        {
            throw new NotImplementedException();
        }

        public PlanPaginationDTO GetByCompanyId(int page, int itemsPerPage, int companyId)
        {
            var plans = _planRepository.GetByCompanyIdGrouped(page, itemsPerPage, companyId);
            var planDtos = new List<PlanDTO>();

            foreach(var item in plans.Plans)
            {
                var shifts = PlanHelper.ShiftsToList(item.Shifts);
                var shiftNames = PlanHelper.GetShiftNames(shifts);

                var mealIds = new List<int>();
                var meals = new List<MealDTO>();
                foreach (var meal in item.Meals)
                {
                    mealIds.Add(meal.Id);
                    meals.Add(new MealDTO
                    {
                        Id = meal.Id,
                        Name = meal.Name,
                        NameForeign = meal.NameForeign,
                        ImageBase64 = "",
                        PlanId = 0,
                        Allergens = new List<CommonNameDTO>(),
                        Ingredients = new List<CommonNameDTO>()
                    });
                }

                planDtos.Add(new PlanDTO
                {
                    Ids = item.Ids,
                    Shifts = shifts,
                    ShiftNames = shiftNames,
                    Date = item.Date,
                    EditableFrom = item.EditableFrom,
                    EditableTo = item.EditableTo,
                    CompanyId = item.CompanyId,
                    MealIds = mealIds,
                    Meals = meals
                });
            }

            return new PlanPaginationDTO
            {
                Plans = planDtos,
                TotalRows = plans.TotalRows
            };
        }

        public PlanDTO GetByIds(List<int> ids)
        {
            var plan = _planRepository.GetByIds(ids);

            var shifts = PlanHelper.ShiftsToList(plan.Shifts);
            var shiftNames = PlanHelper.GetShiftNames(shifts);

            var mealIds = new List<int>();
            var meals = new List<MealDTO>();
            foreach (var meal in plan.Meals)
            {
                mealIds.Add(meal.Id);
                meals.Add(new MealDTO
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    NameForeign = meal.NameForeign,
                    ImageBase64 = "",
                    PlanId = 0,
                    Allergens = new List<CommonNameDTO>(),
                    Ingredients = new List<CommonNameDTO>()
                });
            }

            var planDto = new PlanDTO
            {
                Ids = plan.Ids,
                Shifts = shifts,
                ShiftNames = shiftNames,
                Date = plan.Date,
                EditableFrom = plan.EditableFrom,
                EditableTo = plan.EditableTo,
                CompanyId = plan.CompanyId,
                MealIds = mealIds,
                Meals = meals
            };

            return planDto;
        }

        public List<ReportResponseDTO> GetReports(ReportRequestDTO requestDto)
        {
            var reports = _planRepository.GetReports(requestDto.CompanyId, requestDto.FromDate, requestDto.ToDate);
            var response = new List<ReportResponseDTO>();

            foreach(var report in reports)
            {
                response.Add(new ReportResponseDTO
                {
                    Date = report.Date,
                    MealName = report.MealName,
                    Shift = PlanHelper.GetShiftName(report.Shift),
                    TotalOrders = report.TotalOrders
                });
            }

            return response;
        }

        public List<ReportDetailedResponseDTO> GetDetailedReports(ReportDetailedRequestDTO requestDto)
        {
            var reports = _planRepository.GetDetailedReports(requestDto.CompanyId, requestDto.FromDate, requestDto.ToDate, requestDto.Shift, requestDto.Delivered);
            var response = new List<ReportDetailedResponseDTO>();

            foreach (var report in reports)
            {
                response.Add(new ReportDetailedResponseDTO
                {
                    Date = report.Date,
                    MealName = report.MealName,
                    Shift = PlanHelper.GetShiftName(report.Shift),
                    IsDelivered = report.IsDelivered
                });
            }

            return response;
        }
    }
}
