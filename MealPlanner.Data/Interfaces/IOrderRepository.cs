using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
        void Delivered(int id);
        Order GetByRfid(string rfid, DateTime date, int shift);
        int GetByDateAndShift(int employeeId, DateTime date, int shift);
    }
}
