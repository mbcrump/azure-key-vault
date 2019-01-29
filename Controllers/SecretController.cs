using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KeyVaultApi.Controllers
{
    [Route("secret")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        private readonly IConfiguration _config;
        public SecretController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Secret = _config.GetValue<string>("deep-thought") });
        }
    }
}