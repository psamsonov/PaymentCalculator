using NUnit.Framework;
using PaymentCalculator.Models;
using PaymentCalculator.Services;
using System.Collections.Generic;
using System.Linq;

namespace PaymentCalculator.Tests
{
    public class ReportServiceTests
    {
        PayPeriod FirstHalfOfMonth;
        PayPeriod SecondHalfOfMonth;

        [SetUp]
        public void Setup()
        {
            FirstHalfOfMonth = new PayPeriod
            {
                StartDate = new System.DateTime(2020, 7, 1),
                EndDate = new System.DateTime(2020, 7, 15)
            };
            SecondHalfOfMonth = new PayPeriod
            {
                StartDate = new System.DateTime(2020, 7, 16),
                EndDate = new System.DateTime(2020, 7, 31)
            };
        }

        [Test]
        public void EmptyTest()
        {
            var result = ReportService.GenerateReport(new List<PaymentModel>());
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void PayPeriodTest()
        {
            var temp = PayPeriod.FindPayPeriod(new System.DateTime(2020, 7, 14));
            Assert.AreEqual(FirstHalfOfMonth, temp);
            temp = PayPeriod.FindPayPeriod(new System.DateTime(2020, 7, 16));
            Assert.AreEqual(SecondHalfOfMonth, temp);
        }

        [Test]
        public void BasicReportTest()
        {
            PaymentModel test = new PaymentModel
            {
                Date = new System.DateTime(2020, 7, 16),
                EmployeeId = 1,
                HoursWorked = 1,
                JobGroup = "A"
            };

            var result = ReportService.GenerateReport(new List<PaymentModel> { test });
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().EmployeeID);
            Assert.AreEqual(SecondHalfOfMonth, result.First().PayPeriod);
            Assert.AreEqual(20.00d, result.First().AmountPaid);
        }

        [Test]
        public void AdvancedReportTest()
        {
            PaymentModel test = new PaymentModel
            {
                Date = new System.DateTime(2020, 7, 16),
                EmployeeId = 1,
                HoursWorked = 1,
                JobGroup = "A"
            };

            PaymentModel test2 = new PaymentModel
            {
                Date = new System.DateTime(2020, 7, 17),
                EmployeeId = 1,
                HoursWorked = 1,
                JobGroup = "B"
            };

            PaymentModel test3 = new PaymentModel
            {
                Date = new System.DateTime(2020, 7, 14),
                EmployeeId = 1,
                HoursWorked = 1,
                JobGroup = "B"
            };

            var result = ReportService.GenerateReport(new List<PaymentModel> { test, test2 });
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().EmployeeID);
            Assert.AreEqual(SecondHalfOfMonth, result.First().PayPeriod);
            Assert.AreEqual(50.00d, result.First().AmountPaid);

            result = ReportService.GenerateReport(new List<PaymentModel> { test, test2, test3 });
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.First().EmployeeID);
            Assert.AreEqual(FirstHalfOfMonth, result.First().PayPeriod);
            Assert.AreEqual(30.00d, result.First().AmountPaid);
        }

    }
}