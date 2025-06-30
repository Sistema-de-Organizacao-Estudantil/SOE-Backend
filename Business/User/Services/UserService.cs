namespace SistemaOrganizacaoEstudantil.Business.User.Services;

using Exceptions;
using Infrastructure;
using Models;

using Microsoft.EntityFrameworkCore;

public class UserService
{
    public UserService(DataContext context)
    {
        this.context = context;
    }

    public async Task<UserModel> GetById(int id)
    {
        var user = await context.Users
            .Where(user => user.Id == id)
            .Select(user => UserModel.FromUser(user))
            .FirstAsync();

        if (user == null)
            throw new UserNotFoundException(id);

        return user;
    }

    public async Task<UserModel> GetByEmail(string email)
    {
        var user = await context.Users
            .Where(user => user.Email == email)
            .Select(user => UserModel.FromUser(user))
            .FirstAsync();

        if (user == null)
            throw new UserNotFoundException(email);

        return user;
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        return await context.Users
            .Select(user => UserModel.FromUser(user))
            .ToListAsync();
    }

    private DataContext context;
}
