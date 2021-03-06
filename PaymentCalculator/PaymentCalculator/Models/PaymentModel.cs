﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    public class PaymentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [IgnoreDataMember]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
        public int EmployeeId { get; set; }
        public string JobGroup { get; set; }
    }
}
