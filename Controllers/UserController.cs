namespace SistemaOrganizacaoEstudantil.Controllers;

using Business.User.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    public UserController(UserService service)
    {
        this.service = service;
    }

    [Route("me")]
    [HttpGet]
    public async Task<IActionResult> Me()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value!;
        return Ok(await service.GetByEmail(email));
    }

    UserService service;
}
