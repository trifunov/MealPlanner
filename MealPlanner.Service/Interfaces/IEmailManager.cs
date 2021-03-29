using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IEmailManager
    {
        void SendEmail(string subject, string body, string toMail);
        string PrepareAddEmail(string rfid, DateTime date, string meal);
        string PrepareEditEmail(string rfid, DateTime date, string oldMeal, string newMeal);
        string PrepareDeleteEmail(string rfid, DateTime date);
    }
}
