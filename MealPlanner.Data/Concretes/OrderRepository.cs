using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MealPlanner.Data.Concretes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MealPlannerContext _context;

        public OrderRepository(MealPlannerContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddDeliveryLog(DeliveryLog deliveryLog)
        {
            _context.DeliveryLogs.Add(deliveryLog);
            _context.SaveChanges();
        }

        public void AddSoftMeal(SoftMeal softMeal)
        {
            _context.SoftMeals.Add(softMeal);
            _context.SaveChanges();
        }

        public Order GetByDateAndEmployee(Order order)
        {
           return _context.Orders.Include(x => x.Plan).FirstOrDefault(x => x.Plan.Date == order.Plan.Date && x.EmployeeId == order.EmployeeId);
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Order not found");
            }
        }

        public void Update(Order orderInput)
        {
            var order = _context.Orders.Find(orderInput.Id);

            if (order != null)
            {
                order.PlanId = orderInput.PlanId;
                order.Shift = orderInput.Shift;
                order.IsDelivered = orderInput.IsDelivered;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Order not found");
            }
        }

        public Order GetById(int id)
        {
            var order = _context.Orders.Include(x => x.Plan).ThenInclude(x => x.Meal).FirstOrDefault(x => x.Id == id);

            if (order != null)
            {
                return order;
            }
            else
            {
                throw new Exception("Order not found");
            }
        }

        public Order GetByRfid(string rfid, DateTime date, int shift)
        {
            return _context.Orders
                .Include(x => x.Employee)
                .Include(x => x.Plan)
                .ThenInclude(x => x.Meal)
                .ThenInclude(x => x.MealImage)
                .FirstOrDefault(x => x.Employee.Rfid == rfid && x.Plan.Date == date && x.Shift == shift);
        }

        public SoftMeal GetSoftMeal(string rfid, DateTime date, int shift)
        {
            return _context.SoftMeals
                .Include(x => x.Employee)
                .Include(x => x.SoftMealDetail)
                .FirstOrDefault(x => x.Employee.Rfid == rfid && x.Date == date && x.Shift == shift);
        }

        public SoftMealDetail GetSoftMealDetail(int id)
        {
            return _context.SoftMealDetails.Find(id);
        }

        public int GetByDateAndShift(int employeeId, DateTime date, int shift)
        {
            var order = _context.Orders.Include(x => x.Plan).FirstOrDefault(x => x.EmployeeId == employeeId && x.Plan.Date == date && x.Shift == shift);

            if(order != null)
            {
                return order.PlanId;
            }
            else
            {
                return 0;
            }
        }

        public void Delivered(int id)
        {
            var order = _context.Orders.Find(id);

            if (order != null)
            {
                order.IsDelivered = true;
                order.DeliveredDate = DateTime.Now;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Order not found");
            }
        }

        public void DeliveredSoftMeal(int id)
        {
            var softMeal = _context.SoftMeals.Find(id);

            if (softMeal != null)
            {
                softMeal.IsDelivered = true;
                softMeal.DeliveredDate = DateTime.Now;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Soft Meal not found");
            }
        }

        public List<Order> GetFiltered(List<int> employeeIds, DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            return _context.Orders
                .Include(x => x.Employee).ThenInclude(x => x.User)
                .Include(x => x.Plan).ThenInclude(x => x.Meal)
                .Where(x => employeeIds.Contains(x.Employee.Id) && x.Plan.Date > fromDate && x.Plan.Date < toDate)
                .OrderByDescending(x => x.Plan.Date).ToList();
        }
    }
}
