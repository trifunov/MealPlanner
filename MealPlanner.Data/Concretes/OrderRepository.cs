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
            var orderDb = _context.Orders.Include(x => x.Plan).FirstOrDefault(x => x.Plan.Date == order.Plan.Date && x.EmployeeId == order.EmployeeId && x.Shift == order.Shift);

            if (orderDb == null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                orderDb.PlanId = order.PlanId;
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

        public List<Order> GetOrdersByRfid(string rfid)
        {
            throw new NotImplementedException();
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
    }
}
