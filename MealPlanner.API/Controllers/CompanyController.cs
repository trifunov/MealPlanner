using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MealPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanyManager _companyManager;

        public CompanyController(ICompanyManager companyManager, IHttpContextAccessor httpContextAccessor)
        {
            _companyManager = companyManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager,HR")]
        public IActionResult GetAll()
        {
            try
            {
                var companyId = 0;
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claimRole != null && claimRole.Value == "HR")
                {
                    var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                    if (claimCompanyId != null)
                    {
                        companyId = Int32.Parse(claimCompanyId.Value);
                    }
                }

                return Ok(_companyManager.GetAll(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Manager,HR")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_companyManager.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,HR")]
        public IActionResult GetName(int id)
        {
            try
            {
                return Ok(_companyManager.GetName(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult Add([FromBody] CompanyDTO companyDto)
        {
            try
            {
                _companyManager.Add(companyDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult Update([FromBody] CompanyDTO companyDto)
        {
            try
            {
                _companyManager.Update(companyDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            try
            {
                _companyManager.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetLogo()
        {
            try
            {
                var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");

                if(claimCompanyId == null)
                {
                    return Ok(new CompanyNameDTO { Name = "", ImageBase64 = "" });
                }
                else
                {
                    return Ok(_companyManager.GetName(Int32.Parse(claimCompanyId.Value)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
