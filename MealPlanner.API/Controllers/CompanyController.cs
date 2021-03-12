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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    public class CompanyController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanyManager _companyManager;

        public CompanyController(ICompanyManager companyManager, IHttpContextAccessor httpContextAccessor)
        {
            _companyManager = companyManager;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_companyManager.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
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
