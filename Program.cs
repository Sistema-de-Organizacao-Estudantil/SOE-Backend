namespace SistemaOrganizacaoEstudantil;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Program
{
    public static void Main(String[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<Infrastructure.DataContext>();

        builder.Services.AddScoped<Business.User.Services.UserService>();
        builder.Services.AddScoped<Business.Auth.Services.AuthService>();

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                var jwt = builder.Configuration.GetSection("Jwt");
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwt["Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddCors(options => {
            options.AddDefaultPolicy(policy => {
                policy
                    .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        app.UseMiddleware<Middleware.ExceptionHandlerMiddleware>();

        app.UseCors();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<Infrastructure.DataContext>();
            db.Database.Migrate();
        }

        app.MapControllers();
        app.Run();
    }
}
