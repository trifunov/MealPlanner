using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
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
    public class AccountController : ControllerBase
    {
        private IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                _accountManager.Register(registerDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var result = _accountManager.Login(loginDto);
                if (Convert.ToBoolean(result.GetValue("success")) == true)
                {
                    return Ok(result);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
