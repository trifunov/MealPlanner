using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMealManager _mealManager;

        public MealController(IMealManager mealManager, IHttpContextAccessor httpContextAccessor)
        {
            _mealManager = mealManager;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<controller>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult GetAll(int page, int itemsPerPage, bool paged)
        {
            try
            {
                return Ok(_mealManager.GetAll(page, itemsPerPage, paged));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetValid(DateTime date, int shift)
        {
            try
            {
                var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                var companyId = (claimCompanyId == null) ? 0 : Int32.Parse(claimCompanyId.Value);
                return Ok(_mealManager.GetValid(companyId,shift,date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_mealManager.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult Add([FromBody] MealDTO mealDto)
        {
            try
            {
                _mealManager.Add(mealDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult Delete(int id)
        {
            try
            {
                _mealManager.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager")]
        public IActionResult Update([FromBody] MealDTO mealDto)
        {
            try
            {
                _mealManager.Update(mealDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
