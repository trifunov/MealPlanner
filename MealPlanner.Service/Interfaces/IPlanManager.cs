﻿using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IPlanManager
    {
        void Add(PlanDTO planDto);
        List<PlanDTO> GetActivePlans();
        PlanPaginationDTO GetByCompanyId(PlanGetByCompanyIdRequestDTO requestDto);
        PlanDTO GetByIds(List<int> ids);
        void Delete(List<int> ids);
        List<ReportResponseDTO> GetReports(ReportRequestDTO requestDto);
        List<ReportDetailedResponseDTO> GetDetailedReports(ReportDetailedRequestDTO requestDto);
        void Update(PlanDTO planDto);
    }
}
