//using AspNetCore.Reporting;
using AspNetCore.Reporting;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace RDLCReport.Con
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            // Console.WriteLine("Hello World!");
            ExportPDFReport();
            //PrintReport();
        }

        

        private static void ExportPDFReport()
        {
            var dt = new DataTable();
            dt = GetEmployeeList();
            string mimetype = "";
            int extension = 1;
            //var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\RptEmployees.rdlc";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDirectory, "RptEmployees.rdlc");
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC report (Set as parameter)");
            LocalReport lr = new LocalReport(path);
            lr.AddDataSource("dtEmployee", dt);
            var result = lr.Execute(RenderType.Pdf, extension, parameters, mimetype);
            //return File(result.MainStream, "application/pdf");
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
    }
}
