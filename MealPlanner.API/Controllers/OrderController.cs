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
    public class OrderController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager, IHttpContextAccessor httpContextAccessor)
        {
            _orderManager = orderManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult Add([FromBody] OrderDTO orderDto)
        {
            try
            {
                var claimEmployeeId = _httpContextAccessor.HttpContext.User.FindFirst("EmployeeId");
                orderDto.EmployeeId = (claimEmployeeId == null) ? 0 : Int32.Parse(claimEmployeeId.Value);
                _orderManager.Add(orderDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Update([FromBody] OrderDTO orderDto)
        {
            try
            {
                _orderManager.Update(orderDto);
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
                _orderManager.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
