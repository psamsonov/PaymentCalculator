using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    public class EmployeeReport
    {
        public int EmployeeID { get; set; }

        public PayPeriod PayPeriod { get; set; }
        
        public double HoursWorked { get; set; }


        //Special formatting for consuption by the front end
        [JsonIgnore]
        public double AmountPaid { get; set; }

        public string amountPaid { get => "$" + Math.Round(AmountPaid, 2); }
    }

    

}
