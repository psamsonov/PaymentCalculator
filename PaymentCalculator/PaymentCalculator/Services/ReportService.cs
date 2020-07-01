using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentCalculator.Models;
using PaymentCalculator.Repositories;

namespace PaymentCalculator.Services
{
    public class ReportService
    {
        public static IEnumerable<EmployeeReport> GetReport()
        {
            return PaymentRepository.GenerateReport();
        }
    }
}
