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
            var orderDb = _context.Orders.Include(x => x.Plan).FirstOrDefault(x => x.Plan.Date == order.Plan.Date && x.EmployeeId == order.EmployeeId);

            if (orderDb == null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                orderDb.PlanId = order.PlanId;
                orderDb.Shift = order.Shift;
                _context.SaveChanges();
            }
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
                order.IsDelivered = orderInput.IsDelivered;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Order not found");
            }
        }

        public Order GetByRfid(string rfid, DateTime date, int shift)
        {
            return _context.Orders.Include(x => x.Employee).Include(x => x.Plan).ThenInclude(x => x.Meal).FirstOrDefault(x => x.Employee.Rfid == rfid && x.Plan.Date == date && x.Shift == shift && x.IsDelivered == false);
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
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Order not found");
            }
        }
    }
}
