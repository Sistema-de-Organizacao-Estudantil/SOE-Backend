namespace SistemaOrganizacaoEstudantil.Controllers;

using Business.Auth.Services;
using Requests.Auth;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    public AuthController(AuthService service)
    {
        this.service = service;
    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok(await service.Login(request));
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        return Ok(await service.Register(request));
    }

    AuthService service;
}
