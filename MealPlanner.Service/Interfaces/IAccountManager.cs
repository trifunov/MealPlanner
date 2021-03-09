using MealPlanner.Service.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IAccountManager
    {
        void Register(RegisterDTO registerDto);
        JObject Login(LoginDTO loginDto);
        JObject LoginRfid(string rfid);
    }
}
