﻿using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Add(OrderDTO orderDto)
        {
            var order = new Order
            { 
                Date = orderDto.Date,
                EmployeeId = orderDto.EmployeeId,
                IsDelivered = orderDto.IsDelivered,
                MealId = orderDto.MealId,
                Shift = orderDto.Shift
            };

            _orderRepository.Add(order);
        }

        public void Update(OrderDTO orderDto)
        {
            var order = new Order
            {
                Id = orderDto.Id,
                Date = orderDto.Date,
                EmployeeId = orderDto.EmployeeId,
                IsDelivered = orderDto.IsDelivered,
                MealId = orderDto.MealId,
                Shift = orderDto.Shift
            };

            _orderRepository.Add(order);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }
    }
}
