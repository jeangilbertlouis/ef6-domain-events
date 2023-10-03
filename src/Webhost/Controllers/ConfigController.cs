using Microsoft.AspNetCore.Mvc;

namespace Webhost.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly Config _config;

    public ConfigController(Config config)
    {
        _config = config;
    }

    [HttpGet]
    [Route("/config")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(_config);
    }
}