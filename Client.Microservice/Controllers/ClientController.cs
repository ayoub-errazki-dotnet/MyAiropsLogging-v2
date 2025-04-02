using System.Text;
using System.Text.Json;
using Logging.Microservice.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Client.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string _loggingServiceUrl = "http://localhost:5208/api/logging";

        public ClientController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string message, string level)
        {
            try
            {
                if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(level))
                    return BadRequest("Message and level are required");

                var levelEnum = Enum.Parse<Level>(level, true);
                var log = new LogDto
                {
                    Id = Guid.NewGuid(),
                    Message = message,
                    Level = levelEnum,
                    TimeStamp = DateTime.Now
                };
                var content = new StringContent(JsonSerializer.Serialize(log), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_loggingServiceUrl, content);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);

                return Ok(content);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
