using Microsoft.EntityFrameworkCore;
using PaymentCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace PaymentCalculator.Repositories
{

    public class PaymentsContext : DbContext
    {
        public DbSet<PaymentReport> Reports { get; set; }

        public DbSet<PaymentModel> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=PaymentCalculator.db");
    }

    public class PaymentRepository
    {

        public static IEnumerable<PaymentModel> GetPayments()
        {
            using (var db = new PaymentsContext())
            {
                return new List<PaymentModel>(db.Payments.OrderBy(x => x.EmployeeId).ToList());
            }

        }

        public static void WriteRecords(IEnumerable<PaymentModel> payments, string reportId)
        {
            using (var db = new PaymentsContext())
            {
                db.Reports.Add(new PaymentReport { ReportId = reportId });

                foreach (var payment in payments)
                {
                    db.Payments.Add(payment);
                }
                db.SaveChanges();
            }
        }

        public static bool DoesReportExist(string reportId)
        {
            using (var db = new PaymentsContext())
            {
                var exists = db.Reports.Where(x => x.ReportId == reportId).FirstOrDefault() != null;

                return exists;
            }
        }

    }
}
