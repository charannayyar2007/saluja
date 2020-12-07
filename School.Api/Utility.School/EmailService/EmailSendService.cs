using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace School.Utility.EmailService
{
  public static  class EmailSendService
    {
        public static bool SendMail(string htmlString,string toMail,string Subject)
        {
            try
            {
                string FromMail = ConfigurationManager.AppSettings["FromMail"];
                string Password = ConfigurationManager.AppSettings["MailPassword"];
                string Host = ConfigurationManager.AppSettings["HostMail"];
                string Port = ConfigurationManager.AppSettings["PortMail"];
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(FromMail);
                message.To.Add(new MailAddress(toMail));
                message.Subject = Subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = Convert.ToInt32(Port);
                smtp.Host = Host; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(FromMail, Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
