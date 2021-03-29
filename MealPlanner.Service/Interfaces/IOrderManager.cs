using MealPlanner.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IOrderManager
    {
        OrderAddResponseDTO Add(OrderDTO orderDto, string role);
        void EditFromList(OrderForEditDTO orderDto, string role);
        void Update(OrderDTO orderDto);
        OrderAddResponseDTO Delete(int id, string role);
        void Delivered(int id);
        int GetByDateAndShift(int employeeId, DateTime date, int shift);
        OrderForEditDTO GetById(int id);
        OrderDeliveryDTO GetByRfid(string rfid, DateTime date, int shift);
        List<OrderFilteredResponseDTO> GetFiltered(OrderFilteredRequestDTO request);
    }
}
