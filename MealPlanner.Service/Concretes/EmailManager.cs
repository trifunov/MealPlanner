using MealPlanner.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace MealPlanner.Service.Concretes
{
    public class EmailManager : IEmailManager
    {
        public string PrepareAddEmail(string rfid, DateTime date, string meal)
        {
            string m = File.ReadAllText(@"c:\inetpub\wwwroot\mi\html_promena_obrok.txt");
            return m.Replace("[idno]", rfid).Replace("[dayte]", date.ToString("yyyy-MM-dd")).Replace("[dodobrok]", meal);
        }

        public string PrepareDeleteEmail(string rfid, DateTime date)
        {
            string m = File.ReadAllText(@"c:\inetpub\wwwroot\mi\html_promena_obrok.txt");
            return m.Replace("[idno]", rfid).Replace("[dayte]", date.ToString("yyyy-MM-dd"));
        }

        public string PrepareEditEmail(string rfid, DateTime date, string oldMeal, string newMeal)
        {
            string m = File.ReadAllText(@"c:\inetpub\wwwroot\mi\html_promena_obrok.txt");
            return m.Replace("[idno]", rfid).Replace("[dayte]", date.ToString("yyyy-MM-dd")).Replace("[preobrok]", oldMeal).Replace("[novobrok]", newMeal);
        }

        public void SendEmail(string subject, string body, string toMail)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("robot@dalma.com.mk");
            message.To.Add(toMail);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Host = "mail.dalma.com.mk";
            smtpClient.Port = 25;
            smtpClient.EnableSsl = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("robot", "K4jBC37>PA_Nf%\"^");
            smtpClient.Send(message);
        }
    }
}
