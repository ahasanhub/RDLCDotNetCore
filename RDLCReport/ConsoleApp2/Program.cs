using ConsoleApp2.EmailService;
using ConsoleApp2.ReportDataset;

using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
           
            var dt = new DataTable();
            dt = GetEmployeeList();

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string path = Path.Combine(baseDirectory, "RptEmployees.rdlc");


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = "RptEmployees.rdlc";
           

            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(new ReportDataSource("dtEmployee", dt));

            // Optionally, define parameters
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("prm", "RDLC report (Set as parameter)"); // Replace "Parameter1" with your parameter name and "Value1" with your desired value

            // Set the parameters to the report
            viewer.LocalReport.SetParameters(parameters);

            // Refresh the report viewer
            viewer.RefreshReport();


            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            //using (System.IO.FileStream fs = new System.IO.FileStream("output1.pdf", System.IO.FileMode.Create))
            //{
            //    fs.Write(bytes, 0, bytes.Length);
            //}
            string mailTo = "shobuz.cse03@gmail.com";
            //string mailTo = "ataur017@gmail.com";
            string mailSubject = "Test mail with attachment";
            string mailBody = "Test mail with attachment";
            

            Mailer.SendMail(GetSmtpSettings(), mailTo, mailSubject, mailBody, bytes,"NotificationMessage.pdf");
        }
        private static DataTable GetEmployeeList()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Department");
            dt.Columns.Add("BirthDate");
            DataRow row;

            for (int i = 1; i < 100; i++)
            {
                row = dt.NewRow();
                row["EmpId"] = i;
                row["EmpName"] = i.ToString() + " Empl";
                row["Department"] = "XXYY";
                row["BirthDate"] = DateTime.Now.AddDays(-10000);
                dt.Rows.Add(row);
            }
            return dt;
        }
        private static SmtpSettings GetSmtpSettings()
        {
            var server = ConfigurationManager.AppSettings["Server"];
            var port = ConfigurationManager.AppSettings["Port"];
            var senderName = ConfigurationManager.AppSettings["SenderName"];
            var senderEmail = ConfigurationManager.AppSettings["SenderEmail"];
            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];


            SmtpSettings smtpSettings = new SmtpSettings { 
            Server = server,
            Port=int.Parse(port),
            SenderName=senderName,
            SenderEmail=senderEmail,
            Username=username,
            Password=password
            };
           

            return smtpSettings;

        }
    }
}
