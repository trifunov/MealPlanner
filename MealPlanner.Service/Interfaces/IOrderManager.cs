using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IOrderManager
    {
        OrderAddResponseDTO Add(OrderDTO orderDto);
        void EditFromList(OrderForEditDTO orderDto);
        void Update(OrderDTO orderDto);
        void Delete(int id);
        void Delivered(int id);
        int GetByDateAndShift(int employeeId, DateTime date, int shift);
        OrderForEditDTO GetById(int id);
        OrderDeliveryDTO GetByRfid(string rfid, DateTime date, int shift);
        List<OrderFilteredResponseDTO> GetFiltered(OrderFilteredRequestDTO request);
    }
}
