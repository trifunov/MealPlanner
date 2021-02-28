using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        List<Order> GetOrdersByRfid(string rfid);
    }
}
