using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("error")]
    public IActionResult TriggerError()
    {
        throw new Exception("This is a secret internal error message.");
    }

    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        return Ok(new
        {
            message = "You are authenticated."
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult Admin()
    {
        return Ok(new
        {
            message = "You are an Admin."
        });
    }

    [Authorize(Policy = "CanManageProperties")]
    [HttpGet("manage")]
    public IActionResult Manage()
    {
        return Ok(new
        {
            message = "You have permission to manage properties."
        });
    }
}