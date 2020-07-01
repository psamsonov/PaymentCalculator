using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace PaymentCalculator.Models
{
    public class PayPeriod : IComparable
    {
        //Specially formatted properties for consumption by the front end
        public string startDate { get => StartDate.Date.ToShortDateString(); }
        public string endDate { get => EndDate.ToShortDateString(); }

        [JsonIgnore]
        public DateTime StartDate { get; set; }

        [JsonIgnore]
        public DateTime EndDate { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as PayPeriod;

            if (other == null)
                return false;

            bool equals = other.StartDate.Date == this.StartDate.Date && other.EndDate.Date == this.EndDate.Date;
            return equals;
        }

        public override int GetHashCode()
        {
            return StartDate.GetHashCode() ^ EndDate.GetHashCode();
        }

        /// <summary>
        /// Return pay period for a certain input date.
        /// If the date is in the first half of the month, the 1-15th pay period is returned.
        /// Otherwise, the 16-end of month pay period is returned.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static PayPeriod FindPayPeriod(DateTime date)
        {
            if (date.Day <= 15)
            {
                return new PayPeriod
                {
                    StartDate = new DateTime(date.Year, date.Month, 1),
                    EndDate = new DateTime(date.Year, date.Month, 15)
                };
            }
            else
            {
                return new PayPeriod
                {
                    StartDate = new DateTime(date.Year, date.Month, 16),
                    EndDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
                };
            }
        }

        public int CompareTo(object obj)
        {
            return (int)(this.StartDate - (obj as PayPeriod).StartDate).TotalDays;
        }
    }


}
