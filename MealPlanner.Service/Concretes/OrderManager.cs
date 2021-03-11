using MealPlanner.Data.Interfaces;
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
        private readonly IPlanRepository _planRepository;

        public OrderManager(IOrderRepository orderRepository, IPlanRepository planRepository)
        {
            _orderRepository = orderRepository;
            _planRepository = planRepository;
        }

        public void Add(OrderDTO orderDto)
        {
            var order = new Order
            { 
                EmployeeId = orderDto.EmployeeId,
                IsDelivered = orderDto.IsDelivered,
                PlanId = orderDto.PlanId,
                Shift = orderDto.Shift,
                Plan = _planRepository.GetById(orderDto.PlanId)
            };

            _orderRepository.Add(order);
        }

        public void Update(OrderDTO orderDto)
        {
            var order = new Order
            {
                Id = orderDto.Id,
                EmployeeId = orderDto.EmployeeId,
                IsDelivered = orderDto.IsDelivered,
                PlanId = orderDto.PlanId,
                Shift = orderDto.Shift
            };

            _orderRepository.Add(order);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }

        public int GetByDateAndShift(int employeeId, DateTime date, int shift)
        {
            return _orderRepository.GetByDateAndShift(employeeId, date, shift);
        }
    }
}
