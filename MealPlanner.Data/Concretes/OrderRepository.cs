using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
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
            var orderDb = _context.Orders.FirstOrDefault(x => x.Date == order.Date && x.EmployeeId == order.EmployeeId && x.Shift == order.Shift);

            if (orderDb == null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                orderDb.MealId = order.MealId;
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
                order.MealId = orderInput.MealId;
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
    }
}
