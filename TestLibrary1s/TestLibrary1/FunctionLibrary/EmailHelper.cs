using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace TestLibrary1.FunctionLibrary
{
    public static class EmailHelper
    {
        public static bool SendEmail(List<string> to, string subject, string body)
        {
            bool success = true;

            MailAddress fromMailAddress = new MailAddress(GlobalConfig.AppSettingsLookup("emailSender"), GlobalConfig.AppSettingsLookup("senderDisplayName"));

            MailMessage message = new MailMessage();
            foreach(string t in to)
            {
                message.To.Add(t);
            }
            message.From = fromMailAddress;
            message.Body = body;
            message.Subject = subject;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();

            client.Send(message);

            return success;
        }
    }
}
