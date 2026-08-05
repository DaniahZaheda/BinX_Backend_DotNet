using Microsoft.AspNetCore.Mvc;
using Day5Lab.Services;

namespace Day5Lab.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IGreetingService _service;

    public HomeController(IGreetingService service)
    {
        _service = service;
    }


    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetGreeting());
    }
}