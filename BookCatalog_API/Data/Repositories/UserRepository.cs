using BookCatalog_API.Entities;
using BookCatalog_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BookCatalog_API.Data.Repositories;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<bool> AddUserAsync(User user)
    {
        context.Users.Add(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        User? user = await context.Users
            .FirstOrDefaultAsync(a => a.Email == email);
        if (user == null) 
            throw new ArgumentNullException("User was not found");

        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        if (user == null) throw new ArgumentNullException("User was not found");
        return user;
    }

    public async Task<bool> RemoveUserAsync(User user)
    {
        context.Users.Remove(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUser(User user)
    {
        context.Users.Update(user);
        return await context.SaveChangesAsync() > 0;
    }
}
