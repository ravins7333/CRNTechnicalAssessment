using CRN.Application.DTOs;
using CRN.Application.Services;
using CRN.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRN.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterRequestDto request)
    {
        var result = _authService.Register(request);

        return Ok(result);
    }


    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto request)
    {
        var result = _authService.Login(request);

        return Ok(result);
    }


    [HttpPost("refresh-token")]
    public IActionResult RefreshToken(RefreshTokenRequest request)
    {
        var result = _authService.RefreshToken(request);

        return Ok(result);
    }


    [HttpPost("logout")]
    public IActionResult Logout(RefreshTokenRequest request)
    {
        var result = _authService.Logout(request);

        return Ok(result);
    }


    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok("JWT Token Valid");
    }
}