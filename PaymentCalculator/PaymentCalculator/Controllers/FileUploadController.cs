using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentCalculator.Exceptions;
using PaymentCalculator.Services;

namespace PaymentCalculator.Controllers
{
    [ApiController]
    [Route("files")]
    public class FileUploadController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                try
                {
                   FileService.ProcessFile(filePath, file.FileName);
                }
                catch (ReportAlreadyExistsException)
                {
                    return BadRequest("This report was already uploaded");
                }

                return Ok();
            }
            else
            {
                return BadRequest("The uploaded file is invalid");
            }
            
        }
    }
}
