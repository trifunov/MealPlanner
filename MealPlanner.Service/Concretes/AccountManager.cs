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

        public string Register(RegisterDTO registerDto)
        {
            var user = new ApplicationUser { Email = registerDto.Email, UserName = registerDto.Username };
            var result = _userManager.CreateAsync(user, registerDto.Password).Result;

            var roleResult = false;
            if (!string.IsNullOrEmpty(registerDto.Role))
            {
                roleResult = _userManager.AddToRoleAsync(user, registerDto.Role).Result.Succeeded;
            }
            
            if (!result.Succeeded)
            {
                var errorMessage = "";
                foreach (var error in result.Errors)
                {
                    errorMessage += error.Description + "\r\n";
                }
                throw new Exception(errorMessage);
            }

            return user.Id;
        }

        public void Update(UserEmployeeDTO employeeDTO)
        {
            var user = _userManager.FindByIdAsync(employeeDTO.UserId).Result;
            if (user != null)
            {
                user.Email = employeeDTO.Email;
                user.UserName = employeeDTO.Username;
                var result = _userManager.UpdateAsync(user).Result;

                var roleResult = false;
                var roles = _userManager.GetRolesAsync(user).Result;
                if (roles.Count > 0 && employeeDTO.Role != roles[0])
                {
                    roleResult = _userManager.RemoveFromRolesAsync(user, roles).Result.Succeeded;
                }

                if (!string.IsNullOrEmpty(employeeDTO.Role) && (roles.Count == 0 || roleResult == true))
                {
                    roleResult = _userManager.AddToRoleAsync(user, employeeDTO.Role).Result.Succeeded;
                }

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
        }

        public void Delete(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                var result = _userManager.DeleteAsync(user).Result;

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
        }

        public void ResetPassword(string userId, string password)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                var tempToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                var result = _userManager.ResetPasswordAsync(user, tempToken, password).Result;

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
        }

        public JObject Login(LoginDTO loginDto)
        {
            var user = _userManager.FindByNameAsync(loginDto.Username).Result;
            if (user != null && _userManager.CheckPasswordAsync(user, loginDto.Password).Result)
            {
                var employee = _employeeManager.GetByUserId(user.Id);

                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("CompanyId", employee.CompanyId.ToString()),
                    new Claim("EmployeeId", employee.Id.ToString())
                };

                var tokenRole = "";
                var roles = _userManager.GetRolesAsync(user);
                foreach (var role in roles.Result)
                {
                    tokenRole = role;
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

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
                    role = tokenRole
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
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("CompanyId", employee.CompanyId.ToString()),
                    new Claim("EmployeeId", employee.Id.ToString())
                };

                var tokenRole = "";
                var roles = _userManager.GetRolesAsync(user);
                foreach (var role in roles.Result)
                {
                    tokenRole = role;
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

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
                    role = tokenRole
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
