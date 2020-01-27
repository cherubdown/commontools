using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Intranet2.Helpers
{
    public class EmailHelper
    {
        private static string DefaultHost = "192.168.1.3";
        private static int DefaultPort = 25;
        private static string DefaultFromEmailAddress = "wabur@grainc.net";
        private static string AdminEmailAddress = "chris.bales@grainc.net";

        /// <summary>
        /// Safely send an email using the configuration interface.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="toEmailAddress"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public static void SendMail(string toEmailAddress,
            string subject, string message)
        {
            string host = DefaultHost;
            int port = DefaultPort;
            string fromEmail = DefaultFromEmailAddress;
            
            SmtpClient smtpServer = new SmtpClient()
            {
                Host = host,
                Port = port,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                IsBodyHtml = true,
                Body = message
            };
            mail.To.Add(toEmailAddress);
            
            try
            {
                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                EmailHelper.SendErrorEmail(ex);
            }
        }

        public static void SendErrorEmail(string error)
        {
            SendMail(AdminEmailAddress, "Error", "An error occurred.<br/><br/>" + error);
        }

        public static void SendErrorEmail(Exception ex)
        {
            SendMail(AdminEmailAddress, "Error", "An error occurred.<br/><br/>" + ex.StackTrace);
        }

        public static void SendAdminEmail(string subject, string message)
        {
            SendMail(AdminEmailAddress, subject, message);
        }
    }
}
