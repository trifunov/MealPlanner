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
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                var claimEmployeeId = _httpContextAccessor.HttpContext.User.FindFirst("EmployeeId");
                orderDto.EmployeeId = (claimEmployeeId == null) ? 0 : Int32.Parse(claimEmployeeId.Value);               
                return Ok(_orderManager.Add(orderDto, claimRole.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddFromList([FromBody] OrderDTO orderDto)
        {
            try
            {
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claimRole == null || claimRole.Value != "HR")
                {
                    var claimEmployeeId = _httpContextAccessor.HttpContext.User.FindFirst("EmployeeId");
                    var employeeId = (claimEmployeeId == null) ? 0 : Int32.Parse(claimEmployeeId.Value);
                    orderDto.EmployeeId = employeeId;
                }

                return Ok(_orderManager.Add(orderDto, claimRole.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult EditFromList([FromBody] OrderForEditDTO orderDto)
        {
            try
            {
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claimRole == null || claimRole.Value != "HR")
                {
                    var claimEmployeeId = _httpContextAccessor.HttpContext.User.FindFirst("EmployeeId");
                    var employeeId = (claimEmployeeId == null) ? 0 : Int32.Parse(claimEmployeeId.Value);
                    orderDto.EmployeeId =  employeeId;
                }

                _orderManager.EditFromList(orderDto, claimRole.Value);
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
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                _orderManager.Delete(id, claimRole.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Delivered(int id)
        {
            try
            {
                _orderManager.Delivered(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetByDateAndShift(OrderSelectedDTO orderDto)
        {
            try
            {
                var claimEmployeeId = _httpContextAccessor.HttpContext.User.FindFirst("EmployeeId");
                var employeeId = (claimEmployeeId == null) ? 0 : Int32.Parse(claimEmployeeId.Value);
                return Ok(_orderManager.GetByDateAndShift(employeeId, orderDto.Date, orderDto.Shift));
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
                return Ok(_orderManager.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetByRfid(OrderRfidDTO orderDto)
        {
            try
            {
                return Ok(_orderManager.GetByRfid(orderDto.Rfid, orderDto.Date, orderDto.Shift));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetFiltered(OrderFilteredRequestDTO orderDto)
        {
            try
            {
                var claimRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
                if (claimRole == null || claimRole.Value != "HR")
                {
                    var claimEmployeeId = _httpContextAccessor.HttpContext.User.FindFirst("EmployeeId");
                    var employeeId = (claimEmployeeId == null) ? 0 : Int32.Parse(claimEmployeeId.Value);
                    orderDto.EmployeeIds = new List<int> { employeeId };
                }

                return Ok(_orderManager.GetFiltered(orderDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
