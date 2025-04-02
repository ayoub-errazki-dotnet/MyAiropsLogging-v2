using Logging.Microservice.Interfaces;
using Logging.Microservice.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        public LoggingController(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }
        [HttpPost]
        public IActionResult Log(LogDto log)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _loggingService.LogMessage(log);

                return Ok(log);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
