using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class AccountManager : IAccountManager
    {
        private UserManager<ApplicationUser> _userManager;
        private IEmployeeManager _employeeManager;

        public AccountManager(UserManager<ApplicationUser> userManager, IEmployeeManager employeeManager)
        {
            _userManager = userManager;
            _employeeManager = employeeManager;
        }

        public void Register(RegisterDTO registerDto)
        {
            var user = new ApplicationUser { Email = registerDto.Email, UserName = registerDto.Username };
            var result = _userManager.CreateAsync(user, registerDto.Password).Result;
            if (!result.Succeeded)
            {
                var errorMessage = "";
                foreach (var error in result.Errors)
                {
                    errorMessage += error.Description + "\r\n";
                }
                throw new Exception(errorMessage);
            }
        }

        public JObject Login(LoginDTO loginDto)
        {
            var user = _userManager.FindByNameAsync(loginDto.Username).Result;
            if (user != null && _userManager.CheckPasswordAsync(user, loginDto.Password).Result)
            {
                var employee = _employeeManager.GetByUserId(user.Id);
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("CompanyId", employee.CompanyId.ToString()),
                    new Claim("EmployeeId", employee.Id.ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BiggerSecureKeyBecauseOfSize"));

                var token = new JwtSecurityToken(
                    issuer: "trifunov",
                    audience: "trifunov",
                    expires: DateTime.Now.AddHours(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return JObject.FromObject(new
                {
                    success = true,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = user.UserName,
                    role = "Administrator"
                });
            }
            else
            {
                return JObject.FromObject(new
                {
                    success = false
                });
            }
        }

        public JObject LoginRfid(string rfid)
        {
            var employee = _employeeManager.GetByRfid(rfid);
            var user = _userManager.FindByIdAsync(employee.UserId).Result;
            if (user != null)
            {
                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("CompanyId", employee.CompanyId.ToString()),
                    new Claim("EmployeeId", employee.Id.ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BiggerSecureKeyBecauseOfSize"));

                var token = new JwtSecurityToken(
                    issuer: "trifunov",
                    audience: "trifunov",
                    expires: DateTime.Now.AddHours(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return JObject.FromObject(new
                {
                    success = true,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = user.UserName,
                    role = "Administrator"
                });
            }
            else
            {
                return JObject.FromObject(new
                {
                    success = false
                });
            }
        }
    }
}
