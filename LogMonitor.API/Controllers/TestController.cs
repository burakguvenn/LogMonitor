namespace LogMonitor.API.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult GetError()
    {
        throw new Exception ("Kontrol için konulan test hatasıdır.");
    }

    [HttpGet("ok")]
    public IActionResult GetOk()
    {
        return Ok("API çalışıyor.");
    }
}