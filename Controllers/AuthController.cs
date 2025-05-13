using AuthenticationApiProject.Application.DTOs;
using AuthenticationApiProject.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApiProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public String Health()
    {
        return "Authentication API Running...";
    }

    [HttpPost("isAuthenticated")]
    public async Task<IActionResult> IsAuthenticated(string jwtToken)
    {
        Task<bool> isAuthenticated = _authService.IsUserAuthenticated(jwtToken);
        return Ok(isAuthenticated);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto dto)
    {
        var token = await _authService.RegisterAsync(dto.Email, dto.Password, dto.Username, dto.ProfileImage);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto.Email, dto.Password);
        if (token == null) return Unauthorized();
        return Ok(new { token });
    }
}
