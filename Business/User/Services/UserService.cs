namespace SistemaOrganizacaoEstudantil.Business.User.Services;

using Exceptions;
using Infrastructure;
using Contracts.Responses;

using Microsoft.EntityFrameworkCore;

public class UserService
{
    public UserService(DataContext context)
    {
        this.context = context;
    }

    public async Task<UserResponse> GetById(int id)
    {
        var user = await context.Users
            .Where(user => user.Id == id)
            .Select(user => UserResponse.FromUser(user))
            .FirstAsync();

        if (user == null)
            throw new UserNotFoundException(id);

        return user;
    }

    public async Task<UserResponse> GetByEmail(string email)
    {
        var user = await context.Users
            .Where(user => user.Email == email)
            .Select(user => UserResponse.FromUser(user))
            .FirstAsync();

        if (user == null)
            throw new UserNotFoundException(email);

        return user;
    }

    public async Task<IEnumerable<UserResponse>> GetAll()
    {
        return await context.Users
            .Select(user => UserResponse.FromUser(user))
            .ToListAsync();
    }

    private DataContext context;
}
