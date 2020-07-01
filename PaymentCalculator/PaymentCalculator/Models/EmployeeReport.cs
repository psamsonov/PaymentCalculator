using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    public class PayPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class EmployeeReport
    {
        public int EmployeeID { get; set; }
        public PayPeriod PayPeriod { get; set; }
        public double AmountPaid { get; set; }
    }
}
