using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IOrderManager
    {
        void Add(OrderDTO orderDto);
        void Update(OrderDTO orderDto);
        void Delete(int id);
    }
}
