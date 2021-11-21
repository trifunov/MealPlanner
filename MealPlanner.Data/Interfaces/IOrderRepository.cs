using MealPlanner.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Data.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void AddSoftMeal(SoftMeal softMeal);
        void Update(Order order);
        void Delete(int id);
        void Delivered(int id);
        void DeliveredSoftMeal(int id);
        void AddDeliveryLog(DeliveryLog deliveryLog);
        Order GetById(int id);
        Order GetByRfid(string rfid, DateTime date, int shift);
        SoftMeal GetSoftMeal(string rfid, DateTime date, int shift);
        SoftMealDetail GetSoftMealDetail(int id);
        int GetByDateAndShift(int employeeId, DateTime date, int shift);
        Order GetByDateAndEmployee(Order order);
        List<Order> GetFiltered(List<int> employeeIds, DateTime fromDate, DateTime toDate);
    }
}
