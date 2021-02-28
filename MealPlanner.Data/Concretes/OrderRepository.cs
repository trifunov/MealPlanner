using MealPlanner.Data.Interfaces;
using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
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

        public List<Order> GetOrdersByRfid(string rfid)
        {
            throw new NotImplementedException();
        }
    }
}
