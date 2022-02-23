using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using MealPlanner.Service.DTOs;
using MealPlanner.Service.Helpers;
using MealPlanner.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IEmailManager _emailManager;
        private readonly IEmployeeRepository _employeeRepository;

        public OrderManager(IOrderRepository orderRepository, IPlanRepository planRepository, IEmailManager emailManager, IEmployeeRepository employeeRepository)
        {
            _orderRepository = orderRepository;
            _planRepository = planRepository;
            _emailManager = emailManager;
            _employeeRepository = employeeRepository;
        }

        public OrderAddResponseDTO Add(OrderDTO orderDto, string role)
        {
            var order = new Order
            { 
                EmployeeId = orderDto.EmployeeId,
                IsDelivered = orderDto.IsDelivered,
                PlanId = orderDto.PlanId,
                Shift = orderDto.Shift,
                Plan = _planRepository.GetById(orderDto.PlanId)
            };

            if ((DateTime.Now > order.Plan.EditableFrom && DateTime.Now <= order.Plan.EditableTo.AddHours(23).AddMinutes(59).AddSeconds(59)))
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
            else if(role == "HR")
            {
                var orderDb = _orderRepository.GetByDateAndEmployee(order);
                var employee = _employeeRepository.GetById(order.EmployeeId);

                if (orderDb == null)
                {
                    _orderRepository.Add(order);
                    var emailBody = _emailManager.PrepareAddEmail(employee.Rfid, order.Plan.Date, order.Plan.Meal.Name, employee.Company.Name);
                    _emailManager.SendEmail("Додаден оброк", emailBody, employee.User.Email);
                    _emailManager.SendEmail("Додаден оброк", emailBody, "naracki@dalma.com.mk");
                }
                else
                {
                    order.Id = orderDb.Id;
                    var oldMeal = orderDb.Plan.Meal.Name;

                    _orderRepository.Update(order);
                    var emailBody = _emailManager.PrepareEditEmail(employee.Rfid, order.Plan.Date, oldMeal, order.Plan.Meal.Name, employee.Company.Name);
                    _emailManager.SendEmail("Променет оброк", emailBody, employee.User.Email);
                    _emailManager.SendEmail("Променет оброк", emailBody, "naracki@dalma.com.mk");
                }

                
                return new OrderAddResponseDTO { Message = "Променет оброк надвор од периодот за промени" };
            }
            else
            {
                throw new Exception("Моментално сте надвор од периодот за избирање на овој оброк");
            }
        }

        public void EditFromList(OrderForEditDTO orderDto, string role)
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

            if (DateTime.Now > order.Plan.EditableFrom && DateTime.Now <= order.Plan.EditableTo.AddHours(23).AddMinutes(59).AddSeconds(59))
            {
                _orderRepository.Update(order);
            }
            else if(role == "HR")
            {
                var employee = _employeeRepository.GetById(order.EmployeeId);
                var oldMeal = _orderRepository.GetById(order.Id).Plan.Meal.Name;

                _orderRepository.Update(order);
                var emailBody = _emailManager.PrepareEditEmail(employee.Rfid, order.Plan.Date, oldMeal, order.Plan.Meal.Name, employee.Company.Name);
                _emailManager.SendEmail("Променет оброк", emailBody, employee.User.Email);
                _emailManager.SendEmail("Променет оброк", emailBody, "naracki@dalma.com.mk");
            }
            else
            {
                throw new Exception("Моментално сте надвор од периодот за избирање на овој оброк");
            }
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

        public OrderAddResponseDTO Delete(int id, string role)
        {
            var order = _orderRepository.GetById(id);
            var employee = _employeeRepository.GetById(order.EmployeeId);

            if (DateTime.Now > order.Plan.EditableFrom && DateTime.Now <= order.Plan.EditableTo.AddHours(23).AddMinutes(59).AddSeconds(59))
            {
                _orderRepository.Delete(id);
                return new OrderAddResponseDTO { Message = "" };
            }
            else if(role == "HR")
            {
                _orderRepository.Delete(id);
                var emailBody = _emailManager.PrepareDeleteEmail(employee.Rfid, order.Plan.Date, employee.Company.Name);
                _emailManager.SendEmail("Откажан оброк", emailBody, employee.User.Email);
                _emailManager.SendEmail("Откажан оброк", emailBody, "naracki@dalma.com.mk");
                return new OrderAddResponseDTO { Message = "Променет оброк надвор од периодот за промени" };
            }
            else
            {
                throw new Exception("Моментално сте надвор од периодот за избирање на овој оброк");
            }
        }

        public void Delivered(int orderId, int softMealId, int employeeId)
        {
            if (orderId > 0)
            {
                var log = new DeliveryLog
                {
                    Date = DateTime.Now,
                    Status = 4,
                    Description = "Подигната нарачка",
                    EmployeeId = employeeId,
                    OrderId = orderId,
                    SoftMealId = 0
                };
                _orderRepository.AddDeliveryLog(log);

                _orderRepository.Delivered(orderId);
            }
            else
            {
                var log = new DeliveryLog
                {
                    Date = DateTime.Now,
                    Status = 5,
                    Description = "Подигнат сув оброк",
                    EmployeeId = employeeId,
                    OrderId = 0,
                    SoftMealId = softMealId
                };
                _orderRepository.AddDeliveryLog(log);

                _orderRepository.DeliveredSoftMeal(softMealId);
            }
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
                    PlanId = order.Plan.Id,
                    EmployeeId = order.EmployeeId
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
                if(!order.IsDelivered)
                {
                    var log = new DeliveryLog
                    {
                        Date = DateTime.Now,
                        Status = 0,
                        Description = "Побарана нарачка",
                        EmployeeId = order.EmployeeId,
                        OrderId = order.Id,
                        SoftMealId = 0
                    };
                    _orderRepository.AddDeliveryLog(log);

                    return new OrderDeliveryDTO
                    {
                        OrderId = order.Id,
                        SoftMealId = 0,
                        IsDelivered = order.IsDelivered,
                        ImageBase64 = order.Plan.Meal.MealImage.ImageBase64,
                        Name = order.Plan.Meal.Name,
                        NameForeign = order.Plan.Meal.NameForeign
                    };
                }
                else
                {
                    var log = new DeliveryLog
                    {
                        Date = DateTime.Now,
                        Status = 1,
                        Description = "Веќе е подигната нарачка во оваа смена",
                        EmployeeId = order.EmployeeId,
                        OrderId = order.Id,
                        SoftMealId = 0
                    };
                    _orderRepository.AddDeliveryLog(log);

                    throw new Exception("Веќе е подигната нарачка во оваа смена");
                }
            }
            else
            {
                var softMeal = _orderRepository.GetSoftMeal(rfid, date, shift);
                if(softMeal != null)
                {
                    if (!softMeal.IsDelivered)
                    {
                        var log = new DeliveryLog
                        {
                            Date = DateTime.Now,
                            Status = 2,
                            Description = "Побаран сув оброк",
                            EmployeeId = softMeal.EmployeeId,
                            OrderId = 0,
                            SoftMealId = softMeal.Id
                        };
                        _orderRepository.AddDeliveryLog(log);

                        return new OrderDeliveryDTO
                        {
                            OrderId = 0,
                            SoftMealId = softMeal.Id,
                            IsDelivered = softMeal.IsDelivered,
                            ImageBase64 = softMeal.SoftMealDetail.ImageBase64,
                            Name = softMeal.SoftMealDetail.Name,
                            NameForeign = softMeal.SoftMealDetail.NameForeign
                        };
                    }
                    else
                    {
                        var log = new DeliveryLog
                        {
                            Date = DateTime.Now,
                            Status = 3,
                            Description = "Веќе е подигнат сув оброк во оваа смена",
                            EmployeeId = softMeal.EmployeeId,
                            OrderId = 0,
                            SoftMealId = softMeal.Id
                        };
                        _orderRepository.AddDeliveryLog(log);

                        throw new Exception("Веќе е подигнат сув оброк во оваа смена");
                    }
                }
                else
                {
                    var employee = _employeeRepository.GetByRfid(rfid);
                    var softMealDb = new SoftMeal
                    {
                        Date = date,
                        EmployeeId = employee.Id,
                        IsDelivered = false,
                        Shift = shift,
                        SoftMealDetailId = 1
                    };
                    _orderRepository.AddSoftMeal(softMealDb);

                    var log = new DeliveryLog
                    {
                        Date = DateTime.Now,
                        Status = 2,
                        Description = "Побаран сув оброк",
                        EmployeeId = softMealDb.EmployeeId,
                        OrderId = 0,
                        SoftMealId = softMealDb.Id
                    };
                    _orderRepository.AddDeliveryLog(log);

                    var softMealDetail = _orderRepository.GetSoftMealDetail(1);
                    return new OrderDeliveryDTO
                    {
                        OrderId = 0,
                        SoftMealId = softMealDb.Id,
                        IsDelivered = softMealDb.IsDelivered,
                        ImageBase64 = softMealDetail.ImageBase64,
                        Name = softMealDetail.Name,
                        NameForeign = softMealDetail.NameForeign
                    };
                }
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
