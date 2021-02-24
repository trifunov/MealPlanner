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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyManager _companyManager;

        public CompanyController(ICompanyManager companyManager)
        {
            _companyManager = companyManager;
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
    }
}
