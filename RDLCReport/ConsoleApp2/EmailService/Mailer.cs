
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ConsoleApp2.EmailService
{

    public class Mailer
    {
        
        public static void SendMail(SmtpSettings settings, string mailTo, string subject, string mailBody, byte[] mailAttachment,string attachmentName)
        {
            try
            {
                

                // Mail server (SMTP) configuration
                SmtpClient smtpClient = new SmtpClient(settings.Server, settings.Port)
                {
                    Credentials = new NetworkCredential(settings.SenderEmail, settings.Password),
                    EnableSsl = true
                };                

                // Create a MailMessage object
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(settings.SenderEmail),
                    Subject = subject,
                    Body = mailBody,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(mailTo);

                MemoryStream memoryStream = new MemoryStream(mailAttachment);

                // Add an attachment
                Attachment attachment = new Attachment(memoryStream, attachmentName, MediaTypeNames.Application.Octet);
                mailMessage.Attachments.Add(attachment);

                // Send the email
                smtpClient.Send(mailMessage);

                Console.WriteLine("Email with attachment sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
    }
}
