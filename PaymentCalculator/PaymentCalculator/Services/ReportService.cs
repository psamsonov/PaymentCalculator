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
            var payments = PaymentRepository.GetPayments();
            return GenerateReport(payments);
        }

        public static IEnumerable<EmployeeReport> GenerateReport(IEnumerable<PaymentModel> payments)
        {
            Dictionary<int, Dictionary<PayPeriod, EmployeeReport>> employees = new Dictionary<int, Dictionary<PayPeriod, EmployeeReport>>();

            foreach (var payment in payments)
            {
                if (!employees.ContainsKey(payment.EmployeeId))
                    employees.Add(payment.EmployeeId, new Dictionary<PayPeriod, EmployeeReport>());

                var employeePaymentPeriods = employees[payment.EmployeeId];
                var payPeriod = PayPeriod.FindPayPeriod(payment.Date);
                var salary = PayScale.Payments[payment.JobGroup] * payment.HoursWorked;
                if (!employeePaymentPeriods.ContainsKey(payPeriod))
                    employeePaymentPeriods.Add(payPeriod, new EmployeeReport
                    {
                        EmployeeID = payment.EmployeeId,
                        PayPeriod = payPeriod
                    });
                employeePaymentPeriods[payPeriod].AmountPaid += salary;
            }

            var reports = new List<EmployeeReport>();

            foreach (var employee in employees.Values)
            {
                foreach (var report in employee.Values.OrderBy(x => x.PayPeriod))
                {
                    reports.Add(report);
                }
            }

            return reports;
        }
    }
}
