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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public IActionResult Add([FromBody] PlanDTO planDto)
        {
            try
            {
                var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                planDto.CompanyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                _planManager.Add(planDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetByCompanyId()
        {
            try
            {
                var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                var companyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                return Ok(_planManager.GetByCompanyId(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
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
    }
}
