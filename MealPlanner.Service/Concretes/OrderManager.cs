using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Helpers;
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

        public OrderAddResponseDTO Add(OrderDTO orderDto)
        {
            var order = new Order
            { 
                EmployeeId = orderDto.EmployeeId,
                IsDelivered = orderDto.IsDelivered,
                PlanId = orderDto.PlanId,
                Shift = orderDto.Shift,
                Plan = _planRepository.GetById(orderDto.PlanId)
            };

            if (DateTime.Now > order.Plan.EditableFrom && DateTime.Now <= order.Plan.EditableTo.AddHours(23).AddMinutes(59).AddSeconds(59))
            {
                var orderDb = _orderRepository.GetByDateAndEmployee(order);
                if (orderDb == null)
                {
                    _orderRepository.Add(order);
                    return new OrderAddResponseDTO { Message = "" };
                }
                else
                {
                    order.Id = orderDb.Id;
                    var differentShift = order.Shift != orderDb.Shift;

                    _orderRepository.Update(order);

                    if (differentShift)
                    {
                        return new OrderAddResponseDTO { Message = "Променет оброк за истиот датум од друга смена" };
                    }
                    else
                    {
                        return new OrderAddResponseDTO { Message = "" };
                    }
                }
            }
            else
            {
                throw new Exception("Моментално сте надвор од периодот за избирање на овој оброк");
            }
        }

        public void EditFromList(OrderForEditDTO orderDto)
        {
            var order = new Order
            {
                Id = orderDto.OrderId,
                EmployeeId = orderDto.EmployeeId,
                IsDelivered = false,
                PlanId = orderDto.PlanId,
                Shift = orderDto.Shift,
                Plan = _planRepository.GetById(orderDto.PlanId)
            };

            _orderRepository.Update(order);
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

        public void Delivered(int id)
        {
            _orderRepository.Delivered(id);
        }

        public int GetByDateAndShift(int employeeId, DateTime date, int shift)
        {
            return _orderRepository.GetByDateAndShift(employeeId, date, shift);
        }

        public OrderForEditDTO GetById(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order != null)
            {
                return new OrderForEditDTO
                {
                    OrderId = order.Id,
                    Date = order.Plan.Date,
                    Shift = order.Shift,
                    PlanId = order.Plan.Id
                };
            }
            else
            {
                return null;
            }
        }

        public OrderDeliveryDTO GetByRfid(string rfid, DateTime date, int shift)
        {
            var order = _orderRepository.GetByRfid(rfid, date, shift);

            if (order != null)
            {
                return new OrderDeliveryDTO
                {
                    OrderId = order.Id,
                    IsDelivered = order.IsDelivered,
                    ImageBase64 = order.Plan.Meal.ImageBase64,
                    Name = order.Plan.Meal.Name,
                    NameForeign = order.Plan.Meal.NameForeign
                };
            }
            else
            {
                return null;
            }
        }

        public List<OrderFilteredResponseDTO> GetFiltered(OrderFilteredRequestDTO request)
        {
            var orders = _orderRepository.GetFiltered(request.EmployeeIds, request.FromDate, request.ToDate);
            var response = new List<OrderFilteredResponseDTO>();

            foreach(var item in orders)
            {
                response.Add(new OrderFilteredResponseDTO
                {
                    Employee = item.Employee.User.UserName,
                    Date = item.Plan.Date,
                    Meal = item.Plan.Meal.Name,
                    OrderId = item.Id,
                    Shift = PlanHelper.GetShiftName(item.Shift),
                    IsDelivered = item.IsDelivered
                });
            }

            return response;
        }
    }
}
