using Microsoft.VisualBasic.CompilerServices;
using PaymentCalculator.Exceptions;
using PaymentCalculator.Models;
using PaymentCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCalculator.Services
{
    public class FileService
    {
        public static void ProcessFile(string filename, string originalFileName)
        {
            string reportId = originalFileName.Split('-')[^1].Split('.')[0];

            if (PaymentRepository.DoesReportExist(reportId))
            {
                throw new ReportAlreadyExistsException();
            }


            List<PaymentModel> payments = new List<PaymentModel>();

            using (var reader = new StreamReader(filename))
            {
                string dateTimeFormat = "d/M/yyyy";
                CultureInfo provider = CultureInfo.InvariantCulture;

                reader.ReadLine(); //skip the header
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    payments.Add(new PaymentModel
                    {
                        Date = DateTime.ParseExact(values[0], dateTimeFormat, provider),
                        HoursWorked = Double.Parse(values[1]),
                        EmployeeId = int.Parse(values[2]),
                        JobGroup = values[3]
                    });
                }
            }

            if (payments.Count > 0)
            {
                PaymentRepository.WriteRecords(payments, reportId);
            }
        }
    }
}
