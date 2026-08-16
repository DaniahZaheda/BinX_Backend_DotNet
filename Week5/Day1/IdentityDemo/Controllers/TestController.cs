using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok(new
        {
            message = "You are authenticated!"
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new
        {
            message = "Welcome Admin!"
        });
    }

    [Authorize(Policy = "CanManageProperties")]
    [HttpGet("manage")]
    public IActionResult ManageProperties()
    {
        return Ok(new
        {
            message = "You have permission to manage properties!"
        });
    }
}

