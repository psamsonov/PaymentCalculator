using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentCalculator.Models;
using PaymentCalculator.Services;

namespace PaymentCalculator.Controllers
{
    
    [ApiController]
    [Route("report")]
    public class ReportController : Controller
    {
       [HttpGet]
       public ActionResult<IEnumerable<EmployeeReport>> GetReports()
        {

            return Ok(ReportService.GetReport());
        }
    }
}
