using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    public class PayScale
    {
        public static Dictionary<string, double> Payments { get; private set; }

        static PayScale()
        {
            Payments = new Dictionary<string, double>();
            Payments.Add("A", 20);
            Payments.Add("B", 30);
        }

    }
}
