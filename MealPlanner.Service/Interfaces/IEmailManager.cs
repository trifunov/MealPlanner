using System;
using System.Collections.Generic;
using System.Text;

namespace MealPlanner.Service.Interfaces
{
    public interface IEmailManager
    {
        void SendEmail(string subject, string body, string toMail);
        string PrepareAddEmail(string rfid, DateTime date, string meal, string company);
        string PrepareEditEmail(string rfid, DateTime date, string oldMeal, string newMeal, string company);
        string PrepareDeleteEmail(string rfid, DateTime date, string company);
    }
}
