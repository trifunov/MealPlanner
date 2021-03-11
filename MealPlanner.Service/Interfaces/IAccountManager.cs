using MealPlanner.Service.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IAccountManager
    {
        string Register(RegisterDTO registerDto);
        void Update(UserEmployeeDTO employeeDto);
        void Delete(string userId);
        JObject Login(LoginDTO loginDto);
        JObject LoginRfid(string rfid);
    }
}
