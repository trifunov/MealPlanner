using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MealPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPlanManager _planManager;

        public PlanController(IPlanManager planManager, IHttpContextAccessor httpContextAccessor)
        {
            _planManager = planManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult Add([FromBody] PlanDTO planDto)
        {
            try
            {
                //var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                //planDto.CompanyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                _planManager.Add(planDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult Update([FromBody] PlanDTO planDto)
        {
            try
            {
                //var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                //planDto.CompanyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                _planManager.Update(planDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult GetByCompanyId([FromBody] PlanGetByCompanyIdRequestDTO requestDto)
        {
            try
            {
                //var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                //var companyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                return Ok(_planManager.GetByCompanyId(requestDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult Delete([FromBody] List<int> ids)
        {
            try
            {
                _planManager.Delete(ids);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult GetByIds([FromBody] List<int> ids)
        {
            try
            {
                return Ok(_planManager.GetByIds(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager,HR")]
        public IActionResult GetReports([FromBody] ReportRequestDTO planDto)
        {
            try
            {
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claimRole == null || claimRole.Value != "Administrator")
                {
                    var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                    var companyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                    planDto.CompanyId = companyId;
                }

                return Ok(_planManager.GetReports(planDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager,HR")]
        public IActionResult GetDetailedReports([FromBody] ReportDetailedRequestDTO planDto)
        {
            try
            {
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claimRole == null || claimRole.Value != "Administrator")
                {
                    var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                    var companyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                    planDto.CompanyId = companyId;
                }

                return Ok(_planManager.GetDetailedReports(planDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
