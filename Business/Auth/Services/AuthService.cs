namespace SistemaOrganizacaoEstudantil.Business.Auth.Services;

using Models;
using Entities;
using Exceptions;
using Infrastructure;
using Requests.Auth;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    public AuthService(DataContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public async Task<String> Login(LoginRequest request)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
        if (user == null)
            throw new InvalidCredentialsException();

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new InvalidCredentialsException();

        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);

        var handler = new JwtSecurityTokenHandler();
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: new[] {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            },
            expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:ExpiresInMinutes"]!)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return handler.WriteToken(token);
    }

    public async Task<UserModel> Register(RegisterRequest request)
    {
        var user = new User {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };

        var hasher = new PasswordHasher<User>();
        user.Password = hasher.HashPassword(user, user.Password);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return UserModel.FromUser(user);
    }

    private DataContext context;
    private readonly IConfiguration configuration;
}
