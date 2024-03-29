﻿using MealPlanner.Service.DTOs;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,HR")]
    public class EmployeeController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeManager _employeeManager;
        private readonly IAccountManager _accountManager;

        public EmployeeController(IHttpContextAccessor httpContextAccessor, IEmployeeManager employeeManager, IAccountManager accountManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _employeeManager = employeeManager;
            _accountManager = accountManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_employeeManager.GetAll());
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
                return Ok(_employeeManager.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserEmployeeDTO employeeDTO)
        {
            try
            {
                employeeDTO.UserId = _accountManager.Register(new RegisterDTO
                {
                    Email = employeeDTO.Email,
                    Username = employeeDTO.Username,
                    Password = employeeDTO.Password,
                    Role = employeeDTO.Role
                });
                _employeeManager.Add(employeeDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] UserEmployeeDTO employeeDTO)
        {
            try
            {
                _accountManager.Update(employeeDTO);
                _employeeManager.Update(employeeDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id, string userId)
        {
            try
            {
                _accountManager.Delete(userId);
                _employeeManager.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetByCompanyId(int companyId)
        {
            try
            {
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claimRole != null && claimRole.Value == "HR")
                {
                    var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                    if (claimCompanyId != null)
                    {
                        companyId = Int32.Parse(claimCompanyId.Value);
                    }
                }

                return Ok(_employeeManager.GetByCompanyId(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetByCompanyIdFromToken()
        {
            try
            {
                var companyId = 0;
                var claimCompanyId = _httpContextAccessor.HttpContext.User.FindFirst("CompanyId");
                if (claimCompanyId != null)
                {
                    companyId = Int32.Parse(claimCompanyId.Value);
                }

                return Ok(_employeeManager.GetByCompanyId(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUsersWithoutEmployee()
        {
            try
            {
                return Ok(_employeeManager.GetUsersWithoutEmployee());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
