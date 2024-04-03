using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("erpmail.techvision@gmail.com", "xqgggsdwilmjrfff"),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("erpmail.techvision@gmail.com"),
                    Subject = "Test email subject",
                    Body = "This is a test email body",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add("shobuz.cse03@gmail.com");

                client.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
            }
        }        

    }
    

}
