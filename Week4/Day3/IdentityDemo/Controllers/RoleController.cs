using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Assign a role to a user
    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(
        string email,
        string role)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!await _roleManager.RoleExistsAsync(role))
        {
            return BadRequest("Role does not exist.");
        }

        var result = await _userManager.AddToRoleAsync(
            user,
            role
        );

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            message = $"User {email} assigned to role {role}."
        });
    }

    // Remove a role from a user
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRole(
        string email,
        string role)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var result = await _userManager.RemoveFromRoleAsync(
            user,
            role
        );

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            message = $"Role {role} removed from {email}."
        });
    }

    // Get user's roles
    [HttpGet("user-roles")]
    public async Task<IActionResult> GetUserRoles(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            email = user.Email,
            roles = roles
        });
    }
}

